using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VL_VendasLanches.Migrations
{
    public partial class Add_Produtos_para_tabela_Produtos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("Insert INTO Produtos(Id,CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco,ExisteTamanho)" +
                "VALUES('6ebe7b79-ced4-4cba-9974-8f09be03c7c6', '652df5ad-1723-4244-aea5-4bf0da77d8e0','Pão, hambúrger, ovo, presunto, queijo e batata palha','Delicioso pão de hambúrger com ovo frito, presunto e queijo de primeira qualidade acompanhado com batata palha',1,'http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg','http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg',0,'Cheese Salada',12.50, 0)");

            migrationBuilder.Sql("INSERT INTO Produtos(Id,CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco,ExisteTamanho)" +
                "VALUES('8d55bbed-2da3-46a7-9220-7916cf0d673a', '652df5ad-1723-4244-aea5-4bf0da77d8e0','Pão, presunto, mussarela e tomate','Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho',1,'http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg','http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg',0,'Misto Quente', 8.00,0)");

            migrationBuilder.Sql("INSERT INTO Produtos(Id,CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco,ExisteTamanho)" +
                "VALUES('a88eba7b-3cfe-4c32-95ea-7ac29a7dec6d', '652df5ad-1723-4244-aea5-4bf0da77d8e0','Pão, hambúrger, presunto, mussarela e batalha palha','Pão de hambúrger especial com hambúrger de nossa preparação e presunto e mussarela, acompanha batata palha.',1,'http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg',0,'Cheese Burger', 11.00, 0)");

            migrationBuilder.Sql("INSERT INTO Produtos(Id,CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco,ExisteTamanho)" +
                "VALUES('8b6cca6b-b9ac-48ba-87c7-5f2e09f0210a', 'fd696153-467d-4cfd-8ec2-394ca031b805','Pão Integral, queijo branco, peito de peru, cenoura, alface, iogurte','Pão integral natural com queijo branco, peito de peru e cenoura ralada com alface picado e iorgurte natural.',1,'http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg','http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg',1,'Lanche Natural Peito Peru', 15.00, 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM produtos");
        }
    }
}