using jar_Proyecto_PNT1.V2.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static jar_Proyecto_PNT1.V2.Models.Validations;

namespace jar_Proyecto_PNT1.V2.Models
{
    public class Paciente //ver de hacer la validación del dni buscando en la base, y validación de la fechaDeNacimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo no puede quedar vacío")]
        [Range(90000, 100000000, ErrorMessage = "El documento debe tener al menos 6 caracteres y como máximo 9")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El documento solo debe contener números")]
        [DniExistsAtributte]
        public int Documento { get; set; }

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

        [ValidDateAtributte]
        [Required(ErrorMessage = "El campo no puede quedar vacío")]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaDeNacimiento { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "El campo no puede quedar vacío")]
        [MinLength(6, ErrorMessage = "La obre social debe tener al menos 6 caracteres")]
        [MaxLength(15, ErrorMessage = "La obre social debe tener como máximo 15 caracteres")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "La obra social solo puede contener letras y/o números")]
        [Display(Name = "Obra Social")]
        public string ObraSocial { get; set; }


        [Required(ErrorMessage = "El campo no puede quedar vacío")]
        [Range(90000, 1000000000000, ErrorMessage = "El número de afiliado debe tener entre 6 y 12 caracteres.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El número de afiliado/a solo debe contener números")]//CORREGIR ACÁ, QUE SOLO SEAN N'ROS
        //[RegularExpression(("([1-9][0-9]*)"), ErrorMessage = "Please enter valid Number")]
        [Display(Name = "Nro Afiliado")]
        public int NroAfiliado { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Teléfono de Contacto inválido")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El teléfono solo debe contener números")]
        [Display(Name = "Telefono de Contacto")]
        public string Telefono { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El campo no puede quedar vacío")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "El Email no es válido.")]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

    }

    
        
        
}
