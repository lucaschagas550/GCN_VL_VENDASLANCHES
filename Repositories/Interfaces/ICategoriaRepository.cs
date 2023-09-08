using VL_VendasLanches.Models;

namespace VL_VendasLanches.Repositories.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<bool> NomeExiste(Categoria categoria);
        Task<List<Categoria>> ObterCategorias();
    }
}