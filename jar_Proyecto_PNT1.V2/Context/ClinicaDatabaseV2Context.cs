using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jar_Proyecto_PNT1.V2.Models;

namespace jar_Proyecto_PNT1.V2.Context
{
    public class ClinicaDatabaseV2Context : DbContext

    {
        public ClinicaDatabaseV2Context()
        {
        }

        public ClinicaDatabaseV2Context(DbContextOptions<ClinicaDatabaseV2Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

           
            
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-B0N0DT2G\SQLEXPRESS;Database=ClinicaDatabaseV2;Trusted_Connection=True;");
        }


        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Estudio> Estudios { get; set; }
        public DbSet<jar_Proyecto_PNT1.V2.Models.TurnoConsultaMedica> TurnoConsultaMedica { get; set; }

        
        public DbSet<TurnoPracticaMedica> TurnoPracticaMedica { get; set; }

       


    }
}
