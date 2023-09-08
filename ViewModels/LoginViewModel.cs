using System.ComponentModel.DataAnnotations;

namespace VL_VendasLanches.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

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

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Senha")]
        [Compare("Password", ErrorMessage = "As senhas estão diferentes")]
        public string confirmesenha { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome")]
        [StringLength(150, ErrorMessage = "O tamanho máxmimo é {1} caracteres")]
        [Display(Name = "Sobrenome ")]
        public string sobrenome { get; set; }

        public string email { get; set; }

        public string idaspuser { get; set; }
    }
}
