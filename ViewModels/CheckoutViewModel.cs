﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VL_VendasLanches.Models;

namespace VL_VendasLanches.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome")]
        [StringLength(50)]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe o seu endereço")]
        [StringLength(100)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Informe o complemento")]
        [StringLength(100)]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [StringLength(100)]
        [Display(Name = "Numero")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Informe o seu CEP")]
        [Display(Name = "CEP")]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "O {0} deve ter no máximo {1} caracteres e no mínimo {2}")]
        public string Cep { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        [StringLength(50)]
        public string Cidade { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Informe o seu telefone")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "Informe o email.")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "O email não possui um formato correto")]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total do Pedido")]
        public decimal PedidoTotal { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Itens no Pedido")]
        public int TotalItensPedido { get; set; }

        [Display(Name = "Data do Pedido")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }

        [Display(Name = "Data Envio Pedido")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PedidoEntregueEm { get; set; }

        public string idaspuser { get; set; }

        [Required(ErrorMessage = "Informea Forma de Pagamento")]
        [Display(Name = "Forma de Pagamento")]
        public string FormaDePagamento { get; set; }

        [Display(Name = "Status Pedido")]
        public string StatusPedido { get; set; }

        [Display(Name = "Numero do pedido")]
        public string NumerodoPedido { get; set; }

        [Required(ErrorMessage = "Informe o Modo de Entrega")]
        [Display(Name = "Modelo de Entrega")]
        public string ModelodeEntrega { get; set; }

        //[Display(Name = "Forma de Pagamento")]
        //public string FormadePagamento { get; set; }

        //public bool pix { get; set; }

        //public bool dinheiro { get; set; }

        //public bool cartaodecredito { get; set; }

        //public bool cartaodedebito { get; set; }

        //public bool entrega { get; set; }

        //public bool retirarnaloja { get; set; }

        [Display(Name = "Frete")]
        public decimal Frete { get; set; }
        public List<CarrinhoCompraItem> Carrinho { get; set; }
    }
}
