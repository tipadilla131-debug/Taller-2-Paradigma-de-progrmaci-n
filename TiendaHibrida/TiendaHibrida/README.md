# Tienda Híbrida — Taller 2

Aplicación de escritorio en **.NET 8 + Windows Forms + CsvHelper**: vende productos
físicos y digitales con tres CRUDs (Productos, Clientes, Ventas) persistidos en CSV.

## Cómo abrirlo

1. Descomprime el zip y abre `TiendaHibrida.sln` con Visual Studio 2022
   (carga de trabajo "Desarrollo de escritorio de .NET").
2. Si no tienes el SDK de .NET 8, cambia `<TargetFramework>net8.0-windows</TargetFramework>`
   en `TiendaHibrida.csproj` por tu versión (por ejemplo `net6.0-windows`).
3. F5. El paquete NuGet `CsvHelper` se restaura solo.

Los datos quedan en `bin/Debug/net8.0-windows/Data/` (`productos.csv`, `clientes.csv`,
`ventas.csv`, `saledetails.csv`). **No los abras en Excel mientras la app corre**: Excel bloquea
el archivo y la app no podrá guardar (te lo avisará con un mensaje).

## Estructura

```
TiendaHibrida/
├── Modelo/    Producto (abstracta), ProductoFisico, ProductoDigital, Cliente, Venta, SaleDetail
├── Datos/     Contexto, ArchivoCsv (único lugar que toca CsvHelper), 3 repositorios
│   └── Maps/  Filas planas (DTO) para el CSV
└── Forms/     FrmPrincipal (menú), Validacion
    ├── Productos/  FrmProductoFisico, FrmProductoDigital, FrmCatalogoProductos
    ├── Clientes/   FrmClientes
    └── Ventas/     FrmVenta, FrmHistorialVentas
```

Los formularios están en formato estándar de Visual Studio (`.cs` + `.Designer.cs` + `.resx`):
doble clic sobre cualquiera abre el diseñador visual y el Cuadro de herramientas se llena.

## Dónde se cumple cada criterio del taller

| Criterio | Dónde |
|---|---|
| **Productos (25%)** común / físico / digital y dos formularios | `Modelo/Producto*.cs`, `FrmProductoFisico`, `FrmProductoDigital`, catálogo único en `FrmCatalogoProductos` (crear, listar, **editar**, eliminar) |
| **Clientes (15%)** validaciones y relación con ventas | `FrmClientes`: documento único al crear y al actualizar; no se elimina un cliente con ventas |
| **Ventas (35%)** SaleDetail, stock, total | `Modelo/Venta.cs`, `Modelo/SaleDetail.cs`, `FrmVenta`, `FrmHistorialVentas` |
| **Persistencia (25%)** CsvHelper | `Datos/*Repositorio.cs`, `Datos/ArchivoCsv.cs`, `Datos/Contexto.cs` |

## Decisiones de diseño (las tienes que justificar TÚ en la sustentación)

- **Polimorfismo, sin `if (es físico)` en la venta.** `Producto` define `CalcularCostoAdicional()`,
  `ValidarDisponibilidad()` y `ConfirmarVenta()`; el físico los sobrescribe, el digital no
  (nunca se agota, nunca se envía). `Venta` y `SaleDetail` no saben de qué tipo es cada producto.
- **El stock se descuenta al confirmar la venta, no al agregar el detalle.** Al agregar solo se
  *valida* (contando también lo que ya está en esa venta). Así, cancelar una venta no deja el
  stock alterado. Si no alcanza, el detalle no se agrega.
- **Una venta registrada es inmutable** (`Venta.Confirmada`): no se puede agregar ni quitar
  detalles, y por eso el historial es solo de consulta (no hay editar/eliminar ventas).
- **SaleDetail copia** descripción, precio unitario, envío y cantidad. Por eso eliminar o
  cambiar un producto vendido no altera ventas pasadas.
- **Envío:** se cobra **una vez por línea de detalle**, no por unidad
  (`Subtotal = PrecioUnitario × Cantidad + CostoEnvio`). Si tu profesor espera envío por unidad,
  cambia solo `SaleDetail.Subtotal`.
- **Eliminar un producto vendido:** se permite (el histórico no se rompe, ver arriba).
- **Eliminar un cliente con ventas:** no se permite (sus ventas quedarían sin cliente).
- **Físico y digital en un CSV plano:** columna discriminadora `Tipo` (`Fisico`/`Digital`);
  las columnas que no aplican quedan vacías. Al leer, `Tipo` decide qué clase instanciar
  (`ProductoRepositorio.Cargar`).
- **Reconstruir una venta desde filas sueltas:** `saledetails.csv` lleva `NumeroVenta`
  (llave foránea); al cargar se agrupa con `GroupBy` (`VentaRepositorio.Cargar`).
- **Escritura segura:** cada guardado escribe a un `.tmp` y luego reemplaza el CSV, así un fallo
  a mitad de escritura no lo corrompe.
- **`Contexto` estático:** simplifica el proyecto; en uno más grande se usaría inyección de dependencias.

## Qué se probó y qué no

- Probado con el compilador y ejecutando código: todo el modelo (total, stock al confirmar,
  acumulado por producto, inmutabilidad, digital ilimitado, detalle congelado) y toda la
  persistencia con CsvHelper 33.0.1 real (guardar, releer, discriminador, comas y comillas,
  decimales, reconstrucción de ventas, eliminar producto vendido, archivos vacíos).
- **No se pudo compilar la parte visual** (formularios): requiere el SDK de Windows Desktop.
  Se verificó sintaxis, que cada control usado exista en su `.Designer.cs` y que cada evento
  tenga su método. Si Visual Studio marca algún error al primer build, será menor.
