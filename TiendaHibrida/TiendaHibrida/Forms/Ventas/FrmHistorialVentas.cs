using System;
using System.Linq;
using System.Windows.Forms;
using TiendaHibrida.Datos;
using TiendaHibrida.Modelo;

namespace TiendaHibrida.Forms.Ventas
{
    // Consulta de ventas. No hay editar ni eliminar: una venta registrada es un
    // registro histórico y nada de afuera puede modificarla.
    public partial class FrmHistorialVentas : Form
    {
        public FrmHistorialVentas()
        {
            InitializeComponent();
            dgvVentas.AutoGenerateColumns = false;
            dgvDetalles.AutoGenerateColumns = false;
        }

        private void FrmHistorialVentas_Load(object sender, EventArgs e)
        {
            dgvVentas.DataSource = Contexto.Ventas.OrderByDescending(v => v.NumeroVenta).ToList();
            MostrarDetalles();
        }

        private void dgvVentas_SelectionChanged(object sender, EventArgs e)
        {
            MostrarDetalles();
        }

        private void MostrarDetalles()
        {
            var venta = dgvVentas.CurrentRow?.DataBoundItem as Venta;
            dgvDetalles.DataSource = venta?.Detalles.ToList();
        }
    }
}
