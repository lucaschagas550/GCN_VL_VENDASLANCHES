using System.ComponentModel.DataAnnotations.Schema;

namespace VL_VendasLanches.Models
{
    public class TamanhoProduto : Entity
    {
        [ForeignKey("TamanhoId")]
        public Guid TamanhoId { get; set; }

        [ForeignKey("ProdutoId")]
        public Guid ProdutoId { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Tamanho Tamanho { get; set; }

        public TamanhoProduto() {}
        public TamanhoProduto(Guid tamanhoId, Guid lancheId)
        {
            TamanhoId = tamanhoId;
            ProdutoId = lancheId;
        }
    }
}
