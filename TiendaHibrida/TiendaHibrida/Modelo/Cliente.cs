using System.Text.RegularExpressions;

namespace TiendaHibrida.Modelo
{
    public class Cliente
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        public bool EsCorreoValido()
        {
            if (string.IsNullOrWhiteSpace(Correo)) return false;
            return Regex.IsMatch(Correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public override string ToString() => $"{Documento} - {Nombre}";
    }
}
