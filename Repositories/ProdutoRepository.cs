using Microsoft.EntityFrameworkCore;
using VL_VendasLanches.Context;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Repositories
{

    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {

        public ProdutoRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Produto> LanchesPreferidos => _context.Lanche
                                                        .Where(l => l.IsLanchePreferido)
                                                        .Include(c => c.Categoria);
        public IEnumerable<Categoria> TodosLanches()
        {
            List<ListaProdutosHomeViewModel> home = new List<ListaProdutosHomeViewModel>();

            var categoria = _context.Categoria.ToList();
            if (categoria != null)
            {
                foreach (var cat in categoria)
                {
                    ListaProdutosHomeViewModel homeprod = new ListaProdutosHomeViewModel();
                    homeprod.Lanches = new List<Produto>();
                    homeprod.Categorias = cat;

                    foreach (var prod in cat.Produtos)
                    {
                        homeprod.Lanches.Add(prod);
                    }
                    home.Add(homeprod);
                }
            }
            return categoria;
        }
        public async Task<IEnumerable<Produto>> ProdutoOrdenadoPorCategoriaId() =>
             await _context.Lanche.Include(c => c.Categoria).AsNoTracking()
            .OrderBy(l => l.Id).ToListAsync().ConfigureAwait(false);

        public async Task<IEnumerable<Produto>> ProdutoFiltroCategoriaNomeOrdenadoNome(string categoriaNome) =>
             await _context.Lanche.Include(c => c.Categoria).AsNoTracking()
            .Where(l => l.Categoria.CategoriaNome.ToLower().Equals(categoriaNome.ToLower()))
            .OrderBy(c => c.Nome)
            .ToListAsync().ConfigureAwait(false);

        public async Task<IEnumerable<Produto>> ProdutoOrdenadoId() =>
            await _context.Lanche.AsNoTracking().OrderBy(p => p.Id).ToListAsync();

        public async Task<IEnumerable<Produto>> ProdutoPesquisaNome(string produtoPesquisado) =>
            await _context.Lanche.AsNoTracking()
            .Where(p => p.Nome.ToLower().Contains(produtoPesquisado.ToLower()))
            .ToListAsync().ConfigureAwait(false);

        public async Task<IEnumerable<Produto>> ObterProdutosTamanhosCategorias() =>
            await _context.Lanche
            .Include(p => p.Categoria)
            .Include(p => p.Tamanhos)
            .ThenInclude(p => p.Tamanho)
            .ToListAsync();

        public async Task<Produto> ObterProdutoCategoriaComplementoTamanhoPorId(Guid id) =>
            await _context.Lanche
            .Include(l => l.Categoria)
            .Include(p => p.Complementos.OrderBy(c => c.Complemento.Nome))
            .ThenInclude(c => c.Complemento)
            .Include(p => p.Tamanhos.OrderBy(t => t.Tamanho.TamanhoEnum))
            .ThenInclude(t => t.Tamanho)
            .FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);

        public async Task<bool> NomeExiste(ProdutoViewModel produto) =>
            await _context.Lanche.AnyAsync(p => p.Nome.Equals(produto.Nome)).ConfigureAwait(false);

        public async Task<bool> NomeExistee(Produto produto)
        {
             _context.Lanche.UpdateRange(produto);

            return true;

        }
    }
}
