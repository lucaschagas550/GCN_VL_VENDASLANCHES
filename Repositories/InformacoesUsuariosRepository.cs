using VL_VendasLanches.Context;
using VL_VendasLanches.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using VL_VendasLanches.Models;

namespace VL_VendasLanches.Repositories
{
    public class InformacoesUsuariosRepository : Repository<InformacoesUsuarios>, IInformacoesUsuariosRepository
    {
        public InformacoesUsuariosRepository(AppDbContext context) : base(context) { }

        public InformacoesUsuarios IdAspuser(string id)
        {
            return _context.InformacoesUsuarios.Where(l => l.idaspuser == id).AsNoTracking().FirstOrDefault();
        }
    }
}
