# App móvil

Esta carpeta es del integrante de la aplicación móvil.

La pantalla se implementa en la rama `feature/6-movil-correspondencia`, creada desde `develop` después de que el pull request #7 esté fusionado.

Debe consumir la API de `web/`:

`POST /api/correspondencias`

```json
{
  "remitente": "Municipalidad",
  "asunto": "Solicitud de informe",
  "tipoDocumento": "Oficio",
  "fechaRecepcion": "2026-10-08"
}
```

Tipos válidos: Carta, Oficio, Memo, Factura, Otro. El asunto admite como máximo 150 caracteres. Si falta un dato, la API responde 400 y no guarda. Si el registro es válido, responde 201 con el correlativo.

La aplicación móvil no se conecta directo a los datos. Solo usa la API.
