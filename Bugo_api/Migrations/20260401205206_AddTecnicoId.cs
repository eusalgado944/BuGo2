using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bugo_blazor.Migrations
{
    /// <inheritdoc />
    public partial class AddTecnicoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tecnicoId",
                table: "Chamados",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tecnicoId",
                table: "Chamados");
        }
    }
}
