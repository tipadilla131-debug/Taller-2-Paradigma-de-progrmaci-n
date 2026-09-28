using System;
using System.Linq;
using System.Windows.Forms;
using TiendaHibrida.Datos;
using TiendaHibrida.Modelo;

namespace TiendaHibrida.Forms.Ventas
{
    public partial class FrmVenta : Form
    {
        private Venta _venta;

        public FrmVenta()
        {
            InitializeComponent();
            dgvDetalles.AutoGenerateColumns = false;   // las columnas están definidas en el diseñador
        }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            _venta = new Venta
            {
                NumeroVenta = Contexto.SiguienteNumeroVenta(),
                Fecha = DateTime.Now
            };
            lblEncabezado.Text = $"Venta N° {_venta.NumeroVenta}  —  {_venta.Fecha:dd/MM/yyyy HH:mm}";

            cmbClientes.DataSource = Contexto.Clientes.ToList();
            cmbProductos.DataSource = Contexto.Productos.ToList();

            RefrescarDetalles();
        }

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cada producto se describe solo (Detalle), el formulario no pregunta su tipo.
            lblInfoProducto.Text = cmbProductos.SelectedItem is Producto p
                ? $"{p.Tipo}  |  Precio: {p.Precio:C2}  |  {p.Detalle}"
                : "";
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            if (!(cmbProductos.SelectedItem is Producto producto))
            {
                Validacion.Aviso("Seleccione un producto.");
                return;
            }

            int cantidad = (int)nudCantidad.Value;

            try
            {
                // La venta valida el stock por su cuenta (polimorfismo): el formulario no distingue
                // entre físico y digital. Si no alcanza, lanza excepción y el detalle NO se agrega.
                _venta.AgregarDetalle(producto, cantidad);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            nudCantidad.Value = 1;
            RefrescarDetalles();
        }

        private void btnQuitarDetalle_Click(object sender, EventArgs e)
        {
            if (dgvDetalles.CurrentRow == null)
            {
                Validacion.Aviso("Seleccione el detalle que quiere quitar.");
                return;
            }

            _venta.QuitarDetalle(dgvDetalles.CurrentRow.Index);
            RefrescarDetalles();
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            _venta.Cliente = cmbClientes.SelectedItem as Cliente;

            if (_venta.Cliente == null)
            {
                Validacion.Aviso("Seleccione un cliente.", cmbClientes);
                return;
            }
            if (!_venta.EsValida())
            {
                Validacion.Aviso("Una venta sin productos no se puede guardar. Agregue al menos uno.");
                return;
            }

            try
            {
                // Aquí (y no antes) se descuenta el stock de los físicos y la venta queda inmutable.
                _venta.Confirmar();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "No se pudo registrar la venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Contexto.Ventas.Add(_venta);
            Contexto.GuardarVentas();
            Contexto.GuardarProductos();   // el stock de los físicos cambió

            MessageBox.Show($"Venta N° {_venta.NumeroVenta} registrada.\nTotal: {_venta.Total:C2}",
                "Venta guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_venta != null && _venta.Detalles.Count > 0)
            {
                var respuesta = MessageBox.Show("¿Descartar esta venta? Se perderán los productos agregados.",
                    "Cancelar venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta != DialogResult.Yes) return;
            }

            // No se tocó ningún stock ni archivo: el stock solo cambia al confirmar.
            DialogResult = DialogResult.Cancel;
        }

        private void RefrescarDetalles()
        {
            dgvDetalles.DataSource = null;
            dgvDetalles.DataSource = _venta.Detalles.ToList();
            lblTotal.Text = $"Total: {_venta.Total:C2}";   // el total lo calcula la Venta, no la pantalla
        }
    }
}
