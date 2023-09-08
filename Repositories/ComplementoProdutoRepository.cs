using Microsoft.EntityFrameworkCore;
using VL_VendasLanches.Context;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;

namespace VL_VendasLanches.Repositories
{
    public class ComplementoProdutoRepository : Repository<ComplementoProduto>, IComplementoProdutoRepository
    {
        public ComplementoProdutoRepository(AppDbContext context) : base(context) { }

        public async Task<List<ComplementoProduto>> ObterComplementoPorProdutoId(Guid id) =>
            await _context.ComplementoProduto.AsNoTracking().Where(t => t.ProdutoId.Equals(id)).ToListAsync().ConfigureAwait(false);
    }
}
