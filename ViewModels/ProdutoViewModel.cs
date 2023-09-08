using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VL_VendasLanches.Models;

namespace VL_VendasLanches.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do produto deve ser informado")]
        [Display(Name = "Nome do produto")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {2} caracter e no máximo {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do produto deve ser informado")]
        [Display(Name = "Descrição do produto")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1}")]
        [MaxLength(200, ErrorMessage = "Descrição não pode exceder {1} caracteres")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A descrição do produto deve ser informado")]
        [Display(Name = "Descrição detalhada do produto")]
        [MinLength(20, ErrorMessage = "Descrição detalhada deve ter no mínimo {1}")]
        [MaxLength(200, ErrorMessage = "Descrição detalhada não pode exceder {1} caracteres")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1.00, 999.99, ErrorMessage = "O {0} deve estar entre {1} e {2}")]
        [RegularExpression(@"^[1-9]\d{0,2}(\,\d{3})*,\d{2}$", ErrorMessage = "O {0} precisar ser no seguinto formato: 1,00")]
        public decimal? Preco { get; set; }

        [Display(Name = "Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        [Required(ErrorMessage = "Informe uma categoria")]
        public Guid CategoriaId { get; set; }

        public bool ExisteTamanho { get; set; }
        public Guid? TamanhoId { get; set; }

        [NotMapped]
        [Display(Name = "Qauntidade")]
        public int quantidade { get; set; }

        [NotMapped]
        [Display(Name = "Observaçâo")]
        public string observacao { get; set; }

        public List<Tamanho> TodosTamanhos { get; set; } = new List<Tamanho>();
        public List<TamanhoProduto> Tamanhos { get; set; } = new List<TamanhoProduto>();
        public List<string> TamanhosSelecionados { get; set; } = new List<string>();


        public List<Complemento> TodosComplementos { get; set; } = new List<Complemento>();
        public List<ComplementoProduto> Complementos { get; set; } = new List<ComplementoProduto>();
        public List<string> ComplementosSelecionados { get; set; } = new List<string>();
    }
}
