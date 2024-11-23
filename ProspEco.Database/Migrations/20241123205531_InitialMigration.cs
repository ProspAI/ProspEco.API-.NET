using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProspEco.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "prospecco_bandeiras_tarifarias",
                columns: table => new
                {
                    id_bandeira = table.Column<long>(type: "number(11)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_vigencia = table.Column<DateTime>(type: "date", nullable: false),
                    ds_tipo_bandeira = table.Column<string>(type: "varchar(20)", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "date", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prospecco_bandeiras_tarifarias", x => x.id_bandeira);
                });

            migrationBuilder.CreateTable(
                name: "prospecco_usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<long>(type: "number(11)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    ds_nome = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    vl_pontuacao_economia = table.Column<decimal>(type: "number(11,2)", nullable: true),
                    ds_role = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    ds_senha = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "date", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prospecco_usuarios", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "prospecco_aparelhos",
                columns: table => new
                {
                    id_aparelho = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_descricao = table.Column<string>(type: "varchar(255)", nullable: false),
                    ds_nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    vl_potencia = table.Column<decimal>(type: "number(11,2)", nullable: false),
                    ds_tipo = table.Column<string>(type: "varchar(50)", nullable: false),
                    id_usuario = table.Column<long>(type: "number(11)", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "date", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prospecco_aparelhos", x => x.id_aparelho);
                    table.ForeignKey(
                        name: "FK_prospecco_aparelhos_prospecco_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "prospecco_usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prospecco_conquistas",
                columns: table => new
                {
                    id_conquista = table.Column<long>(type: "number(11)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_conquista = table.Column<DateTime>(type: "date", nullable: false),
                    ds_descricao = table.Column<string>(type: "varchar(255)", nullable: false),
                    ds_titulo = table.Column<string>(type: "varchar(100)", nullable: false),
                    id_usuario = table.Column<long>(type: "number(11)", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "date", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prospecco_conquistas", x => x.id_conquista);
                    table.ForeignKey(
                        name: "FK_prospecco_conquistas_prospecco_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "prospecco_usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prospecco_metas",
                columns: table => new
                {
                    id_meta = table.Column<long>(type: "number(11)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    fl_atingida = table.Column<string>(type: "char(1)", nullable: false),
                    vl_consumo_alvo = table.Column<decimal>(type: "number(11,2)", nullable: false),
                    dt_fim = table.Column<DateTime>(type: "date", nullable: false),
                    dt_inicio = table.Column<DateTime>(type: "date", nullable: false),
                    id_usuario = table.Column<long>(type: "number(11)", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "date", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prospecco_metas", x => x.id_meta);
                    table.ForeignKey(
                        name: "FK_prospecco_metas_prospecco_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "prospecco_usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prospecco_notificacoes",
                columns: table => new
                {
                    id_notificacao = table.Column<long>(type: "number(11)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_hora = table.Column<DateTime>(type: "date", nullable: false),
                    fl_lida = table.Column<string>(type: "char(1)", nullable: false),
                    ds_mensagem = table.Column<string>(type: "varchar(255)", nullable: false),
                    id_usuario = table.Column<long>(type: "number(11)", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "date", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prospecco_notificacoes", x => x.id_notificacao);
                    table.ForeignKey(
                        name: "FK_prospecco_notificacoes_prospecco_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "prospecco_usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prospecco_recomendacoes",
                columns: table => new
                {
                    id_recomendacao = table.Column<long>(type: "number(11)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_hora = table.Column<DateTime>(type: "date", nullable: false),
                    ds_mensagem = table.Column<string>(type: "varchar(255)", nullable: false),
                    id_usuario = table.Column<long>(type: "number(11)", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "date", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prospecco_recomendacoes", x => x.id_recomendacao);
                    table.ForeignKey(
                        name: "FK_prospecco_recomendacoes_prospecco_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "prospecco_usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prospecco_registros_consumo",
                columns: table => new
                {
                    id_registro = table.Column<long>(type: "number(11)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    vl_consumo = table.Column<decimal>(type: "number(11,2)", nullable: false),
                    dt_hora = table.Column<DateTime>(type: "date", nullable: false),
                    id_aparelho = table.Column<long>(type: "number(11)", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "date", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prospecco_registros_consumo", x => x.id_registro);
                    table.ForeignKey(
                        name: "FK_prospecco_registros_consumo_prospecco_aparelhos_id_aparelho",
                        column: x => x.id_aparelho,
                        principalTable: "prospecco_aparelhos",
                        principalColumn: "id_aparelho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_prospecco_aparelhos_id_usuario",
                table: "prospecco_aparelhos",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_prospecco_conquistas_id_usuario",
                table: "prospecco_conquistas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_prospecco_metas_id_usuario",
                table: "prospecco_metas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_prospecco_notificacoes_id_usuario",
                table: "prospecco_notificacoes",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_prospecco_recomendacoes_id_usuario",
                table: "prospecco_recomendacoes",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_prospecco_registros_consumo_id_aparelho",
                table: "prospecco_registros_consumo",
                column: "id_aparelho");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prospecco_bandeiras_tarifarias");

            migrationBuilder.DropTable(
                name: "prospecco_conquistas");

            migrationBuilder.DropTable(
                name: "prospecco_metas");

            migrationBuilder.DropTable(
                name: "prospecco_notificacoes");

            migrationBuilder.DropTable(
                name: "prospecco_recomendacoes");

            migrationBuilder.DropTable(
                name: "prospecco_registros_consumo");

            migrationBuilder.DropTable(
                name: "prospecco_aparelhos");

            migrationBuilder.DropTable(
                name: "prospecco_usuarios");
        }
    }
}
