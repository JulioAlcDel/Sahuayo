using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SahuayoPrueba.Models
{
    public class PersonaViewModel
    {
        public int IdPersona { get; set; }
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [DisplayName("Nombre")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Ingrese solo letras")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [DisplayName("Apellido Paterno")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Ingrese solo letras")]
        public string ApellidoPaterno { get; set; }
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Ingrese solo letras")]
        [DisplayName("Apellido Materno")]
        public string ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "El campo {0} es Requerido")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Ingrese solo letras")]
        [DisplayName("Descripción")]        
        public string Descripcion { get; set; }
        [DisplayName("Tiene enfermedad")]
        public bool TieneEnfermedad { get; set; }
        [RegularExpression(@"^(\d|-)?(\d|,)*\.?\d*$", ErrorMessage = "Ingrese solo decimales 0.00")]
        public decimal Sueldo { get; set; }


        public DateTime FechaRegistro { get; set; }
    }
}
