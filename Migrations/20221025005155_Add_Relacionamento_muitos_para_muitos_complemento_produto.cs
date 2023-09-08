using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VL_VendasLanches.Migrations
{
    public partial class Add_Relacionamento_muitos_para_muitos_complemento_produto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoCompraItens_Lanches_LancheId",
                table: "CarrinhoCompraItens");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoDetalhes_Lanches_LancheId",
                table: "PedidoDetalhes");

            migrationBuilder.DropForeignKey(
                name: "FK_TamanhoProdutos_Lanches_LancheId",
                table: "TamanhoProdutos");

            migrationBuilder.DropTable(
                name: "Lanches");

            migrationBuilder.RenameColumn(
                name: "LancheId",
                table: "TamanhoProdutos",
                newName: "ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_TamanhoProdutos_LancheId",
                table: "TamanhoProdutos",
                newName: "IX_TamanhoProdutos_ProdutoId");

            migrationBuilder.RenameColumn(
                name: "LancheId",
                table: "PedidoDetalhes",
                newName: "ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoDetalhes_LancheId",
                table: "PedidoDetalhes",
                newName: "IX_PedidoDetalhes_ProdutoId");

            migrationBuilder.RenameColumn(
                name: "LancheId",
                table: "CarrinhoCompraItens",
                newName: "ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_CarrinhoCompraItens_LancheId",
                table: "CarrinhoCompraItens",
                newName: "IX_CarrinhoCompraItens_ProdutoId");

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescricaoCurta = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescricaoDetalhada = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ImagemUrl = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemThumbnailUrl = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsLanchePreferido = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EmEstoque = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ExisteTamanho = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ComplementoProduto",
                columns: table => new
                {
                    ComplementosId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProdutosId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplementoProduto", x => new { x.ComplementosId, x.ProdutosId });
                    table.ForeignKey(
                        name: "FK_ComplementoProduto_Complementos_ComplementosId",
                        column: x => x.ComplementosId,
                        principalTable: "Complementos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplementoProduto_Produtos_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoProduto_ProdutosId",
                table: "ComplementoProduto",
                column: "ProdutosId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoCompraItens_Produtos_ProdutoId",
                table: "CarrinhoCompraItens",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoDetalhes_Produtos_ProdutoId",
                table: "PedidoDetalhes",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TamanhoProdutos_Produtos_ProdutoId",
                table: "TamanhoProdutos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoCompraItens_Produtos_ProdutoId",
                table: "CarrinhoCompraItens");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoDetalhes_Produtos_ProdutoId",
                table: "PedidoDetalhes");

            migrationBuilder.DropForeignKey(
                name: "FK_TamanhoProdutos_Produtos_ProdutoId",
                table: "TamanhoProdutos");

            migrationBuilder.DropTable(
                name: "ComplementoProduto");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "TamanhoProdutos",
                newName: "LancheId");

            migrationBuilder.RenameIndex(
                name: "IX_TamanhoProdutos_ProdutoId",
                table: "TamanhoProdutos",
                newName: "IX_TamanhoProdutos_LancheId");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "PedidoDetalhes",
                newName: "LancheId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoDetalhes_ProdutoId",
                table: "PedidoDetalhes",
                newName: "IX_PedidoDetalhes_LancheId");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "CarrinhoCompraItens",
                newName: "LancheId");

            migrationBuilder.RenameIndex(
                name: "IX_CarrinhoCompraItens_ProdutoId",
                table: "CarrinhoCompraItens",
                newName: "IX_CarrinhoCompraItens_LancheId");

            migrationBuilder.CreateTable(
                name: "Lanches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoriaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DescricaoCurta = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescricaoDetalhada = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmEstoque = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExisteTamanho = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ImagemThumbnailUrl = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemUrl = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsLanchePreferido = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lanches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lanches_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Lanches_CategoriaId",
                table: "Lanches",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoCompraItens_Lanches_LancheId",
                table: "CarrinhoCompraItens",
                column: "LancheId",
                principalTable: "Lanches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoDetalhes_Lanches_LancheId",
                table: "PedidoDetalhes",
                column: "LancheId",
                principalTable: "Lanches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TamanhoProdutos_Lanches_LancheId",
                table: "TamanhoProdutos",
                column: "LancheId",
                principalTable: "Lanches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
