using System;

namespace TiendaHibrida.Datos.Maps
{
    public class VentaCsvRow
    {
        public int NumeroVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string DocumentoCliente { get; set; } // referencia al cliente por su documento
    }
}
