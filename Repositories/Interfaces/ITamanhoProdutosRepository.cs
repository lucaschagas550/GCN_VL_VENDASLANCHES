using VL_VendasLanches.Models;

namespace VL_VendasLanches.Repositories.Interfaces
{
    public interface ITamanhoProdutosRepository : IRepository<TamanhoProduto>
    {
        Task<List<TamanhoProduto>> ObterTamanhoPorProdutoId(Guid id);
    }
}
