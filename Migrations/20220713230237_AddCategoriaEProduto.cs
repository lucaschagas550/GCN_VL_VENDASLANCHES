using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VL_VendasLanches.Migrations
{
    public partial class AddCategoriaEProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //inserindo dados na migration para popular a tabela de Categoria

            migrationBuilder.Sql("INSERT INTO Categorias(Id, CategoriaNome, Descricao) " +
                "VALUES('652df5ad-1723-4244-aea5-4bf0da77d8e0', 'Normal','Lanche feito com ingredientes normais')");

            migrationBuilder.Sql("INSERT INTO Categorias(Id, CategoriaNome, Descricao) " +
                "VALUES('fd696153-467d-4cfd-8ec2-394ca031b805', 'Natural','Lanche feito com ingredientes integrais e naturais')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");

            migrationBuilder.Sql("DELETE FROM Lanches");
        }
    }
}
