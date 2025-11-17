using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VidaPlus.Server.Migrations.FinanceiroLeitos
{
    /// <inheritdoc />
    public partial class CriarTabelaFinanceiroLeitos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internacoes_Leitos_LeitoId",
                table: "Internacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leitos",
                table: "Leitos");

            migrationBuilder.RenameTable(
                name: "Leitos",
                newName: "Leito");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Leito",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leito",
                table: "Leito",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Financeiro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Unidade = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financeiro", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Internacoes_Leito_LeitoId",
                table: "Internacoes",
                column: "LeitoId",
                principalTable: "Leito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internacoes_Leito_LeitoId",
                table: "Internacoes");

            migrationBuilder.DropTable(
                name: "Financeiro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leito",
                table: "Leito");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Leito");

            migrationBuilder.RenameTable(
                name: "Leito",
                newName: "Leitos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leitos",
                table: "Leitos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Internacoes_Leitos_LeitoId",
                table: "Internacoes",
                column: "LeitoId",
                principalTable: "Leitos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
