using Microsoft.EntityFrameworkCore;
using VL_VendasLanches.Context;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;

namespace VL_VendasLanches.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context) { }

        public async Task<bool> NomeExiste(Categoria categoria) =>
            await _context.Categoria.AsNoTracking().AnyAsync(p => p.CategoriaNome.Equals(categoria.CategoriaNome)).ConfigureAwait(false);

        public async Task<List<Categoria>> ObterCategorias() =>
            await _context.Categoria.AsNoTracking().OrderBy(c => c.CategoriaNome).ToListAsync();

    }
}
