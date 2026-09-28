using System.Windows.Forms;

namespace TiendaHibrida.Forms
{
    // Validaciones de formulario reutilizadas por todas las pantallas.
    internal static class Validacion
    {
        public static void Aviso(string mensaje, Control foco = null)
        {
            MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            foco?.Focus();
        }

        public static bool Obligatorio(TextBox caja, string campo)
        {
            if (string.IsNullOrWhiteSpace(caja.Text))
            {
                Aviso($"El campo '{campo}' es obligatorio.", caja);
                return false;
            }
            return true;
        }

        public static bool TryDecimal(TextBox caja, string campo, out decimal valor)
        {
            if (!decimal.TryParse(caja.Text.Trim(), out valor) || valor < 0)
            {
                Aviso($"'{campo}' debe ser un número mayor o igual a cero.", caja);
                return false;
            }
            return true;
        }

        public static bool TryDouble(TextBox caja, string campo, out double valor)
        {
            if (!double.TryParse(caja.Text.Trim(), out valor) || valor < 0)
            {
                Aviso($"'{campo}' debe ser un número mayor o igual a cero.", caja);
                return false;
            }
            return true;
        }

        public static bool TryEntero(TextBox caja, string campo, out int valor)
        {
            if (!int.TryParse(caja.Text.Trim(), out valor) || valor < 0)
            {
                Aviso($"'{campo}' debe ser un número entero mayor o igual a cero.", caja);
                return false;
            }
            return true;
        }
    }
}
