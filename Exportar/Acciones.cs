using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exportar
{
    internal class Acciones
    {
        private List<Alumno> alumnoList = new List<Alumno>();

        public List<Alumno> Mostrar()
        {
            return alumnoList;
        }
        public bool ExportaraExcel()
        {
            try
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Alumnos");

                // Encabezados
                worksheet.Cell(1, 1).Value = "Nombre";
                worksheet.Cell(1, 2).Value = "Edad";
                worksheet.Cell(1, 3).Value = "Carrera";
                worksheet.Cell(1, 4).Value = "Matricula";
                worksheet.Cell(1, 5).Value = "Fecha de Ingreso";

                // Llenar datos
                for (int i = 0; i < alumnoList.Count; i++)
                {
                    var alumno = alumnoList[i];
                    worksheet.Cell(i + 2, 1).Value = alumno.Nombre;
                    worksheet.Cell(i + 2, 2).Value = alumno.Edad;
                    worksheet.Cell(i + 2, 3).Value = alumno.Carrera;
                    worksheet.Cell(i + 2, 4).Value = alumno.Matricula;
                    worksheet.Cell(i + 2, 5).Value = alumno.FechaNacimiento.ToShortDateString();
                }

                // Obtener ruta del escritorio
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "ListaAlumnos.xlsx");

                // Guardar archivo
                workbook.SaveAs(filePath);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ImportardeExcel()
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "ListaAlumnos.xlsx");

                if (!File.Exists(filePath))
                    return false;

                var newList = new List<Alumno>();

                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Alumnos");
                    var rows = worksheet.RowsUsed().Skip(1); // Omitir encabezados

                    foreach (var row in rows)
                    {
                        string nombre = row.Cell(1).GetString();
                        int edad = int.Parse(row.Cell(2).GetValue<string>());
                        string carrera = row.Cell(3).GetString();
                        int matricula = int.Parse(row.Cell(4).GetValue<string>());
                        DateTime fechaIngreso = DateTime.Parse(row.Cell(5).GetString());

                        newList.Add(new Alumno(nombre, edad, carrera, matricula, fechaIngreso));
                    }
                }

                alumnoList = newList;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
