using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VL_VendasLanches.Models;

namespace VL_VendasLanches.Context
{
    //Para ter mais de um banco, basta criar outro contexto e adicionar o serviço no startup

    //IdentityDbContext fornece todas as propriedades dbset necessarias para gerenciar as tabelas de identidade
    //IdentityUser contem propriedades para Username,passwordHas, Email etc. Classe usada pelo identity para gerenciar usuarios registrados no seu aplicativo
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Produto> Lanche { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }
        public DbSet<Tamanho> Tamanhos { get; set; }
        public DbSet<TamanhoProduto> TamanhoProdutos { get; set; }
        public DbSet<Complemento> Complemento { get; set; }
        public DbSet<InformacoesUsuarios> InformacoesUsuarios { get; set; }
        public DbSet<ComplementoProduto> ComplementoProduto { get; set; }
    }
}
