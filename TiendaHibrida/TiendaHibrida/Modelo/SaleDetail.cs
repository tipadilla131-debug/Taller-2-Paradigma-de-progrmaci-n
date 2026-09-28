namespace TiendaHibrida.Modelo
{
    // Copia INMUTABLE de lo que se vendió, tal como estaba ese día.
    // No apunta al Producto: si mañana cambian el precio, el nombre o lo eliminan,
    // la venta histórica no se entera.
    public class SaleDetail
    {
        public string DescripcionProducto { get; }
        public decimal PrecioUnitario { get; }
        public decimal CostoEnvio { get; }   // envío de esta línea (0 para digitales)
        public int Cantidad { get; }
        public decimal Subtotal => PrecioUnitario * Cantidad + CostoEnvio;

        public SaleDetail(string descripcionProducto, decimal precioUnitario, decimal costoEnvio, int cantidad)
        {
            DescripcionProducto = descripcionProducto;
            PrecioUnitario = precioUnitario;
            CostoEnvio = costoEnvio;
            Cantidad = cantidad;
        }

        // Nótese que aquí NO hay ningún "if (producto es físico)": todo es polimorfismo.
        public static SaleDetail DesdeProducto(Producto producto, int cantidad)
        {
            return new SaleDetail(
                producto.ObtenerDescripcionParaVenta(),
                producto.Precio,
                producto.CalcularCostoAdicional(),
                cantidad);
        }
    }
}
