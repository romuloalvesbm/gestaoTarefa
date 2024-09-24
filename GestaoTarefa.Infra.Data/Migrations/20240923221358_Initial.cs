using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoTarefa.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SETOR",
                columns: table => new
                {
                    SetorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SETOR", x => x.SetorId);
                });

            migrationBuilder.CreateTable(
                name: "TAREFA",
                columns: table => new
                {
                    TarefaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SetorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Prioridade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAREFA", x => x.TarefaId);
                    table.ForeignKey(
                        name: "FK_TAREFA_SETOR_SetorId",
                        column: x => x.SetorId,
                        principalTable: "SETOR",
                        principalColumn: "SetorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SETOR_Nome",
                table: "SETOR",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TAREFA_SetorId",
                table: "TAREFA",
                column: "SetorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAREFA");

            migrationBuilder.DropTable(
                name: "SETOR");
        }
    }
}
