using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VL_VendasLanches.Migrations
{
    public partial class alterando_Pedido_detalhe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "PedidoDetalhes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "PedidoDetalhes");
        }
    }
}
