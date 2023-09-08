using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VL_VendasLanches.Models
{
    [Table("Categorias")]
    public class Categoria : Entity
    {
        [Required(ErrorMessage = "Informe o nome da categoria")]
        [StringLength(100,ErrorMessage ="O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "Nome")]
        public string CategoriaNome { get; set; }

        [Required(ErrorMessage = "Informe a descrição da categoria")]
        [StringLength(200, ErrorMessage = "O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public ICollection<Produto> Produtos { get; set; }
        public Categoria()
        {
            Produtos = new HashSet<Produto>();
        }
    }
}