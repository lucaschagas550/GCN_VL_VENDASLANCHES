using System.ComponentModel.DataAnnotations;

namespace VL_VendasLanches.ViewModels
{
    public class SenhaViewModel
    {

        [Required(ErrorMessage = "Informe a senha antiga")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Passwordold { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

       
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Senha")]
        [Compare("Password")]
        public string confirmesenha { get; set; }

  
       
    }
}
