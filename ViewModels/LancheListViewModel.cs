using VL_VendasLanches.Models;

namespace VL_VendasLanches.ViewModels
{
    public class ProdutoListViewModel
    {
        public IEnumerable<Produto> Lanches { get; set; }
        public string CategoriaAtual{ get; set; }

        public ProdutoListViewModel(IEnumerable<Produto> lanches, string categoriaAtual)
        {
            Lanches=lanches;
            CategoriaAtual=categoriaAtual;
        }

        public ProdutoListViewModel() {}
    }
}
