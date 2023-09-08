using VL_VendasLanches.Models;

namespace VL_VendasLanches.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> ObterCategorias();
        Task<Categoria> ObterCategoriaPorId(Guid id);
        Task CriarCategoria(Categoria categoria);
        Task AtualizarCategoria(Categoria categoria);
        Task DeletarCategoria(Guid id);
        Task<bool> VerificaNomeExiste(Categoria categoria);
    }
}