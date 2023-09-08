using VL_VendasLanches.Models;

namespace VL_VendasLanches.ViewModels
{
    public class ListaProdutosHomeViewModel
    {
        public List<Produto> Lanches { get; set; }
        public Categoria Categorias { get; set; }
    }
}
