using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jar_Proyecto_PNT1.V2.Migrations
{
    public partial class jar_Proyecto_PNT1V2ContextClinicaDatabaseV2Context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoEstudio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<int>(maxLength: 7, nullable: false),
                    Nombre = table.Column<string>(maxLength: 30, nullable: false),
                    Apellido = table.Column<string>(maxLength: 30, nullable: false),
                    Especialidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Documento = table.Column<int>(maxLength: 9, nullable: false),
                    Nombre = table.Column<string>(maxLength: 30, nullable: false),
                    Apellido = table.Column<string>(maxLength: 30, nullable: false),
                    FechaDeNacimiento = table.Column<DateTime>(nullable: false),
                    ObraSocial = table.Column<string>(maxLength: 15, nullable: false),
                    NroAfiliado = table.Column<int>(maxLength: 11, nullable: false),
                    Telefono = table.Column<string>(nullable: false),
                    CorreoElectronico = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estudios");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
