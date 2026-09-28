namespace TiendaHibrida.Datos.Maps
{
    public class SaleDetailCsvRow
    {
        public int NumeroVenta { get; set; }        // llave foránea hacia ventas.csv
        public string DescripcionProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal CostoEnvio { get; set; }
        public int Cantidad { get; set; }
    }
}
