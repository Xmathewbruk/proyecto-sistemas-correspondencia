const acceso = document.querySelector("#acceso");
const trabajo = document.querySelector("#trabajo");
const formulario = document.querySelector("#formulario");
const aviso = document.querySelector("#aviso");
const cuenta = document.querySelector("#cuenta");
const asunto = document.querySelector("#asunto");
const filas = document.querySelector("#filas");
const tabla = document.querySelector("#tabla");
const vacio = document.querySelector("#vacio");

function sesionActiva() {
  return sessionStorage.getItem("sesionSecretaria") === "activa";
}

function mostrarSesion() {
  const activa = sesionActiva();
  acceso.classList.toggle("oculto", activa);
  trabajo.classList.toggle("oculto", !activa);
  if (activa) cargarRegistros();
}

function limpiarErrores() {
  document.querySelectorAll("[data-error]").forEach((nodo) => {
    nodo.textContent = "";
  });
  aviso.className = "aviso";
  aviso.textContent = "";
}

function pintarErrores(errores) {
  limpiarErrores();
  aviso.className = "aviso error-general";
  aviso.textContent = "No se guardó el registro. Revise los campos indicados.";
  errores.forEach((error) => {
    const nodo = document.querySelector(`[data-error="${error.campo}"]`);
    if (nodo) nodo.textContent = error.mensaje;
  });
}

function validar(datos) {
  const errores = [];
  if (!datos.remitente.trim()) errores.push({ campo: "remitente", mensaje: "El remitente es obligatorio." });
  if (!datos.asunto.trim()) errores.push({ campo: "asunto", mensaje: "El asunto es obligatorio." });
  else if (datos.asunto.trim().length > 150) errores.push({ campo: "asunto", mensaje: "El asunto admite como máximo 150 caracteres." });
  if (!datos.tipoDocumento) errores.push({ campo: "tipoDocumento", mensaje: "El tipo de documento es obligatorio." });
  if (!datos.fechaRecepcion) errores.push({ campo: "fechaRecepcion", mensaje: "La fecha de recepción es obligatoria." });
  return errores;
}

function leerFormulario() {
  return {
    remitente: document.querySelector("#remitente").value,
    asunto: asunto.value,
    tipoDocumento: document.querySelector("#tipoDocumento").value,
    fechaRecepcion: document.querySelector("#fechaRecepcion").value
  };
}

async function cargarRegistros() {
  const respuesta = await fetch("/api/correspondencias");
  const registros = await respuesta.json();
  filas.replaceChildren();
  const hay = registros.length > 0;
  tabla.classList.toggle("oculto", !hay);
  vacio.classList.toggle("oculto", hay);
  registros.slice().reverse().forEach((registro) => {
    const fila = document.createElement("tr");
    [registro.correlativo, registro.remitente, registro.asunto, registro.tipoDocumento, registro.fechaRecepcion]
      .forEach((valor) => {
        const celda = document.createElement("td");
        celda.textContent = valor;
        fila.append(celda);
      });
    filas.append(fila);
  });
}

document.querySelector("#entrar").addEventListener("click", () => {
  sessionStorage.setItem("sesionSecretaria", "activa");
  mostrarSesion();
});

document.querySelector("#salir").addEventListener("click", () => {
  sessionStorage.removeItem("sesionSecretaria");
  mostrarSesion();
});

asunto.addEventListener("input", () => {
  cuenta.textContent = String(asunto.value.length);
});

formulario.addEventListener("submit", async (evento) => {
  evento.preventDefault();
  const datos = leerFormulario();
  const errores = validar(datos);
  if (errores.length > 0) {
    pintarErrores(errores);
    return;
  }

  const respuesta = await fetch("/api/correspondencias", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      remitente: datos.remitente.trim(),
      asunto: datos.asunto.trim(),
      tipoDocumento: datos.tipoDocumento,
      fechaRecepcion: datos.fechaRecepcion
    })
  });
  const cuerpo = await respuesta.json();
  if (!respuesta.ok) {
    pintarErrores(cuerpo.errores || []);
    return;
  }

  limpiarErrores();
  aviso.className = "aviso ok";
  aviso.textContent = `${cuerpo.mensaje} Número correlativo ${cuerpo.correlativo}.`;
  formulario.reset();
  cuenta.textContent = "0";
  await cargarRegistros();
});

mostrarSesion();
