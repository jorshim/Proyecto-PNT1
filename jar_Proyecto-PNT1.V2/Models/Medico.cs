using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static jar_Proyecto_PNT1.V2.Models.Validations;

namespace jar_Proyecto_PNT1.V2.Models
{
    public class Medico
    {
         
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío")]
        [Range(90000, 100000000, ErrorMessage = "La matrícula debe tener al menos 6 caracteres y como máximo 9")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "La matrícula solo debe contener números")]
        [MatriculaExistsAtributte]
        [Display(Name = "Matricula")]
        public int Matricula { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "El campo no puede quedar vacío")]
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
        [MaxLength(30, ErrorMessage = "El nombre debe tener como máximo 30 caracteres")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El nombre solo debe contener letras")]
        //trasladasr a todas las validaciones
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "El campo no puede quedar vacío")]
        [MinLength(3, ErrorMessage = "El apellido debe tener al menos 3 caracteres")]
        [MaxLength(30, ErrorMessage = "El apellido debe tener como máximo 30 caracteres")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El apellido solo debe contener letras")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [EnumDataType(typeof(Especialidad))]
        [Display(Name = "Especialidad")]
        public Especialidad Especialidad { get; set; }
        
        
     

    }
}
