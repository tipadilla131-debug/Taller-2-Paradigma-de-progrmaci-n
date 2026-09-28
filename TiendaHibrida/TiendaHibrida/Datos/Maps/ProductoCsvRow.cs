namespace TiendaHibrida.Datos.Maps
{
    // Un CSV es plano: aquí no hay herencia, solo columnas.
    // "Tipo" es el discriminador que dice cuál subclase reconstruir al leer.
    public class ProductoCsvRow
    {
        public string Tipo { get; set; }   // "Fisico" | "Digital"
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        public double Peso { get; set; }

        // Solo aplican si Tipo == "Fisico" (quedan vacías si es Digital)
        public int? StockDisponible { get; set; }
        public decimal? CostoEnvio { get; set; }

        // Solo aplican si Tipo == "Digital"
        public string Formato { get; set; }
        public string UrlDescarga { get; set; }
    }
}
