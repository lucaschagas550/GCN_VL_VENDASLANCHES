using System.ComponentModel.DataAnnotations.Schema;

namespace VL_VendasLanches.Models
{
    public class PedidoDetalhe : Entity
    {
        [ForeignKey("PedidoId")]
        public Guid PedidoId { get; set; }

        [ForeignKey("ProdutoId")]
        public Guid ProdutoId { get; set; }

        public int Quantidade { get; set; }
        public string Observacao { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}