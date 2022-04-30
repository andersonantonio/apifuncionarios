using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFuncionarios.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FUNCIONARIO",
                columns: table => new
                {
                    IDFUNCIONARIO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MATRICULA = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    DATAADMISSAO = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIO", x => x.IDFUNCIONARIO);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_CPF",
                table: "FUNCIONARIO",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_MATRICULA",
                table: "FUNCIONARIO",
                column: "MATRICULA",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FUNCIONARIO");
        }
    }
}
