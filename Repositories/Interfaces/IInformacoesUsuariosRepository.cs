using VL_VendasLanches.Models;

namespace VL_VendasLanches.Repositories.Interfaces
{
    public interface IInformacoesUsuariosRepository : IRepository<InformacoesUsuarios>
    {
        InformacoesUsuarios IdAspuser(string id);
    }
}
