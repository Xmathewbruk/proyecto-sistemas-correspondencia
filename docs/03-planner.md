# Planner

Un plan para la pareja. Columnas: Backlog, En progreso, En revisión (PR), Hecho.

Las dos tarjetas se asignan a los dos integrantes. Las fechas salen del CPM.

## [3.1] #5 Registro web

- Depósito: En revisión (PR)
- Etiquetas: Fase 3, Web, Ruta crítica
- Inicio: hora 0 (IT de A)
- Vencimiento: hora 3.3 (FT de A)
- Issue: https://github.com/Xmathewbruk/proyecto-sistemas-correspondencia/issues/5
- Pull request: https://github.com/Xmathewbruk/proyecto-sistemas-correspondencia/pull/7
- Rama: `feature/5-web-correspondencia`
- Lista de comprobación:
  - Remitente, asunto, tipo y fecha son obligatorios
  - El asunto admite como máximo 150 caracteres
  - Se guarda con correlativo automático
  - Muestra confirmación al guardar
  - Existe `POST /api/correspondencias`

## [3.2] #6 Registro móvil

- Depósito: Backlog
- Etiquetas: Fase 3, Móvil, Ruta crítica
- Inicio: hora 3.3 (IT de B)
- Vencimiento: hora 7.6 (FT de B)
- Issue: https://github.com/Xmathewbruk/proyecto-sistemas-correspondencia/issues/6
- Rama prevista: `feature/6-movil-correspondencia`
- Se mueve a En progreso cuando el PR de la web esté en `develop`

El nombre de cada tarjeta es `[código EDT] #issue Título`.
