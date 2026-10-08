# PERT — Iteración 01

Tiempo esperado: TE = (O + 4M + P) / 6

Desviación: ? = (P ? O) / 6

Los tiempos están en horas.

| Act. | EDT | Paquete | Predecesora | O | M | P | TE | ? |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| A | 3.1 | Registro web y API | — | 2 | 3 | 6 | 3.3 | 0.67 |
| B | 3.2 | Registro móvil | A | 3 | 4 | 7 | 4.3 | 0.67 |

- A, registro web: (2 + 4×3 + 6) / 6 = 3.3 h. Lo hace el integrante de la web.
- B, registro móvil: (3 + 4×4 + 7) / 6 = 4.3 h. Lo hace el integrante de la app móvil.
- B no puede empezar hasta que exista `POST /api/correspondencias`.
