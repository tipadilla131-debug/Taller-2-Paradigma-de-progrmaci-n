using System;
using System.Linq;
using System.Windows.Forms;
using TiendaHibrida.Datos;
using TiendaHibrida.Modelo;

namespace TiendaHibrida.Forms.Productos
{
    // Sirve para CREAR (constructor vacío) y para EDITAR (constructor con el producto).
    public partial class FrmProductoDigital : Form
    {
        private readonly ProductoDigital _existente;

        public FrmProductoDigital()
        {
            InitializeComponent();
        }

        public FrmProductoDigital(ProductoDigital existente) : this()
        {
            _existente = existente;
        }

        private void FrmProductoDigital_Load(object sender, EventArgs e)
        {
            if (_existente == null) return;

            Text = "Editar producto digital";
            txtCodigo.Text = _existente.Codigo;
            txtCodigo.ReadOnly = true;                  // el código identifica al producto
            txtNombre.Text = _existente.Nombre;
            txtDescripcion.Text = _existente.Descripcion;
            txtPrecio.Text = _existente.Precio.ToString();
            txtCategoria.Text = _existente.Categoria;
            txtPeso.Text = _existente.Peso.ToString();
            txtFormato.Text = _existente.Formato;
            txtUrlDescarga.Text = _existente.UrlDescarga;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validacion.Obligatorio(txtCodigo, "Código") ||
                !Validacion.Obligatorio(txtNombre, "Nombre") ||
                !Validacion.Obligatorio(txtCategoria, "Categoría") ||
                !Validacion.Obligatorio(txtFormato, "Formato") ||
                !Validacion.Obligatorio(txtUrlDescarga, "URL de descarga")) return;

            if (!Validacion.TryDecimal(txtPrecio, "Precio", out decimal precio)) return;
            if (!Validacion.TryDouble(txtPeso, "Tamaño de descarga", out double peso)) return;

            if (!Uri.TryCreate(txtUrlDescarga.Text.Trim(), UriKind.Absolute, out _))
            {
                Validacion.Aviso("La URL de descarga no es válida (ej: https://misitio.com/archivo.pdf).", txtUrlDescarga);
                return;
            }

            string codigo = txtCodigo.Text.Trim();

            if (_existente == null &&
                Contexto.Productos.Any(p => string.Equals(p.Codigo, codigo, StringComparison.OrdinalIgnoreCase)))
            {
                Validacion.Aviso("Ya existe un producto con ese código.", txtCodigo);
                return;
            }

            var producto = _existente ?? new ProductoDigital();
            producto.Codigo = codigo;
            producto.Nombre = txtNombre.Text.Trim();
            producto.Descripcion = txtDescripcion.Text.Trim();
            producto.Precio = precio;
            producto.Categoria = txtCategoria.Text.Trim();
            producto.Peso = peso;
            producto.Formato = txtFormato.Text.Trim();
            producto.UrlDescarga = txtUrlDescarga.Text.Trim();

            if (_existente == null) Contexto.Productos.Add(producto);
            Contexto.GuardarProductos();

            DialogResult = DialogResult.OK;   // cierra el formulario
        }
    }
}
