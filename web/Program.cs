using System.Globalization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var jsonOpciones = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
};

var archivo = Path.Combine(app.Environment.ContentRootPath, "data", "correspondencias.json");
Directory.CreateDirectory(Path.GetDirectoryName(archivo)!);
var cerrojo = new SemaphoreSlim(1, 1);
string[] tiposValidos = ["Carta", "Oficio", "Memo", "Factura", "Otro"];

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/salud", () => Results.Ok(new { estado = "ok" }));
app.MapGet("/api/correspondencias", async () => Results.Ok(await LeerAsync()));

app.MapPost("/api/correspondencias", async (RegistroEntrada entrada) =>
{
    var errores = Validar(entrada);
    if (errores.Count > 0)
        return Results.BadRequest(new { errores });

    await cerrojo.WaitAsync();
    try
    {
        var lista = await LeerAsync();
        var numero = lista.Count + 1;
        var guardado = new RegistroGuardado(
            numero,
            $"CORR-{numero:0000}",
            entrada.Remitente!.Trim(),
            entrada.Asunto!.Trim(),
            TipoCanonico(entrada.TipoDocumento!),
            entrada.FechaRecepcion!.Trim());

        lista.Add(guardado);
        var temporal = archivo + ".tmp";
        await File.WriteAllTextAsync(temporal, JsonSerializer.Serialize(lista, jsonOpciones));
        File.Move(temporal, archivo, overwrite: true);

        return Results.Created($"/api/correspondencias/{guardado.Id}", new
        {
            guardado.Id,
            guardado.Correlativo,
            guardado.Remitente,
            guardado.Asunto,
            guardado.TipoDocumento,
            guardado.FechaRecepcion,
            mensaje = "Correspondencia registrada correctamente."
        });
    }
    finally
    {
        cerrojo.Release();
    }
});

app.Run();

List<ErrorCampo> Validar(RegistroEntrada entrada)
{
    var errores = new List<ErrorCampo>();

    if (string.IsNullOrWhiteSpace(entrada.Remitente))
        errores.Add(new("remitente", "El remitente es obligatorio."));

    if (string.IsNullOrWhiteSpace(entrada.Asunto))
        errores.Add(new("asunto", "El asunto es obligatorio."));
    else if (entrada.Asunto.Trim().Length > 150)
        errores.Add(new("asunto", "El asunto admite como máximo 150 caracteres."));

    if (string.IsNullOrWhiteSpace(entrada.TipoDocumento))
        errores.Add(new("tipoDocumento", "El tipo de documento es obligatorio."));
    else if (!tiposValidos.Contains(entrada.TipoDocumento.Trim(), StringComparer.OrdinalIgnoreCase))
        errores.Add(new("tipoDocumento", "El tipo de documento no es válido."));

    if (string.IsNullOrWhiteSpace(entrada.FechaRecepcion))
        errores.Add(new("fechaRecepcion", "La fecha de recepción es obligatoria."));
    else if (!DateOnly.TryParseExact(entrada.FechaRecepcion.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        errores.Add(new("fechaRecepcion", "La fecha de recepción no es válida."));

    return errores;
}

string TipoCanonico(string tipo) =>
    tiposValidos.First(item => item.Equals(tipo.Trim(), StringComparison.OrdinalIgnoreCase));

async Task<List<RegistroGuardado>> LeerAsync()
{
    if (!File.Exists(archivo))
        return [];

    await using var flujo = File.OpenRead(archivo);
    var lista = await JsonSerializer.DeserializeAsync<List<RegistroGuardado>>(flujo, jsonOpciones);
    return lista ?? [];
}

record RegistroEntrada(string? Remitente, string? Asunto, string? TipoDocumento, string? FechaRecepcion);
record ErrorCampo(string Campo, string Mensaje);
record RegistroGuardado(int Id, string Correlativo, string Remitente, string Asunto, string TipoDocumento, string FechaRecepcion);
