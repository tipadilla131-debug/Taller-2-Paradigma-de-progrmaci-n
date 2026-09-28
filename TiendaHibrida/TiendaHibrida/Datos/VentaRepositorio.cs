using System.Collections.Generic;
using System.Linq;
using TiendaHibrida.Datos.Maps;
using TiendaHibrida.Modelo;

namespace TiendaHibrida.Datos
{
    public class VentaRepositorio
    {
        private readonly string _rutaVentas;
        private readonly string _rutaDetalles;

        public VentaRepositorio(string rutaVentas, string rutaDetalles)
        {
            _rutaVentas = rutaVentas;
            _rutaDetalles = rutaDetalles;
        }

        public List<Venta> Cargar(IEnumerable<Cliente> clientes)
        {
            var filasVenta = ArchivoCsv.Leer<VentaCsvRow>(_rutaVentas);
            var filasDetalle = ArchivoCsv.Leer<SaleDetailCsvRow>(_rutaDetalles);

            // El CSV de detalles son filas sueltas: se agrupan por NumeroVenta
            // (la "llave foránea") para reconstruir cada venta completa.
            var detallesPorVenta = filasDetalle
                .GroupBy(d => d.NumeroVenta)
                .ToDictionary(g => g.Key, g => g.ToList());

            var clientesPorDocumento = clientes
                .GroupBy(c => c.Documento)
                .ToDictionary(g => g.Key, g => g.First());

            var ventas = new List<Venta>();

            foreach (var fila in filasVenta)
            {
                var venta = new Venta
                {
                    NumeroVenta = fila.NumeroVenta,
                    Fecha = fila.Fecha,
                    Cliente = fila.DocumentoCliente != null &&
                              clientesPorDocumento.TryGetValue(fila.DocumentoCliente, out var cliente)
                                  ? cliente : null
                };

                if (detallesPorVenta.TryGetValue(fila.NumeroVenta, out var detalles))
                {
                    foreach (var d in detalles)
                    {
                        // Se respeta lo guardado tal cual: NO se recalcula con el catálogo actual.
                        venta.CargarDetalleHistorico(
                            new SaleDetail(d.DescripcionProducto, d.PrecioUnitario, d.CostoEnvio, d.Cantidad));
                    }
                }

                venta.MarcarComoHistorica();
                ventas.Add(venta);
            }

            return ventas;
        }

        public void Guardar(IEnumerable<Venta> ventas)
        {
            var lista = ventas.ToList();

            var filasVenta = lista.Select(v => new VentaCsvRow
            {
                NumeroVenta = v.NumeroVenta,
                Fecha = v.Fecha,
                DocumentoCliente = v.Cliente?.Documento
            }).ToList();

            var filasDetalle = lista.SelectMany(v => v.Detalles.Select(d => new SaleDetailCsvRow
            {
                NumeroVenta = v.NumeroVenta,
                DescripcionProducto = d.DescripcionProducto,
                PrecioUnitario = d.PrecioUnitario,
                CostoEnvio = d.CostoEnvio,
                Cantidad = d.Cantidad
            })).ToList();

            ArchivoCsv.Escribir(_rutaVentas, filasVenta);
            ArchivoCsv.Escribir(_rutaDetalles, filasDetalle);
        }
    }
}
