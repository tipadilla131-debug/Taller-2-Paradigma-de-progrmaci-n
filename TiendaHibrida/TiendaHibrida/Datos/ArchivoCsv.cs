using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace TiendaHibrida.Datos
{
    // Único lugar donde se toca CsvHelper: los repositorios solo piden "leer" o "escribir".
    internal static class ArchivoCsv
    {
        private static CsvConfiguration ConfigLectura() =>
            new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,   // tolera columnas nuevas ausentes en archivos viejos
                HeaderValidated = null
            };

        public static List<T> Leer<T>(string ruta)
        {
            // Archivo inexistente o vacío = todavía no hay datos.
            if (!File.Exists(ruta) || new FileInfo(ruta).Length == 0)
                return new List<T>();

            using var lector = new StreamReader(ruta);
            using var csv = new CsvReader(lector, ConfigLectura());
            return csv.GetRecords<T>().ToList();
        }

        public static void Escribir<T>(string ruta, IEnumerable<T> registros)
        {
            // Se escribe a un archivo temporal y luego se reemplaza: si algo falla a la mitad,
            // el CSV original no queda corrupto.
            string temporal = ruta + ".tmp";

            using (var escritor = new StreamWriter(temporal))
            using (var csv = new CsvWriter(escritor, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(registros);
            }

            File.Move(temporal, ruta, true);
        }
    }
}
