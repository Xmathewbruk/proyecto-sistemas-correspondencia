# Registro de correspondencia recibida

Proyecto de Sistemas. Caso de uso CU-01: la secretaría registra la correspondencia que ingresa, desde el panel web o desde la aplicación móvil, usando el mismo endpoint.

## Cómo ejecutarlo

Requisito: .NET 10.

```bash
dotnet run
```

Abrir http://localhost:5080

- Panel web: http://localhost:5080/web/

La pantalla móvil la implementa el otro integrante, en la rama `feature/6-movil-correspondencia`, después de que este pull request quede integrado en `develop`.

En ambas pantallas hay que ingresar como secretaría antes de registrar.

## Trazabilidad

Estructura de Descomposición del Trabajo ? Tarjeta en Planner ? Issue en GitHub ? Rama en Git ? Commits ? Pull Request ? Prueba por un compañero ? Integración en `develop`.

| EDT | Planner | Issue | Rama |
| --- | --- | --- | --- |
| 3.1 | [3.1] #5 Registro web | #5 | `feature/5-web-correspondencia` |
| 3.2 | [3.2] #6 Registro móvil | #6 | `feature/6-movil-correspondencia` |

Las ramas salen de `develop`. El pull request apunta a `develop` e incluye `Closes #N`. La integración es squash and merge.

## Estimación PERT

Tiempo esperado = (optimista + 4 × probable + pesimista) / 6.

| Tarea | Optimista | Probable | Pesimista | Esperado | Dependencia |
| --- | --- | --- | --- | --- | --- |
| Desarrollo web (API) | 2 h | 3 h | 6 h | 3.3 h | Ninguna |
| Desarrollo móvil | 3 h | 4 h | 7 h | 4.3 h | Depende de la API web |

La ruta crítica es el desarrollo web seguido del desarrollo móvil.

## API

`POST /api/correspondencias`

```json
{
  "remitente": "Municipalidad",
  "asunto": "Solicitud de informe",
  "tipoDocumento": "Oficio",
  "fechaRecepcion": "2026-10-08"
}
```

Tipos válidos: Carta, Oficio, Memo, Factura, Otro. El asunto admite como máximo 150 caracteres. Si falta un dato obligatorio, la API responde 400 y no guarda el registro. Si el registro es válido, responde 201 con el correlativo automático `CORR-0001`.

`GET /api/correspondencias` devuelve los registros guardados.
