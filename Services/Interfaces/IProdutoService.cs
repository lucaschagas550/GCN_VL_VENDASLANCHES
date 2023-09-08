using VL_VendasLanches.Models;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<List<Categoria>> ObterCategorias();
        Task<List<Tamanho>> ObterTamanhos();
        Task<ProdutoListViewModel> ObterListaProdutoPorCategoria(string categoria);
        Task<IEnumerable<Produto>> ObterListaProdutoComCategoriaComTamanho();
        Task<ProdutoViewModel> ObterProdutoPorId(Guid produtoId);
        Task<ProdutoListViewModel> PesquisarProduto(string palavraChave);
        Task<Produto> ObterProdutoComCategoriaComTamanhoPorId(Guid id);
        Task CriarProduto(ProdutoViewModel produto);
        Task<ProdutoViewModel> ObterProdutoViewModelParaAtualizar(Guid id);
        Task AtualizarProduto(ProdutoViewModel produtoViewModel);
        Task DeletarProduto(Guid id);
        Task<bool> VerificaNomeExiste(ProdutoViewModel produto);
    }
}