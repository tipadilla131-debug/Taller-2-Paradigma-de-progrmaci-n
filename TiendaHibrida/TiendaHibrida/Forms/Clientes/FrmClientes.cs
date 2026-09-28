using System;
using System.Linq;
using System.Windows.Forms;
using TiendaHibrida.Datos;
using TiendaHibrida.Modelo;

namespace TiendaHibrida.Forms.Clientes
{
    public partial class FrmClientes : Form
    {
        private bool _cargandoGrilla;

        public FrmClientes()
        {
            InitializeComponent();
            dgvClientes.AutoGenerateColumns = false;   // las columnas están definidas en el diseñador
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            _cargandoGrilla = true;
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = Contexto.Clientes.ToList();
            dgvClientes.ClearSelection();
            dgvClientes.CurrentCell = null;
            _cargandoGrilla = false;
        }

        private Cliente ClienteSeleccionado() => dgvClientes.CurrentRow?.DataBoundItem as Cliente;

        private void LimpiarCampos()
        {
            txtDocumento.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtDocumento.Focus();
        }

        // Valida los cuatro campos; devuelve false (y avisa) si algo está mal.
        private bool LeerFormulario(out string documento, out string nombre, out string correo, out string telefono)
        {
            documento = txtDocumento.Text.Trim();
            nombre = txtNombre.Text.Trim();
            correo = txtCorreo.Text.Trim();
            telefono = txtTelefono.Text.Trim();

            if (!Validacion.Obligatorio(txtDocumento, "Documento") ||
                !Validacion.Obligatorio(txtNombre, "Nombre") ||
                !Validacion.Obligatorio(txtCorreo, "Correo") ||
                !Validacion.Obligatorio(txtTelefono, "Teléfono")) return false;

            if (!new Cliente { Correo = correo }.EsCorreoValido())
            {
                Validacion.Aviso("El correo no tiene un formato válido (ej: nombre@dominio.com).", txtCorreo);
                return false;
            }
            return true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!LeerFormulario(out string documento, out string nombre, out string correo, out string telefono)) return;

            // Regla: el documento no se repite (al CREAR).
            if (Contexto.Clientes.Any(c => string.Equals(c.Documento, documento, StringComparison.OrdinalIgnoreCase)))
            {
                Validacion.Aviso("Ya existe un cliente con ese documento.", txtDocumento);
                return;
            }

            Contexto.Clientes.Add(new Cliente
            {
                Documento = documento,
                Nombre = nombre,
                Correo = correo,
                Telefono = telefono
            });
            Contexto.GuardarClientes();

            RefrescarGrilla();
            LimpiarCampos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            var seleccionado = ClienteSeleccionado();
            if (seleccionado == null)
            {
                Validacion.Aviso("Seleccione un cliente de la lista.");
                return;
            }

            if (!LeerFormulario(out string documento, out string nombre, out string correo, out string telefono)) return;

            // Regla: el documento no se repite (al ACTUALIZAR, ignorando al propio cliente).
            if (Contexto.Clientes.Any(c => !ReferenceEquals(c, seleccionado) &&
                string.Equals(c.Documento, documento, StringComparison.OrdinalIgnoreCase)))
            {
                Validacion.Aviso("Ese documento ya pertenece a otro cliente.", txtDocumento);
                return;
            }

            seleccionado.Documento = documento;
            seleccionado.Nombre = nombre;
            seleccionado.Correo = correo;
            seleccionado.Telefono = telefono;

            Contexto.GuardarClientes();
            // ventas.csv enlaza a cada cliente por su documento: si el documento cambió,
            // hay que reescribirlo para que las ventas sigan encontrando a su cliente.
            Contexto.GuardarVentas();

            RefrescarGrilla();
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var seleccionado = ClienteSeleccionado();
            if (seleccionado == null)
            {
                Validacion.Aviso("Seleccione un cliente de la lista.");
                return;
            }

            // Decisión de diseño: NO se elimina un cliente que ya compró. Sus ventas son un
            // registro histórico y quedarían sin cliente. (Alternativa válida: "soft delete".)
            if (Contexto.Ventas.Any(v => ReferenceEquals(v.Cliente, seleccionado)))
            {
                MessageBox.Show(
                    "No se puede eliminar: este cliente tiene ventas registradas.\n\n" +
                    "Sus ventas son un registro histórico y quedarían sin cliente.",
                    "Eliminación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var respuesta = MessageBox.Show($"¿Eliminar al cliente '{seleccionado.Nombre}'?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            Contexto.Clientes.Remove(seleccionado);
            Contexto.GuardarClientes();

            RefrescarGrilla();
            LimpiarCampos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgvClientes.ClearSelection();
            dgvClientes.CurrentCell = null;
            LimpiarCampos();
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargandoGrilla) return;

            var seleccionado = ClienteSeleccionado();
            if (seleccionado == null) return;

            txtDocumento.Text = seleccionado.Documento;
            txtNombre.Text = seleccionado.Nombre;
            txtCorreo.Text = seleccionado.Correo;
            txtTelefono.Text = seleccionado.Telefono;
        }
    }
}
