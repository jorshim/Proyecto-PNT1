using jar_Proyecto_PNT1.V2.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jar_Proyecto_PNT1.V2.Models
{
    public class Validations
    {
        public class DniExistsAtributte : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                using (var context = new ClinicaDatabaseV2Context())
                {
                    int Dni = (int)value;
                    if (context.Pacientes.Any(e => e.Documento == Dni)) //probar usando any
                    {
                        return new ValidationResult("El usuario ya está registrado en el sistema");
                    }
                }
                return ValidationResult.Success;
            }
        }

        public class DniExistsDB : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                using (var context = new ClinicaDatabaseV2Context())
                {
                    
                    int Dni = (int)value;
                    Boolean documentoExists = context.Pacientes.Any(e => e.Documento == Dni);
                    if (documentoExists) //probar usando any
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("El paciente no está registrado en sistema. Deberá crearlo primero");
                    }
                }
                
            }
        }


        public class MatriculaExistsAtributte : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                using (var context = new ClinicaDatabaseV2Context())
                {
                    int Matricula = (int)value;
                    if (context.Medicos.Any(e => e.Matricula == Matricula)) 
                    {
                        return new ValidationResult("El médico ya está registrado en el sistema");
                    }
                }
                return ValidationResult.Success;
            }
        }


        public class ValidDateAtributte : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                    DateTime fechaIngresada = (DateTime)value;
                    DateTime date = DateTime.Now;
                    if (fechaIngresada > date) 
                    {
                        return new ValidationResult("La fecha de nacimiento no puede ser mayor al día actual");
                    }
                return ValidationResult.Success;
            }
        }


    }
}
