# Historias de usuario

Vienen del caso de uso CU-01.

## HU-01 · Web · EDT 3.1

Como secretaria quiero registrar la correspondencia recibida desde el panel web para llevar el control centralizado de los documentos que ingresan.

Criterios de aceptación:

- Dado que inicié sesión, cuando completo remitente, asunto, tipo y fecha y guardo, entonces el sistema guarda la correspondencia con un correlativo y muestra confirmación.
- Dado que un campo obligatorio está vacío, cuando intento guardar, entonces veo el mensaje de ese campo y el registro no se guarda.
- El asunto admite como máximo 150 caracteres.

Issue #5. Rama `feature/5-web-correspondencia`. Responsable: integrante web.

## HU-02 · Móvil · EDT 3.2

Como secretaria quiero registrar la correspondencia desde la aplicación móvil para ingresar datos sin depender del escritorio.

Criterios de aceptación:

- Usa el mismo `POST /api/correspondencias`.
- Exige los mismos campos y el mismo máximo de 150 caracteres en el asunto.
- Muestra el correlativo cuando el registro se guarda.
- Si falta un dato, muestra el error y no guarda.

Issue #6. Rama `feature/6-movil-correspondencia`. Responsable: integrante móvil. Depende de HU-01.
