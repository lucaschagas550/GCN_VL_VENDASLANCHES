using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.Services.Interfaces;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Services
{
    public class CarrinhoCompraService : ICarrinhoCompraService
    {
        private IProdutoRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraService(IProdutoRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository=lancheRepository;
            _carrinhoCompra=carrinhoCompra;
        }

        public async Task<CarrinhoCompraViewModel> RecuperarCarrinho()
        {
            _carrinhoCompra.CarrinhoCompraItens = await _carrinhoCompra.GetCarrinhoCompraItens().ConfigureAwait(false);
            return new CarrinhoCompraViewModel(_carrinhoCompra, await _carrinhoCompra.GetCarrinhoCompraTotal().ConfigureAwait(false));
        }

        public async Task<int> ObterQuantidadeCarrinho()
        {
            _carrinhoCompra.CarrinhoCompraItens = await _carrinhoCompra.GetCarrinhoCompraItens().ConfigureAwait(false);
            return _carrinhoCompra.CarrinhoCompraItens.Count;
        }

        public async Task AdicionarItemNoCarrinhoCompra(ProdutoViewModel produtoViewModel)
        {
            var lancheSelecionado = await _lancheRepository.ObterPorId(produtoViewModel.Id).ConfigureAwait(false);

            if (lancheSelecionado != null) await _carrinhoCompra.AdicionarCarrinho(lancheSelecionado, produtoViewModel).ConfigureAwait(false);
        }

        public async Task AtualizarItemCarrinhoCompra(ProdutoCarrinhoAtualizarViewModel produtoCarrinhoAtualizarViewModel)
        {
            var carrinhoItens = await _carrinhoCompra.GetCarrinhoCompraItens().ConfigureAwait(false);
            var atualizarProduto = carrinhoItens.FirstOrDefault(p => p.Produto.Id.Equals(produtoCarrinhoAtualizarViewModel.ProdutoId));
           
            if (atualizarProduto != null)
            {
                atualizarProduto.Quantidade = produtoCarrinhoAtualizarViewModel.Quantidade;
                atualizarProduto.TamanhoId = produtoCarrinhoAtualizarViewModel?.TamanhoId;
                await _carrinhoCompra.AtualizarCarrinho(atualizarProduto).ConfigureAwait(false);
            }
        }

        public async Task RemoverItemCarrinhoCompra(Guid lancheId)
        {
            var lancheSelecionado = await _lancheRepository.ObterPorId(lancheId).ConfigureAwait(false);

            if (lancheSelecionado != null)
            {
                await _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado).ConfigureAwait(false);
            }
        }
    }
}