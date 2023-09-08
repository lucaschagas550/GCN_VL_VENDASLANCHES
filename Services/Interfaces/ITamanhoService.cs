using VL_VendasLanches.Models;

namespace VL_VendasLanches.Services.Interfaces
{
    public interface ITamanhoService
    {
        Task<List<Tamanho>> ObterTamanhos();
    }
}
