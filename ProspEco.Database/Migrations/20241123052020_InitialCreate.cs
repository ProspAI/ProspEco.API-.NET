using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProspEco.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "bandeiras_tarifarias",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    data_vigencia = table.Column<DateTime>(type: "date", nullable: false),
                    tipo_bandeira = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bandeiras_tarifarias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    pontuacao_economia = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    role = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Aparelhos",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    descricao = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    potencia = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    tipo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    usuario_id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aparelhos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Aparelhos_Usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conquistas",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    data_conquista = table.Column<DateTime>(type: "datetime", nullable: false),
                    descricao = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    titulo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    usuario_id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conquistas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Conquistas_Usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "metas",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    atingida = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    consumo_alvo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data_fim = table.Column<DateTime>(type: "date", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "date", nullable: false),
                    usuario_id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metas", x => x.id);
                    table.ForeignKey(
                        name: "FK_metas_Usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notificacoes",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    data_hora = table.Column<DateTime>(type: "datetime", nullable: false),
                    lida = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    mensagem = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    usuario_id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notificacoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_notificacoes_Usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recomendacoes",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    data_hora = table.Column<DateTime>(type: "datetime", nullable: false),
                    mensagem = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: false),
                    usuario_id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recomendacoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_recomendacoes_Usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "registros_consumo",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    consumo = table.Column<decimal>(type: "float(126)", nullable: false),
                    data_hora = table.Column<DateTime>(type: "datetime", nullable: false),
                    aparelho_id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registros_consumo", x => x.id);
                    table.ForeignKey(
                        name: "FK_registros_consumo_Aparelhos_aparelho_id",
                        column: x => x.aparelho_id,
                        principalTable: "Aparelhos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aparelhos_usuario_id",
                table: "Aparelhos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_BandeiraTarifaria_DataVigencia",
                schema: "dbo",
                table: "bandeiras_tarifarias",
                column: "data_vigencia");

            migrationBuilder.CreateIndex(
                name: "IX_Conquistas_usuario_id",
                table: "Conquistas",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_metas_usuario_id",
                schema: "dbo",
                table: "metas",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_notificacoes_usuario_id",
                schema: "dbo",
                table: "notificacoes",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_recomendacoes_usuario_id",
                schema: "dbo",
                table: "recomendacoes",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_registros_consumo_aparelho_id",
                schema: "dbo",
                table: "registros_consumo",
                column: "aparelho_id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_email",
                table: "Usuarios",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bandeiras_tarifarias",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Conquistas");

            migrationBuilder.DropTable(
                name: "metas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "notificacoes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "recomendacoes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "registros_consumo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Aparelhos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
