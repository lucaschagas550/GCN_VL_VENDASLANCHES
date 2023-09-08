using VL_VendasLanches.Models;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Categoria> TodosLanches();
        IEnumerable<Produto> LanchesPreferidos { get; }
        Task<IEnumerable<Produto>> ProdutoOrdenadoPorCategoriaId();
        Task<IEnumerable<Produto>> ProdutoFiltroCategoriaNomeOrdenadoNome(string categoriaNome);
        Task<IEnumerable<Produto>> ProdutoOrdenadoId();
        Task<IEnumerable<Produto>> ProdutoPesquisaNome(string produtoPesquisado);
        Task<IEnumerable<Produto>> ObterProdutosTamanhosCategorias();
        Task<Produto> ObterProdutoCategoriaComplementoTamanhoPorId(Guid id);
        Task<bool> NomeExiste(ProdutoViewModel produto);
    }
}
