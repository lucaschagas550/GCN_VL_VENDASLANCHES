using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.Services.Interfaces;

namespace VL_VendasLanches.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository) =>
            _categoriaRepository=categoriaRepository;

        public async Task AtualizarCategoria(Categoria categoria) =>
            await _categoriaRepository.Atualizar(categoria).ConfigureAwait(false);

        public async Task CriarCategoria(Categoria categoria) =>
            await _categoriaRepository.Adicionar(categoria).ConfigureAwait(false);

        public async Task DeletarCategoria(Guid id) =>
            await _categoriaRepository.Deletar(id).ConfigureAwait(false);

        public async Task<Categoria> ObterCategoriaPorId(Guid id) =>
            await _categoriaRepository.ObterPorId(id).ConfigureAwait(false);

        public async Task<List<Categoria>> ObterCategorias() =>
            await _categoriaRepository.ObterTodos().ConfigureAwait(false);

        public async Task<bool> VerificaNomeExiste(Categoria categoria) =>
            await _categoriaRepository.NomeExiste(categoria).ConfigureAwait(false);
    }
}
