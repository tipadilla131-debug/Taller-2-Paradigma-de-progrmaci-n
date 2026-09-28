namespace TiendaHibrida.Modelo
{
    // Lo COMÚN entre físico y digital vive acá. Lo propio de cada uno, no.
    public abstract class Producto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        public double Peso { get; set; } // físico: kg | digital: tamaño de descarga (MB)

        // Cada subclase se describe a sí misma: nadie de afuera pregunta "¿qué tipo eres?".
        public abstract string Tipo { get; }        // "Fisico" | "Digital" (también es el discriminador del CSV)
        public abstract string Detalle { get; }     // texto para mostrar en la descripción del producto en la venta. Físico: stock y peso. Digital: tamaño de descarga.

        // Envío (físico) o 0 (digital). La venta lo llama sin saber el tipo.
        public abstract decimal CalcularCostoAdicional();

        // Físico: lanza excepción si no alcanza el stock. Digital: nunca falla.
        public virtual void ValidarDisponibilidad(int cantidadSolicitada) { }

        // Se llama SOLO cuando la venta se confirma. Físico: descuenta stock. Digital: no hace nada.
        public virtual void ConfirmarVenta(int cantidad) { }

        // Lo que se "congela" en el SaleDetail el día de la venta.
        public virtual string ObtenerDescripcionParaVenta() => $"[{Codigo}] {Nombre} ({Categoria})";

        public override string ToString() => $"{Codigo} - {Nombre}";
    }
}
