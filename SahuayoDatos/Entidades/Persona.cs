using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahuayoDatos.Entidades
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string  Nombre {get; set;}

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Descripcion { get; set; }
        public bool TieneEnfermedad { get; set; }
        public decimal Sueldo { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
