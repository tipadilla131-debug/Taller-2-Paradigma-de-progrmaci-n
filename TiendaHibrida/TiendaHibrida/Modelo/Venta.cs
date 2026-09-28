using System;
using System.Collections.Generic;
using System.Linq;

namespace TiendaHibrida.Modelo
{
    public class Venta
    {
        private readonly List<SaleDetail> _detalles = new List<SaleDetail>();
        // Producto y cantidad de cada línea, para descontar stock SOLO al confirmar.
        private readonly List<(Producto Prod, int Cant)> _pendientes = new List<(Producto Prod, int Cant)>();

        public int NumeroVenta { get; set; }
        public DateTime Fecha { get; set; }
        public Cliente Cliente { get; set; }
        public bool Confirmada { get; private set; }

        public IReadOnlyList<SaleDetail> Detalles => _detalles;

        // El total lo calcula la venta, nunca la pantalla.
        public decimal Total => _detalles.Sum(d => d.Subtotal);

        public string NombreCliente => Cliente?.Nombre ?? "(cliente no encontrado)";

        public void AgregarDetalle(Producto producto, int cantidad)
        {
            AsegurarEditable();
            if (producto == null) throw new ArgumentNullException(nameof(producto));
            if (cantidad <= 0) throw new ArgumentException("La cantidad debe ser mayor que cero.");

            // Cuenta lo que ya hay de este producto en ESTA venta: dos líneas del mismo
            // físico no pueden, sumadas, superar el stock.
            int yaEnVenta = _pendientes.Where(p => ReferenceEquals(p.Prod, producto)).Sum(p => p.Cant);

            // Si no alcanza, lanza InvalidOperationException y el detalle NO se agrega.
            producto.ValidarDisponibilidad(yaEnVenta + cantidad);

            _detalles.Add(SaleDetail.DesdeProducto(producto, cantidad));
            _pendientes.Add((producto, cantidad));
        }

        public void QuitarDetalle(int indice)
        {
            AsegurarEditable();
            if (indice < 0 || indice >= _detalles.Count) return;
            _detalles.RemoveAt(indice);
            _pendientes.RemoveAt(indice);
        }

        // Una venta sin detalles (o sin cliente) no se guarda.
        public bool EsValida() => Cliente != null && _detalles.Count > 0;

        // Registra la venta: descuenta el stock de los físicos y la vuelve inmutable.
        public void Confirmar()
        {
            AsegurarEditable();
            if (!EsValida())
                throw new InvalidOperationException("La venta necesita un cliente y al menos un producto.");

            // Revalida el total por producto antes de tocar ningún stock.
            foreach (var grupo in _pendientes.GroupBy(p => p.Prod))
                grupo.Key.ValidarDisponibilidad(grupo.Sum(p => p.Cant));

            foreach (var (prod, cant) in _pendientes)
                prod.ConfirmarVenta(cant);

            _pendientes.Clear();
            Confirmada = true;
        }

        // Usados por el repositorio al reconstruir ventas históricas desde el CSV.
        public void CargarDetalleHistorico(SaleDetail detalle) => _detalles.Add(detalle);
        public void MarcarComoHistorica() => Confirmada = true;

        private void AsegurarEditable()
        {
            if (Confirmada)
                throw new InvalidOperationException("Una venta registrada no se puede modificar.");
        }
    }
}
