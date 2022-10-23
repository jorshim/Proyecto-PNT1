using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jar_Proyecto_PNT1.V2.Models
{
    public class Estudio //Los estudios desde el front no se van a poder manipular, los voy a usar para ofrecer los estudios disponibles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [EnumDataType(typeof(TipoEstudio))]
        public TipoEstudio TipoEstudio { get; set; } 

    }
}
