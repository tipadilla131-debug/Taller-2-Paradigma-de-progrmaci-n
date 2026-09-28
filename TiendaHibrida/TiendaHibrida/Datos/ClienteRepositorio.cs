using System.Collections.Generic;
using TiendaHibrida.Modelo;

namespace TiendaHibrida.Datos
{
    public class ClienteRepositorio
    {
        private readonly string _ruta;

        public ClienteRepositorio(string ruta) => _ruta = ruta;

        // Cliente es una clase plana: CsvHelper la lee y la escribe directamente.
        public List<Cliente> Cargar() => ArchivoCsv.Leer<Cliente>(_ruta);

        public void Guardar(IEnumerable<Cliente> clientes) => ArchivoCsv.Escribir(_ruta, clientes);
    }
}
