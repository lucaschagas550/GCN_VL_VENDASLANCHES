using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VL_VendasLanches.Models
{
    [Table("InformacoesUsuarios")]
    public class InformacoesUsuarios : Entity
    {
        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(150, ErrorMessage = "O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "Nome ")]
        public string username { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome")]
        [StringLength(150, ErrorMessage = "O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "Sobrenome ")]
        public string sobrenome { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        [StringLength(150, ErrorMessage = "O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "Endereço ")]
        public string endereco { get; set; }

        [Required(ErrorMessage = "Informe o complemento")]
        [StringLength(10, ErrorMessage = "O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "Complemento")]
        public string complemento { get; set; }

        [Required(ErrorMessage = "Informe o numero")]
        [StringLength(10, ErrorMessage = "O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "numero")]
        public string numero { get; set; }

        [Required(ErrorMessage = "Informe o cep")]
        [StringLength(10, ErrorMessage = "O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "cep")]
        public string cep { get; set; }

        [Required(ErrorMessage = "Informe o telefoe")]
        [StringLength(10, ErrorMessage = "O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "telefone")]
        [Phone]
        public string telefone { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        [StringLength(50, ErrorMessage = "O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o estado")]
        [StringLength(50, ErrorMessage = "O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        public string email { get; set; }

        public string idaspuser { get; set; }
    }
}
