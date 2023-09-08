using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VL_VendasLanches.Models
{
    [Table("CarrinhoCompraItens")]
    public class CarrinhoCompraItem : Entity
    {
        [ForeignKey("ProdutoId")]
        public Guid ProdutoId { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [StringLength(200)]
        public string CarrinhoCompraId { get; set; }

        [ForeignKey("TamanhoId")]
        public Guid? TamanhoId { get; set; }

        [StringLength(600)]
        [Display(Name = "Observaçâo")]
        public string Observacao { get; set; }

        public virtual Tamanho Tamanho { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
