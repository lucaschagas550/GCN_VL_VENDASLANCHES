using Microsoft.EntityFrameworkCore;
using VL_VendasLanches.Context;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext appContext) =>
            _context = appContext;

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //Define uma sessao
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //obtem um servico do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            //obtem ou gera id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //atribui o id do carrinho na sessao
            session.SetString("CarrinhoId", carrinhoId);

            //retorna carrinho com contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId,
            };
        }

        public async Task AdicionarCarrinho(Produto lanche, ProdutoViewModel produtoViewModel)
        {
            var carrinhoCompraItem = await _context.CarrinhoCompraItens.SingleOrDefaultAsync(
                                        s => s.Produto.Id.Equals(lanche.Id)
                                        && s.CarrinhoCompraId.Equals(CarrinhoCompraId)).ConfigureAwait(false);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Produto = lanche,
                    Quantidade = produtoViewModel.quantidade,
                    TamanhoId = produtoViewModel.TamanhoId,
                    Observacao = produtoViewModel.observacao
                };

                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Produto = lanche;
                carrinhoCompraItem.Quantidade = produtoViewModel.quantidade;
                carrinhoCompraItem.TamanhoId = produtoViewModel.TamanhoId;
                carrinhoCompraItem.Observacao = produtoViewModel.observacao;
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task AtualizarCarrinho(CarrinhoCompraItem carrinhoCompraItem)
        {
            _context.Update(carrinhoCompraItem);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task RemoverDoCarrinho(Produto lanche)
        {
            var carrinhoCompraItem = await _context.CarrinhoCompraItens
                .SingleOrDefaultAsync(s => s.Produto.Id.Equals(lanche.Id)
                && s.CarrinhoCompraId.Equals(CarrinhoCompraId))
                .ConfigureAwait(false);

            if (carrinhoCompraItem != null)
            {
                _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<CarrinhoCompraItem>> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItens ?? (CarrinhoCompraItens = await _context.CarrinhoCompraItens
                                            .Include(s => s.Produto)
                                            .Include(s => s.Produto.Tamanhos.OrderBy(s => s.Tamanho.TamanhoEnum))
                                            .ThenInclude(s => s.Tamanho)
                                            .Where(c => c.CarrinhoCompraId.Equals(CarrinhoCompraId))
                                            .ToListAsync().ConfigureAwait(false));
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItensSincrono()
        {
            return CarrinhoCompraItens ?? (CarrinhoCompraItens = _context.CarrinhoCompraItens
                                            .Include(s => s.Produto)
                                            .Where(c => c.CarrinhoCompraId.Equals(CarrinhoCompraId))
                                            .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                                    .Where(carrinho => carrinho.CarrinhoCompraId.Equals(CarrinhoCompraId));

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public async Task<decimal> GetCarrinhoCompraTotal()
        {
            return await _context.CarrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId.Equals(CarrinhoCompraId))
                        .Select(c => c.Produto.Preco * c.Quantidade).SumAsync();
        }
    }
}
