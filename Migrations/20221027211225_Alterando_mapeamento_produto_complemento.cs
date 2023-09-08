using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VL_VendasLanches.Migrations
{
    public partial class Alterando_mapeamento_produto_complemento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplementoProduto_Complementos_ComplementosId",
                table: "ComplementoProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplementoProduto_Produtos_ProdutosId",
                table: "ComplementoProduto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComplementoProduto",
                table: "ComplementoProduto");

            migrationBuilder.RenameColumn(
                name: "ProdutosId",
                table: "ComplementoProduto",
                newName: "ProdutoId");

            migrationBuilder.RenameColumn(
                name: "ComplementosId",
                table: "ComplementoProduto",
                newName: "ComplementoId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplementoProduto_ProdutosId",
                table: "ComplementoProduto",
                newName: "IX_ComplementoProduto_ProdutoId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ComplementoProduto",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComplementoProduto",
                table: "ComplementoProduto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoProduto_ComplementoId",
                table: "ComplementoProduto",
                column: "ComplementoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplementoProduto_Complementos_ComplementoId",
                table: "ComplementoProduto",
                column: "ComplementoId",
                principalTable: "Complementos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplementoProduto_Produtos_ProdutoId",
                table: "ComplementoProduto",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplementoProduto_Complementos_ComplementoId",
                table: "ComplementoProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplementoProduto_Produtos_ProdutoId",
                table: "ComplementoProduto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComplementoProduto",
                table: "ComplementoProduto");

            migrationBuilder.DropIndex(
                name: "IX_ComplementoProduto_ComplementoId",
                table: "ComplementoProduto");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ComplementoProduto");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "ComplementoProduto",
                newName: "ProdutosId");

            migrationBuilder.RenameColumn(
                name: "ComplementoId",
                table: "ComplementoProduto",
                newName: "ComplementosId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplementoProduto_ProdutoId",
                table: "ComplementoProduto",
                newName: "IX_ComplementoProduto_ProdutosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComplementoProduto",
                table: "ComplementoProduto",
                columns: new[] { "ComplementosId", "ProdutosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ComplementoProduto_Complementos_ComplementosId",
                table: "ComplementoProduto",
                column: "ComplementosId",
                principalTable: "Complementos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplementoProduto_Produtos_ProdutosId",
                table: "ComplementoProduto",
                column: "ProdutosId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
