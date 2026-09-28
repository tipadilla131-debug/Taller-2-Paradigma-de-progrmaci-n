using System;

namespace TiendaHibrida.Modelo
{
    public class ProductoFisico : Producto
    {
        public int StockDisponible { get; set; }
        public decimal CostoEnvio { get; set; }

        public override string Tipo => "Fisico";
        public override string Detalle => $"Stock: {StockDisponible} | Envío: {CostoEnvio:C2}";

        public override decimal CalcularCostoAdicional() => CostoEnvio;

        public override void ValidarDisponibilidad(int cantidadSolicitada)
        {
            if (cantidadSolicitada > StockDisponible)
                throw new InvalidOperationException(
                    $"Stock insuficiente para '{Nombre}'. Disponible: {StockDisponible}, solicitado en esta venta: {cantidadSolicitada}.");
        }

        public override void ConfirmarVenta(int cantidad)
        {
            ValidarDisponibilidad(cantidad);
            StockDisponible -= cantidad;
        }
    }
}
