using VL_VendasLanches.Models;

namespace VL_VendasLanches.Repositories.Interfaces
{
    public interface ITamanhoRepository
    {
        Task<List<Tamanho>> ObterTamanhos();
    }
}
