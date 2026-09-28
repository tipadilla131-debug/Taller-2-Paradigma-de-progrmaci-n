using System.Collections.Generic;
using System.IO;
using System.Linq;
using TiendaHibrida.Datos.Maps;
using TiendaHibrida.Modelo;

namespace TiendaHibrida.Datos
{
    public class ProductoRepositorio
    {
        private readonly string _ruta;

        public ProductoRepositorio(string ruta) => _ruta = ruta;

        public List<Producto> Cargar()
        {
            var productos = new List<Producto>();

            foreach (var fila in ArchivoCsv.Leer<ProductoCsvRow>(_ruta))
            {
                // La columna "Tipo" dice qué clase concreta reconstruir.
                Producto producto;
                switch (fila.Tipo)
                {
                    case "Fisico":
                        producto = new ProductoFisico
                        {
                            StockDisponible = fila.StockDisponible ?? 0,
                            CostoEnvio = fila.CostoEnvio ?? 0m
                        };
                        break;
                    case "Digital":
                        producto = new ProductoDigital
                        {
                            Formato = fila.Formato,
                            UrlDescarga = fila.UrlDescarga
                        };
                        break;
                    default:
                        throw new InvalidDataException($"Tipo de producto desconocido en productos.csv: '{fila.Tipo}'");
                }

                producto.Codigo = fila.Codigo;
                producto.Nombre = fila.Nombre;
                producto.Descripcion = fila.Descripcion;
                producto.Precio = fila.Precio;
                producto.Categoria = fila.Categoria;
                producto.Peso = fila.Peso;

                productos.Add(producto);
            }

            return productos;
        }

        public void Guardar(IEnumerable<Producto> productos)
        {
            var filas = productos.Select(p => new ProductoCsvRow
            {
                Tipo = p.Tipo,
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Categoria = p.Categoria,
                Peso = p.Peso,
                StockDisponible = (p as ProductoFisico)?.StockDisponible,
                CostoEnvio = (p as ProductoFisico)?.CostoEnvio,
                Formato = (p as ProductoDigital)?.Formato,
                UrlDescarga = (p as ProductoDigital)?.UrlDescarga
            }).ToList();

            ArchivoCsv.Escribir(_ruta, filas);
        }
    }
}
