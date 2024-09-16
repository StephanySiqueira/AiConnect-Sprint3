using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiConnect.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTE_AI_NOVO",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_CLIENTE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TELEFONE_CLIENTE = table.Column<string>(type: "varchar(20)", nullable: false),
                    EMAIL_CLIENTE = table.Column<string>(type: "varchar(255)", nullable: false),
                    DATA_NASCIMENTO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ENDERECO_CLIENTE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMPRESA_CLIENTE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SEGMENTO_MERCADO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    INTERESSES_CLIENTE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE_AI_NOVO", x => x.ID_CLIENTE);
                });

            migrationBuilder.CreateTable(
                name: "LEAD_AI_NOVO",
                columns: table => new
                {
                    ID_LEAD = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_LEAD = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TELEFONE_LEAD = table.Column<string>(type: "varchar(20)", nullable: false),
                    EMAIL_LEAD = table.Column<string>(type: "varchar(255)", nullable: false),
                    CARGO_LEAD = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMPRESA_LEAD = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ID_CLIENTE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEAD_AI_NOVO", x => x.ID_LEAD);
                    table.ForeignKey(
                        name: "FK_LEAD_AI_NOVO_CLIENTE_AI_NOVO_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE_AI_NOVO",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INTERACOES_AI_NOVO",
                columns: table => new
                {
                    ID_INTERACAO_NOVO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DATA_INTERACAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TIPO_INTERACAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DESCRICAO_INTERACAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ID_CLIENTE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_LEAD = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERACOES_AI_NOVO", x => x.ID_INTERACAO_NOVO);
                    table.ForeignKey(
                        name: "FK_INTERACOES_AI_NOVO_CLIENTE_AI_NOVO_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE_AI_NOVO",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INTERACOES_AI_NOVO_LEAD_AI_NOVO_ID_LEAD",
                        column: x => x.ID_LEAD,
                        principalTable: "LEAD_AI_NOVO",
                        principalColumn: "ID_LEAD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_INTERACOES_AI_NOVO_ID_CLIENTE",
                table: "INTERACOES_AI_NOVO",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_INTERACOES_AI_NOVO_ID_LEAD",
                table: "INTERACOES_AI_NOVO",
                column: "ID_LEAD");

            migrationBuilder.CreateIndex(
                name: "IX_LEAD_AI_NOVO_ID_CLIENTE",
                table: "LEAD_AI_NOVO",
                column: "ID_CLIENTE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INTERACOES_AI_NOVO");

            migrationBuilder.DropTable(
                name: "LEAD_AI_NOVO");

            migrationBuilder.DropTable(
                name: "CLIENTE_AI_NOVO");
        }
    }
}
