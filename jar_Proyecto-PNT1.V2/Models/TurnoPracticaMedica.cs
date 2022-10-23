using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static jar_Proyecto_PNT1.V2.Models.Validations;

namespace jar_Proyecto_PNT1.V2.Models
{
    public class TurnoPracticaMedica: Turno

    {
        public int IdPracticaMedica { get; set; }

        [Display(Name = "Fecha del turno")]
        public string FechaConsultaMedica { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío")]
        [Range(90000, 100000000, ErrorMessage = "El documento debe tener al menos 6 caracteres y como máximo 9")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El documento solo debe contener números")]
        [DniExistsDB]
        [Display(Name = "Documento del paciente")]
        public int DocumentoPaciente { get; set; }
    }
}
