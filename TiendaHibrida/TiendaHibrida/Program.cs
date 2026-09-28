using System;
using System.Windows.Forms;
using TiendaHibrida.Datos;
using TiendaHibrida.Forms;

namespace TiendaHibrida
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Debe ir antes de crear cualquier formulario.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
                MessageBox.Show(
                    "Ocurrió un error inesperado:\n\n" + e.Exception.Message +
                    "\n\nSi el problema es al guardar, verifica que los archivos .csv no estén abiertos en Excel.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Contexto.CargarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los archivos de datos:\n\n" + ex.Message,
                    "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new FrmPrincipal());
        }
    }
}
