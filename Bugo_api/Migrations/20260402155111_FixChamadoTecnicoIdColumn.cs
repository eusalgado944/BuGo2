using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bugo_api.Migrations
{
    /// <inheritdoc />
    public partial class FixChamadoTecnicoIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tecnico",
                table: "Chamados");

            migrationBuilder.RenameColumn(
                name: "tecnicoId",
                table: "Chamados",
                newName: "TecnicoId");

            migrationBuilder.AlterColumn<int>(
                name: "TecnicoId",
                table: "Chamados",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TecnicoId",
                table: "Chamados",
                newName: "tecnicoId");

            migrationBuilder.AlterColumn<int>(
                name: "tecnicoId",
                table: "Chamados",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tecnico",
                table: "Chamados",
                type: "text",
                nullable: true);
        }
    }
}
