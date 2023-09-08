﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VL_VendasLanches.Migrations
{
    public partial class alterandoCarrinhoCompraItemNullTamanho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoCompraItens_Tamanhos_TamanhoId",
                table: "CarrinhoCompraItens");

            migrationBuilder.AlterColumn<Guid>(
                name: "TamanhoId",
                table: "CarrinhoCompraItens",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoCompraItens_Tamanhos_TamanhoId",
                table: "CarrinhoCompraItens",
                column: "TamanhoId",
                principalTable: "Tamanhos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoCompraItens_Tamanhos_TamanhoId",
                table: "CarrinhoCompraItens");

            migrationBuilder.AlterColumn<Guid>(
                name: "TamanhoId",
                table: "CarrinhoCompraItens",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoCompraItens_Tamanhos_TamanhoId",
                table: "CarrinhoCompraItens",
                column: "TamanhoId",
                principalTable: "Tamanhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
