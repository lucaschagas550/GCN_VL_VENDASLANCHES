using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Services.Interfaces
{
    public interface ICarrinhoCompraService
    {
        Task<CarrinhoCompraViewModel> RecuperarCarrinho();
        Task<int> ObterQuantidadeCarrinho();
        Task AdicionarItemNoCarrinhoCompra(ProdutoViewModel produtoViewModel);
        Task RemoverItemCarrinhoCompra(Guid lancheId);
        Task AtualizarItemCarrinhoCompra(ProdutoCarrinhoAtualizarViewModel produtoCarrinhoAtualizarViewModel);
    }
}
