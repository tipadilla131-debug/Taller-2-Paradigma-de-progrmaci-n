using System;
using System.Windows.Forms;
using TiendaHibrida.Datos;
using TiendaHibrida.Forms.Clientes;
using TiendaHibrida.Forms.Productos;
using TiendaHibrida.Forms.Ventas;

namespace TiendaHibrida.Forms
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void catalogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var frm = new FrmCatalogoProductos();
            frm.ShowDialog(this);
        }

        // Los formularios de producto se guardan solos (agregan al Contexto y persisten en el CSV).
        private void nuevoFisicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var frm = new FrmProductoFisico();
            frm.ShowDialog(this);
        }

        private void nuevoDigitalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var frm = new FrmProductoDigital();
            frm.ShowDialog(this);
        }

        private void gestionarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var frm = new FrmClientes();
            frm.ShowDialog(this);
        }

        private void nuevaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Contexto.Clientes.Count == 0)
            {
                Validacion.Aviso("Registre al menos un cliente antes de crear una venta.");
                return;
            }
            if (Contexto.Productos.Count == 0)
            {
                Validacion.Aviso("Registre al menos un producto antes de crear una venta.");
                return;
            }

            using var frm = new FrmVenta();
            frm.ShowDialog(this);
        }

        private void historialVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var frm = new FrmHistorialVentas();
            frm.ShowDialog(this);
        }
    }
}
