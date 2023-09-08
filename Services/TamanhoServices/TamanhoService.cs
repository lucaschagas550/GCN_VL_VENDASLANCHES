using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.Services.Interfaces;

namespace VL_VendasLanches.Services
{
    public class TamanhoService : ITamanhoService
    {
        private readonly ITamanhoRepository _tamanhoRepository;
        public TamanhoService(ITamanhoRepository tamanhoRepository) =>
            _tamanhoRepository = tamanhoRepository;

        public async Task<List<Tamanho>> ObterTamanhos()
        {
            return await _tamanhoRepository.ObterTamanhos();
        }
    }
}
