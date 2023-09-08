using VL_VendasLanches.Models;

namespace VL_VendasLanches.Repositories.Interfaces
{
    public interface IComplementoRepository : IRepository<Complemento>
    {
        Task<IEnumerable<Complemento>> ObterListaComplemento();

        Task<bool> NomeExiste(Complemento complemento);
    }
}