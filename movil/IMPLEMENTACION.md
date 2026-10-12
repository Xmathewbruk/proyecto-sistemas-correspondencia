# Aplicación móvil de correspondencia

## Implementación

Material 3 con Inicio, Drawer y Nueva correspondencia. El servicio envía un POST real con JSON a la API existente; no genera correlativos ni guarda registros localmente. Las pruebas con MockClient simulan respuestas solamente dentro de test/.

Estructura de lib/:

- main.dart: aplicación, tema institucional y navegación.
- config/api_config.dart: única configuración de URL base.
- models/correspondencia.dart: entrada JSON y resultado del registro.
- pages/home_page.dart: Inicio y acceso al registro.
- pages/registro_correspondencia_page.dart: formulario, calendario, validaciones, carga y mensajes.
- services/correspondencia_api.dart: POST, timeout y manejo de respuestas 201/400 y fallos.

Dependencias añadidas: http y flutter_localizations (SDK Flutter). La localización permite mostrar el calendario en español.

## Backend existente

Se necesita el SDK .NET 10. Desde la raíz del repositorio:

```powershell
dotnet run --project web/Correspondencia.csproj
```

El perfil existente escucha en http://localhost:5080. Para un teléfono físico, el servidor debe escuchar también en la interfaz de red:

```powershell
dotnet run --project web/Correspondencia.csproj --no-launch-profile --urls http://0.0.0.0:5080
```

El teléfono y la computadora deben estar en la misma red, y el firewall debe permitir el puerto 5080 para esa red local. La API continúa guardando en web/data/correspondencias.json. No se modificó el backend.

## Ejecutar Flutter

Desde movil/:

```powershell
flutter pub get
flutter devices
flutter run -d ID_DEL_DISPOSITIVO
```

La URL predeterminada para Android Emulator está centralizada en lib/config/api_config.dart. Para usar un teléfono físico, reemplazar IP_DEL_PC por la dirección local de la computadora:

```powershell
flutter run -d ID_DEL_TELEFONO --dart-define=API_BASE_URL=http://IP_DEL_PC:5080
```

Usar una URL base sin barra final. No es necesario editar el servicio ni la pantalla.

Para generar un APK debug con otra URL:

```powershell
flutter build apk --debug --dart-define=API_BASE_URL=http://IP_DEL_PC:5080
```

## Android y HTTP

El permiso android.permission.INTERNET está en android/app/src/main/AndroidManifest.xml.
El tráfico HTTP sin cifrar está habilitado solo en android/app/src/debug/AndroidManifest.xml mediante usesCleartextTraffic. Permite la API local durante desarrollo, incluso si cambia la IP del PC. Esta excepción no se incluye en release ni profile. Para producción se debe configurar una API HTTPS; no se prepara aquí un APK de producción.

## Flujo del usuario

1. Abrir Inicio y pulsar Nueva correspondencia (también disponible en el menú).
2. Completar Remitente y Asunto (hasta 150 caracteres), elegir Tipo y seleccionar Fecha en el calendario.
3. Pulsar Guardar correspondencia. El botón muestra Guardando... y los campos quedan deshabilitados mientras se envía una sola solicitud.
4. Ante HTTP 201 con respuesta válida, aparece la confirmación y el correlativo recibido del backend. Aceptar cierra el diálogo; el formulario queda vacío para otro registro.
5. Ante errores 400, aparecen los mensajes asociados a los campos. Los datos permanecen para corregirlos.
6. Ante problemas de conexión, aparece un mensaje legible y el formulario conserva sus datos.

La fecha se presenta como dd/MM/yyyy y se envía como yyyy-MM-dd. En timeout o respuesta ilegible el servidor podría haber guardado: comprobar el registro antes de reintentar. No hay reenvíos automáticos.

## Pruebas

```powershell
flutter analyze
flutter test
flutter build apk --debug
```

Las pruebas cubren campos vacíos, asunto largo, navegación, selección de fecha/tipo, envío JSON, doble envío, limpieza, respuesta 201 y correlativo, errores 400, respuesta inesperada, timeout y servidor inaccesible. Las pruebas controladas no sustituyen una prueba Android con el backend real.

Prueba manual real, con backend y Android disponibles:

1. Intentar guardar sin datos y verificar los cuatro mensajes obligatorios.
2. Escribir 151 caracteres en Asunto y verificar que no se envíe.
3. Registrar Junta Vecinal Los Pinos / Solicitud de alumbrado público / Carta / fecha seleccionada.
4. Verificar el correlativo mostrado contra GET /api/correspondencias o el panel web. Esta prueba crea un registro real en el almacenamiento del backend.
5. Detener el servidor, completar otro formulario y verificar el mensaje de conexión sin limpiar los datos.
6. Iniciar el servidor y repetir el envío.

APK debug: build/app/outputs/flutter-apk/app-debug.apk.

El registro fue probado desde la aplicación móvil contra la API real, según la confirmación del autor. Se verificó CORR-0001 tanto en web/data/correspondencias.json como en GET /api/correspondencias (HTTP 200), sin diferencias en los campos almacenados. El autor confirmó que el correlativo mostrado por Flutter coincide con el backend.

Para reproducir la prueba se necesita un dispositivo Android o un emulador con aceleración por hardware habilitada, y el SDK .NET 10 disponible para levantar el backend.
