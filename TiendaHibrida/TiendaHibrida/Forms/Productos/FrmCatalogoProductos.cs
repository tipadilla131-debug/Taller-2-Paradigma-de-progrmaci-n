using System;
using System.Linq;
using System.Windows.Forms;
using TiendaHibrida.Datos;
using TiendaHibrida.Modelo;

namespace TiendaHibrida.Forms.Productos
{
    // Un único catálogo que lista físicos y digitales en la misma grilla.
    public partial class FrmCatalogoProductos : Form
    {
        public FrmCatalogoProductos()
        {
            InitializeComponent();
            dgvProductos.AutoGenerateColumns = false;   // las columnas están definidas en el diseñador
        }

        private void FrmCatalogoProductos_Load(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = Contexto.Productos.ToList();
        }

        private Producto ProductoSeleccionado() => dgvProductos.CurrentRow?.DataBoundItem as Producto;

        private void btnNuevoFisico_Click(object sender, EventArgs e)
        {
            using var frm = new FrmProductoFisico();
            if (frm.ShowDialog(this) == DialogResult.OK) RefrescarGrilla();
        }

        private void btnNuevoDigital_Click(object sender, EventArgs e)
        {
            using var frm = new FrmProductoDigital();
            if (frm.ShowDialog(this) == DialogResult.OK) RefrescarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var seleccionado = ProductoSeleccionado();
            if (seleccionado == null)
            {
                Validacion.Aviso("Seleccione un producto de la lista.");
                return;
            }

            // Cada tipo tiene su propio formulario, así que aquí sí hay que elegir cuál abrir.
            Form frm = seleccionado is ProductoFisico fisico
                ? new FrmProductoFisico(fisico)
                : new FrmProductoDigital((ProductoDigital)seleccionado);

            using (frm)
            {
                if (frm.ShowDialog(this) == DialogResult.OK) RefrescarGrilla();
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEditar_Click(sender, EventArgs.Empty);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var seleccionado = ProductoSeleccionado();
            if (seleccionado == null)
            {
                Validacion.Aviso("Seleccione un producto de la lista.");
                return;
            }

            // Respuesta a "¿qué pasa si eliminan un producto ya vendido?": no pasa nada con el
            // histórico, porque cada SaleDetail guardó su propia copia de descripción y precio.
            var respuesta = MessageBox.Show(
                $"¿Eliminar '{seleccionado.Nombre}' del catálogo?\n\nLas ventas ya registradas no se verán afectadas.",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return;

            Contexto.Productos.Remove(seleccionado);
            Contexto.GuardarProductos();
            RefrescarGrilla();
        }
    }
}
