using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VidaPlus.Server.Migrations.Profissionais
{
    /// <inheritdoc />
    public partial class CPFProfissional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Profissionais",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Profissionais");
        }
    }
}
