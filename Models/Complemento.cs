using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VL_VendasLanches.Models
{
    [Table("Complementos")]
    public class Complemento : Entity
    {
        [Required(ErrorMessage = "Informe o nome do complemento")]
        [Display(Name = "Nome do complemento")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "O {0} deve ter no mínimo {2} e no máximo {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o preço do complemento")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1.00, 999.99, ErrorMessage = "O {0} deve estar entre {1} e {2}")]
        [RegularExpression(@"^[1-9]\d{0,2}(\,\d{3})*,\d{2}$", ErrorMessage ="O {0} precisar ser no seguinto formato: 1,00")]
        public decimal Preco { get; set; }

        public virtual List<ComplementoProduto> ComplementoProduto { get; set; }
        
        public Complemento() =>
            ComplementoProduto = new List<ComplementoProduto>();
    }
}