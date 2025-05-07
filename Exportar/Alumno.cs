using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exportar
{
    internal class Alumno
    {
        public Alumno(string nombre, int edad, string carrera, int matricula, DateTime fechaNacimiento)
        {
            Nombre = nombre;
            Edad = edad;
            Carrera = carrera;
            Matricula = matricula;
            FechaNacimiento = fechaNacimiento;
        }

        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Carrera { get; set; }
        public int Matricula { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
