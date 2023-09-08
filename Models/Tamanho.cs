using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VL_VendasLanches.Models
{
    public class Tamanho : Entity
    {
        [Column("Tamanho")]
        [Required(ErrorMessage = "O tamanho deve ser informado")]
        public ETamanho TamanhoEnum { get; set; }

        public virtual List<TamanhoProduto> TamanhoProduto { get; set; }

        public Tamanho() =>
            TamanhoProduto = new List<TamanhoProduto>();
    }

    public enum ETamanho
    {
        Pequeno = 1,
        Medio,
        Grande
    }
}
