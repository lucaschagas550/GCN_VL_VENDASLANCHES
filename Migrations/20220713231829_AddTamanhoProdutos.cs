using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VL_VendasLanches.Migrations
{
    public partial class AddTamanhoProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Tamanhos(Id, Tamanho) VALUES('bb44e9c0-37d5-4c0c-b073-934ed482fb5a',1)");
            migrationBuilder.Sql("INSERT INTO Tamanhos(Id, Tamanho) VALUES('f76506e7-b596-4a6a-9a7c-08f69fbd55a6', 2)");
            migrationBuilder.Sql("INSERT INTO Tamanhos(Id, Tamanho) VALUES('ac8450fa-c76b-473e-8ff5-98a00d54b3f9', 3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM tamanhos");
        }
    }
}
