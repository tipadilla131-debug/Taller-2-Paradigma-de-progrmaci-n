using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TiendaHibrida.Modelo;

namespace TiendaHibrida.Datos
{
    // Datos en memoria compartidos por toda la aplicación + acceso a los repositorios.
    // Al abrir la app se carga todo (CargarTodo); al crear/actualizar/eliminar se llama a Guardar*.
    public static class Contexto
    {
        // OJO con el orden: CarpetaDatos debe declararse antes que los repositorios.
        private static readonly string CarpetaDatos = Path.Combine(AppContext.BaseDirectory, "Data");

        private static readonly ProductoRepositorio RepoProductos =
            new ProductoRepositorio(Path.Combine(CarpetaDatos, "productos.csv"));

        private static readonly ClienteRepositorio RepoClientes =
            new ClienteRepositorio(Path.Combine(CarpetaDatos, "clientes.csv"));

        private static readonly VentaRepositorio RepoVentas =
            new VentaRepositorio(
                Path.Combine(CarpetaDatos, "ventas.csv"),
                Path.Combine(CarpetaDatos, "saledetails.csv"));

        public static List<Producto> Productos { get; private set; } = new List<Producto>();
        public static List<Cliente> Clientes { get; private set; } = new List<Cliente>();
        public static List<Venta> Ventas { get; private set; } = new List<Venta>();

        public static void CargarTodo()
        {
            Directory.CreateDirectory(CarpetaDatos);

            Productos = RepoProductos.Cargar();
            Clientes = RepoClientes.Cargar();
            Ventas = RepoVentas.Cargar(Clientes);   // las ventas necesitan a los clientes ya cargados
        }

        public static void GuardarProductos() => RepoProductos.Guardar(Productos);
        public static void GuardarClientes() => RepoClientes.Guardar(Clientes);
        public static void GuardarVentas() => RepoVentas.Guardar(Ventas);

        public static int SiguienteNumeroVenta() =>
            Ventas.Count == 0 ? 1 : Ventas.Max(v => v.NumeroVenta) + 1;
    }
}
