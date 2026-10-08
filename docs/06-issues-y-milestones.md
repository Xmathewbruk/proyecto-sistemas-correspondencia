# Issues y milestone

El issue dice qué hay que hacer. El milestone agrupa la iteración. La rama y el pull request dicen cómo se hace.

| Pieza | Dónde está |
| --- | --- |
| Milestone | Iteración 01 |
| Issue web | #5 Implementar registro de correspondencia (web) |
| Issue móvil | #6 Implementar registro de correspondencia (móvil) |
| Rama de integración | `develop` |
| Rama de producción | `main` |
| Rama de la historia web | `feature/5-web-correspondencia` |
| Pull request | #7 hacia `develop`, con `Closes #5` |

`develop` es la rama por defecto del repositorio, para que al fusionar el pull request se cierre el issue.

Nadie fusiona su propio pull request. El compañero de la app móvil prueba la web, comenta `Probado por` y aprueba.
