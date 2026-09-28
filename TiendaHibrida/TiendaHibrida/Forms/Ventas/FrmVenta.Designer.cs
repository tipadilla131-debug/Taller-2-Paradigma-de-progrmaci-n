namespace TiendaHibrida.Forms.Ventas
{
    partial class FrmVenta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblEncabezado = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cmbProductos = new System.Windows.Forms.ComboBox();
            this.lblInfoProducto = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnAgregarDetalle = new System.Windows.Forms.Button();
            this.btnQuitarDetalle = new System.Windows.Forms.Button();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnvio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnGuardarVenta = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEncabezado
            // 
            this.lblEncabezado.AutoSize = true;
            this.lblEncabezado.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEncabezado.Location = new System.Drawing.Point(20, 14);
            this.lblEncabezado.Size = new System.Drawing.Size(200, 21);
            this.lblEncabezado.Text = "Venta";
            this.lblEncabezado.Name = "lblEncabezado";
            this.lblEncabezado.TabIndex = 0;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(20, 58);
            this.lblCliente.Size = new System.Drawing.Size(62, 15);
            this.lblCliente.Text = "Cliente:";
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.TabIndex = 1;
            // 
            // cmbClientes
            // 
            this.cmbClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClientes.FormattingEnabled = true;
            this.cmbClientes.Location = new System.Drawing.Point(120, 55);
            this.cmbClientes.Size = new System.Drawing.Size(380, 23);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.TabIndex = 2;
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new System.Drawing.Point(20, 95);
            this.lblProducto.Size = new System.Drawing.Size(69, 15);
            this.lblProducto.Text = "Producto:";
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.TabIndex = 3;
            // 
            // cmbProductos
            // 
            this.cmbProductos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductos.FormattingEnabled = true;
            this.cmbProductos.Location = new System.Drawing.Point(120, 92);
            this.cmbProductos.Size = new System.Drawing.Size(380, 23);
            this.cmbProductos.Name = "cmbProductos";
            this.cmbProductos.TabIndex = 4;
            this.cmbProductos.SelectedIndexChanged += new System.EventHandler(this.cmbProductos_SelectedIndexChanged);
            // 
            // lblInfoProducto
            // 
            this.lblInfoProducto.AutoSize = true;
            this.lblInfoProducto.ForeColor = System.Drawing.Color.DimGray;
            this.lblInfoProducto.Location = new System.Drawing.Point(120, 121);
            this.lblInfoProducto.Size = new System.Drawing.Size(10, 15);
            this.lblInfoProducto.Text = "";
            this.lblInfoProducto.Name = "lblInfoProducto";
            this.lblInfoProducto.TabIndex = 5;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(20, 152);
            this.lblCantidad.Size = new System.Drawing.Size(69, 15);
            this.lblCantidad.Text = "Cantidad:";
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.TabIndex = 6;
            // 
            // nudCantidad
            // 
            this.nudCantidad.Location = new System.Drawing.Point(120, 149);
            this.nudCantidad.Size = new System.Drawing.Size(80, 23);
            this.nudCantidad.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.TabIndex = 7;
            // 
            // btnAgregarDetalle
            // 
            this.btnAgregarDetalle.Location = new System.Drawing.Point(215, 145);
            this.btnAgregarDetalle.Size = new System.Drawing.Size(110, 32);
            this.btnAgregarDetalle.Text = "Agregar";
            this.btnAgregarDetalle.UseVisualStyleBackColor = true;
            this.btnAgregarDetalle.Name = "btnAgregarDetalle";
            this.btnAgregarDetalle.TabIndex = 8;
            this.btnAgregarDetalle.Click += new System.EventHandler(this.btnAgregarDetalle_Click);
            // 
            // btnQuitarDetalle
            // 
            this.btnQuitarDetalle.Location = new System.Drawing.Point(335, 145);
            this.btnQuitarDetalle.Size = new System.Drawing.Size(165, 32);
            this.btnQuitarDetalle.Text = "Quitar seleccionado";
            this.btnQuitarDetalle.UseVisualStyleBackColor = true;
            this.btnQuitarDetalle.Name = "btnQuitarDetalle";
            this.btnQuitarDetalle.TabIndex = 9;
            this.btnQuitarDetalle.Click += new System.EventHandler(this.btnQuitarDetalle_Click);
            // 
            // dgvDetalles
            // 
            this.dgvDetalles.Location = new System.Drawing.Point(12, 190);
            this.dgvDetalles.Size = new System.Drawing.Size(696, 260);
            this.dgvDetalles.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvDetalles.Name = "dgvDetalles";
            this.dgvDetalles.TabIndex = 10;
            this.dgvDetalles.AllowUserToAddRows = false;
            this.dgvDetalles.AllowUserToDeleteRows = false;
            this.dgvDetalles.AllowUserToResizeRows = false;
            this.dgvDetalles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colDescripcion, this.colPrecioUnitario, this.colCantidad, this.colEnvio, this.colSubtotal });
            this.dgvDetalles.MultiSelect = false;
            this.dgvDetalles.ReadOnly = true;
            this.dgvDetalles.RowHeadersVisible = false;
            this.dgvDetalles.RowTemplate.Height = 25;
            this.dgvDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // colDescripcion
            // 
            this.colDescripcion.DataPropertyName = "DescripcionProducto";
            this.colDescripcion.FillWeight = 220F;
            this.colDescripcion.HeaderText = "Producto";
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.ReadOnly = true;
            // 
            // colPrecioUnitario
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "C2";
            this.colPrecioUnitario.DefaultCellStyle = dataGridViewCellStyle1;
            this.colPrecioUnitario.DataPropertyName = "PrecioUnitario";
            this.colPrecioUnitario.FillWeight = 80F;
            this.colPrecioUnitario.HeaderText = "Precio unitario";
            this.colPrecioUnitario.Name = "colPrecioUnitario";
            this.colPrecioUnitario.ReadOnly = true;
            // 
            // colCantidad
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCantidad.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCantidad.DataPropertyName = "Cantidad";
            this.colCantidad.FillWeight = 55F;
            this.colCantidad.HeaderText = "Cantidad";
            this.colCantidad.Name = "colCantidad";
            this.colCantidad.ReadOnly = true;
            // 
            // colEnvio
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            this.colEnvio.DefaultCellStyle = dataGridViewCellStyle3;
            this.colEnvio.DataPropertyName = "CostoEnvio";
            this.colEnvio.FillWeight = 70F;
            this.colEnvio.HeaderText = "Envío";
            this.colEnvio.Name = "colEnvio";
            this.colEnvio.ReadOnly = true;
            // 
            // colSubtotal
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            this.colSubtotal.DefaultCellStyle = dataGridViewCellStyle4;
            this.colSubtotal.DataPropertyName = "Subtotal";
            this.colSubtotal.FillWeight = 85F;
            this.colSubtotal.HeaderText = "Subtotal";
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.ReadOnly = true;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.Location = new System.Drawing.Point(20, 468);
            this.lblTotal.Size = new System.Drawing.Size(120, 25);
            this.lblTotal.Text = "Total: $0,00";
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 11;
            // 
            // btnGuardarVenta
            // 
            this.btnGuardarVenta.Location = new System.Drawing.Point(468, 496);
            this.btnGuardarVenta.Size = new System.Drawing.Size(130, 36);
            this.btnGuardarVenta.Text = "Guardar venta";
            this.btnGuardarVenta.UseVisualStyleBackColor = true;
            this.btnGuardarVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnGuardarVenta.Name = "btnGuardarVenta";
            this.btnGuardarVenta.TabIndex = 12;
            this.btnGuardarVenta.Click += new System.EventHandler(this.btnGuardarVenta_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(608, 496);
            this.btnCancelar.Size = new System.Drawing.Size(100, 36);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(720, 544);
            this.Controls.Add(this.lblEncabezado);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.cmbClientes);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.cmbProductos);
            this.Controls.Add(this.lblInfoProducto);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.nudCantidad);
            this.Controls.Add(this.btnAgregarDetalle);
            this.Controls.Add(this.btnQuitarDetalle);
            this.Controls.Add(this.dgvDetalles);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnGuardarVenta);
            this.Controls.Add(this.btnCancelar);
            this.MinimumSize = new System.Drawing.Size(640, 520);
            this.Name = "FrmVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nueva venta";
            this.Load += new System.EventHandler(this.FrmVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblEncabezado;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cmbProductos;
        private System.Windows.Forms.Label lblInfoProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnAgregarDetalle;
        private System.Windows.Forms.Button btnQuitarDetalle;
        private System.Windows.Forms.DataGridView dgvDetalles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnvio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnGuardarVenta;
        private System.Windows.Forms.Button btnCancelar;
    }
}
