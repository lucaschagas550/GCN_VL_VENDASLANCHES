using Microsoft.EntityFrameworkCore;
using VL_VendasLanches.Context;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;

namespace VL_VendasLanches.Repositories
{
    public class TamanhoRepository : ITamanhoRepository
    {
        private readonly AppDbContext _context;

        public TamanhoRepository(AppDbContext context) =>
            _context = context;

        public async Task<List<Tamanho>> ObterTamanhos()
        {
            return await _context.Tamanhos.OrderBy(p => p.TamanhoEnum).ToListAsync();
        }
    }
}
