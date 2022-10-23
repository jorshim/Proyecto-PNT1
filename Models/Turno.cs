using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jar_Proyecto-PNT1.V2.Models
{
    public class Turno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public DiasDisponibles DiasDisponibles { get; set; }
        public HorasDisponibles HorasDisponibles { get; set; }

        //voy a empezar a hacer la lógica de la creación de turno primero desde una especialidad hasta el médico
        //mapear a la bbdd

       
    }
    
}
