using Microsoft.EntityFrameworkCore.Migrations;

namespace jar_Proyecto_PNT1.V2.Migrations
{
    public partial class jar_Proyecto_PNT1V2ContextClinicaDatabaseV2Context03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TurnoConsultaMedica",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaciente = table.Column<int>(nullable: false),
                    DiasDisponibles = table.Column<int>(nullable: false),
                    HorasDisponibles = table.Column<int>(nullable: false),
                    IdMedico = table.Column<int>(nullable: false),
                    FechaConsultaMedica = table.Column<string>(nullable: true),
                    DocumentoPaciente = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnoConsultaMedica", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurnoConsultaMedica");
        }
    }
}
