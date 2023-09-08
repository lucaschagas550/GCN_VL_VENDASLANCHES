using System.ComponentModel.DataAnnotations;

namespace VL_VendasLanches.ViewModels
{
    public class ProdutoCarrinhoAtualizarViewModel
    {
        public Guid ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public Guid TamanhoId { get; set; }

        public string Observacao { get; set; }
    }
}
