using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.Services.Interfaces;

namespace VL_VendasLanches.Services
{
    public class ComplementoService : IComplementoService
    {
        private readonly IComplementoRepository _complementoRepository;

        public ComplementoService(IComplementoRepository complementoRepository) =>
            _complementoRepository = complementoRepository;

        public async Task<IEnumerable<Complemento>> ObterComplementos() =>
           await _complementoRepository.ObterListaComplemento().ConfigureAwait(false);

        public async Task<bool> VerificaNomeExiste(Complemento complemento) =>
           await _complementoRepository.NomeExiste(complemento).ConfigureAwait(false);

        public async Task CriarComplemento(Complemento complemento) =>
            await _complementoRepository.Adicionar(complemento).ConfigureAwait(false);

        public async Task<Complemento> ObterComplementoPorId(Guid id) =>
            await _complementoRepository.ObterPorId(id).ConfigureAwait(false);

        public async Task AtualizarComplemento(Complemento complemento) =>
            await _complementoRepository.Atualizar(complemento).ConfigureAwait(false);

        public async Task DeletarComplemento(Guid id) =>
            await _complementoRepository.Deletar(id).ConfigureAwait(false);
    }
}
