using System;
using System.Linq;
using System.Windows.Forms;
using TiendaHibrida.Datos;
using TiendaHibrida.Modelo;

namespace TiendaHibrida.Forms.Productos
{
    // Sirve para CREAR (constructor vacío) y para EDITAR (constructor con el producto).
    public partial class FrmProductoFisico : Form
    {
        private readonly ProductoFisico _existente;

        public FrmProductoFisico()
        {
            InitializeComponent();
        }

        public FrmProductoFisico(ProductoFisico existente) : this()
        {
            _existente = existente;
        }

        private void FrmProductoFisico_Load(object sender, EventArgs e)
        {
            if (_existente == null) return;

            Text = "Editar producto físico";
            txtCodigo.Text = _existente.Codigo;
            txtCodigo.ReadOnly = true;                  // el código identifica al producto
            txtNombre.Text = _existente.Nombre;
            txtDescripcion.Text = _existente.Descripcion;
            txtPrecio.Text = _existente.Precio.ToString();
            txtCategoria.Text = _existente.Categoria;
            txtPeso.Text = _existente.Peso.ToString();
            txtStock.Text = _existente.StockDisponible.ToString();
            txtCostoEnvio.Text = _existente.CostoEnvio.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validacion.Obligatorio(txtCodigo, "Código") ||
                !Validacion.Obligatorio(txtNombre, "Nombre") ||
                !Validacion.Obligatorio(txtCategoria, "Categoría")) return;

            if (!Validacion.TryDecimal(txtPrecio, "Precio", out decimal precio)) return;
            if (!Validacion.TryDouble(txtPeso, "Peso", out double peso)) return;
            if (!Validacion.TryEntero(txtStock, "Stock disponible", out int stock)) return;
            if (!Validacion.TryDecimal(txtCostoEnvio, "Costo de envío", out decimal costoEnvio)) return;

            string codigo = txtCodigo.Text.Trim();

            if (_existente == null &&
                Contexto.Productos.Any(p => string.Equals(p.Codigo, codigo, StringComparison.OrdinalIgnoreCase)))
            {
                Validacion.Aviso("Ya existe un producto con ese código.", txtCodigo);
                return;
            }

            var producto = _existente ?? new ProductoFisico();
            producto.Codigo = codigo;
            producto.Nombre = txtNombre.Text.Trim();
            producto.Descripcion = txtDescripcion.Text.Trim();
            producto.Precio = precio;
            producto.Categoria = txtCategoria.Text.Trim();
            producto.Peso = peso;
            producto.StockDisponible = stock;
            producto.CostoEnvio = costoEnvio;

            // Editar el producto NO altera ventas pasadas: SaleDetail guardó su propia copia.
            if (_existente == null) Contexto.Productos.Add(producto);
            Contexto.GuardarProductos();

            DialogResult = DialogResult.OK;   // cierra el formulario
        }
    }
}
