using Microsoft.EntityFrameworkCore;
using VL_VendasLanches.Context;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;

namespace VL_VendasLanches.Repositories
{
    public class TamanhoProdutosRepository : Repository<TamanhoProduto>, ITamanhoProdutosRepository
    {
        public TamanhoProdutosRepository(AppDbContext context) : base(context) { }

        public async Task<List<TamanhoProduto>> ObterTamanhoPorProdutoId(Guid id) =>
            await _context.TamanhoProdutos.AsNoTracking().Where(t => t.ProdutoId.Equals(id)).ToListAsync().ConfigureAwait(false);
    }
}
