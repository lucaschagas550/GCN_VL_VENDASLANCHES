using VL_VendasLanches.Models;

namespace VL_VendasLanches.Repositories.Interfaces
{
    public interface IComplementoProdutoRepository : IRepository<ComplementoProduto>
    {
        Task<List<ComplementoProduto>> ObterComplementoPorProdutoId(Guid id);
    }
}