# CPM — Ruta crítica

Duración de A = 3.3 h. Duración de B = 4.3 h. B depende de A.

| Act. | Duración | IT | FT | IL | FL | Holgura |
| --- | --- | --- | --- | --- | --- | --- |
| A Registro web | 3.3 | 0 | 3.3 | 0 | 3.3 | 0 |
| B Registro móvil | 4.3 | 3.3 | 7.6 | 3.3 | 7.6 | 0 |

IT y FT son el inicio y el fin más temprano. IL y FL son el inicio y el fin más tardío. Holgura = IL ? IT.

Ruta crítica: A ? B. Dura 7.6 h. Las dos actividades tienen holgura 0: si la web se atrasa, la entrega móvil también se atrasa.

En Planner, la web usa inicio 0 y vencimiento 3.3. La móvil usa inicio 3.3 y vencimiento 7.6.
