namespace TiendaHibrida.Modelo
{
    public class ProductoDigital : Producto
    {
        public string Formato { get; set; }       // "PDF", "MP3", "EPUB"...
        public string UrlDescarga { get; set; }

        public override string Tipo => "Digital";
        public override string Detalle => $"Formato: {Formato} | {UrlDescarga}";

        public override decimal CalcularCostoAdicional() => 0m; // no se envía, se descarga

        // ValidarDisponibilidad y ConfirmarVenta heredan el comportamiento vacío:
        // un producto digital nunca se agota.
    }
}
