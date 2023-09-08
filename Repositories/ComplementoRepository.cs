using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VL_VendasLanches.Context;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;

namespace VL_VendasLanches.Repositories
{
    public class ComplementoRepository : Repository<Complemento>, IComplementoRepository
    {

        public ComplementoRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Complemento>> ObterListaComplemento() =>
          await _context.Complemento.AsNoTracking().ToListAsync();

        public async Task<bool> NomeExiste(Complemento complemento) =>
          await _context.Complemento.AsNoTracking().AnyAsync(p => p.Nome.Equals(complemento.Nome)).ConfigureAwait(false);
    }
}
