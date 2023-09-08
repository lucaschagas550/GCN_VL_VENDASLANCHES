using System.ComponentModel.DataAnnotations.Schema;

namespace VL_VendasLanches.Models
{
    public class ComplementoProduto : Entity
    {
        [ForeignKey("ProdutoId")]
        public Guid ProdutoId { get; set; }

        [ForeignKey("ComplementoId")]
        public Guid ComplementoId { get; set; }

        public virtual Produto Produto{ get; set; }
        public virtual Complemento Complemento { get; set; }

        public ComplementoProduto() {}

        public ComplementoProduto(Guid complementoId, Guid produtoId)
        {
            ComplementoId = complementoId;
            ProdutoId = produtoId;
        }
    }
}