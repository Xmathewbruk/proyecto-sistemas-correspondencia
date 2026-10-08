# CU-01 Registrar correspondencia recibida

- Actor principal: Secretaría.
- Canales: panel web y aplicación móvil.
- Precondición: el usuario inició sesión.
- EDT: 3.1 web y 3.2 móvil.

## Flujo principal

1. La secretaria entra a nueva correspondencia.
2. Completa remitente, asunto, tipo de documento y fecha de recepción.
3. Guarda.
4. El sistema genera el correlativo y confirma el registro.

## Flujo de error

Si falta un dato obligatorio, el sistema muestra el mensaje del campo y no guarda.

## Datos

- Remitente, obligatorio.
- Asunto, obligatorio, máximo 150 caracteres.
- Tipo de documento: Carta, Oficio, Memo, Factura u Otro.
- Fecha de recepción, obligatoria.

## Postcondición

La correspondencia queda registrada con un correlativo `CORR-0001`, `CORR-0002`, y así sucesivamente.

Historias que salen de este caso: HU-01 y HU-02.
