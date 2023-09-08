using VL_VendasLanches.Models;

namespace VL_VendasLanches.Services.Interfaces
{
    public interface IComplementoService
    {

        Task<IEnumerable<Complemento>> ObterComplementos();

        Task<bool> VerificaNomeExiste(Complemento complemento);

        Task CriarComplemento(Complemento complemento);

        Task<Complemento> ObterComplementoPorId(Guid id);

        Task AtualizarComplemento(Complemento complemento);

        Task DeletarComplemento(Guid id);
    }
}
