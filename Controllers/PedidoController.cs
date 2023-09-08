using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using System.Text;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.Services.Interfaces;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IMemoryCache _memorycache;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidosServices _pedidoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly IInformacoesUsuariosService _informacoesUsuariosService;
        StringBuilder TextoProduto = new StringBuilder();
        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra, IMemoryCache memorycache, IHttpContextAccessor httpContextAccessor, IPedidosServices pedidoService, IInformacoesUsuariosService informacoesUsuariosService)
        {
            _httpContextAccessor = httpContextAccessor;
            _pedidoRepository = pedidoRepository;
            _pedidoService = pedidoService;
            _carrinhoCompra = carrinhoCompra;
            _informacoesUsuariosService = informacoesUsuariosService;
            _memorycache = memorycache;
        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult EditarEndereco()
        {
            CheckoutViewModel pedido = new CheckoutViewModel();
            pedido = MapearUsuario();
            return View(pedido);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditarEndereco(CheckoutViewModel pedido)
        {
            pedido = await CriarCheckout(pedido);
            return View("~/Views/Pedido/Checkout.cshtml", pedido);
        }


        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Checkout()
        {
            CheckoutViewModel pedido = new CheckoutViewModel();
            pedido = MapearUsuario();
            pedido = await CriarCheckout(pedido);

            return View(pedido);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel pedido)
        {
            //obtem os itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> items = await _carrinhoCompra.GetCarrinhoCompraItens().ConfigureAwait(false);
            _carrinhoCompra.CarrinhoCompraItens = items;
            pedido.Carrinho = items;

            //verifica se existe algum erro na model, se tiver retorna
            //if (!ModelState.IsValid) return View(pedido);

            //numero pedido
            Random randNum = new Random();
            var NumeroPedido = randNum.Next(99).ToString();
            pedido.NumerodoPedido = NumeroPedido + DateTime.Now.ToString("ddMMyyyyHHmmss");

            //status do pedido
            pedido.StatusPedido = "Aguardando Confirmação";

            //cria o pedido e os detalhes
            pedido.PedidoEnviado = DateTime.Now;
            _pedidoService.CriarPedido(pedido);

            //define mensagens ao cliente
            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
            ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

            //limpa o carrinho do cliente
            _carrinhoCompra.LimparCarrinho();
            ViewBag.Pedido = pedido;

            // Cria cache  
            //var memoryCacheEntryOptions = new MemoryCacheEntryOptions
            //{
            //    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
            //    SlidingExpiration = TimeSpan.FromSeconds(1200)
            //};
            //if (!_memorycache.TryGetValue(pedido.Id, out Pedido pedidos))
            //{
            //    _memorycache.Set(pedido.Id, pedido, memoryCacheEntryOptions);
            //}

            //exibe a view com dados do cliente e do pedido
            return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);

        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> PedidosUsuario()
        {
            var id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var pedidos = await _pedidoService.ObterListaDePedidosUsuario(id);
            return View(pedidos);
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult PedidosUsuarioDetalhe(Guid id)
        {
            var pedido = _pedidoService.ObterPedidoUsuarioDetalhe(id);
            return View(pedido);
        }

        //------------- Funções----------//
        public CheckoutViewModel MapearUsuario()
        {
            CheckoutViewModel pedido = new CheckoutViewModel();
            var id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _informacoesUsuariosService.GetUserEdit(id);

            // Dados usuario
            pedido.Cep = user.cep;
            pedido.Numero = user.numero;
            pedido.Complemento = user.complemento;
            pedido.Cidade = user.Cidade;
            pedido.Endereco = user.endereco;
            pedido.Email = user.email;
            pedido.Telefone = user.telefone;
            pedido.idaspuser = id;
            pedido.Estado = user.Estado;
            pedido.Nome = user.UserName;
            pedido.Sobrenome = user.sobrenome;

            return pedido;
        }

        public async Task<CheckoutViewModel> CriarCheckout(CheckoutViewModel pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //obtem os itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> items = await _carrinhoCompra.GetCarrinhoCompraItens().ConfigureAwait(false);
            _carrinhoCompra.CarrinhoCompraItens = items;

            //verifica se existem itens de pedido
            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um lanche...");
            }

            //calcula o total de itens e o total do pedido
            foreach (var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Produto.Preco * item.Quantidade);
            }

            //atribui os valores obtidos ao pedido
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;
            pedido.Carrinho = items;

            return pedido;
        }

        public async Task<IActionResult> Whats(Guid pedidoid)
        {
            Pedido pedido;

            if (_memorycache.TryGetValue(pedidoid, out pedido));

            //verifica se existe algum erro na model, se tiver retorna
            //    if (!ModelState.IsValid) return View(pedido);

            //obtem os itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> items = await _carrinhoCompra.GetCarrinhoCompraItens().ConfigureAwait(false);
            _carrinhoCompra.CarrinhoCompraItens = items;

            foreach (var item in pedido.PedidoItens)
            {
                TextoProduto.AppendLine("*Quantidade:" + item.Quantidade + "%20"
                                       + "Produto:" + item.Produto.Nome + "%20"
                                       + "Valor:R$:" + (item.Produto.Preco * item.Quantidade) + "%0A%0A");
            }

            //string url = $"https://api.whatsapp.com/send?phone=5511943664740&text=%F0%9F%91%8B%20Ol%C3%A1%2C%20Novo%20Pedido%20%0A%0A**Tipo%20de%20servi%C3%A7o%3A%20Delivery**%0A%0ANome%3A%20{pedido.Nome}%0ATelefone%3A%20{pedido.Telefone}%0AEndere%C3%A7o%3A%20{pedido.Endereco1}%20%0A%0A*M%C3%A9todo%20de%20pagamento%3A%20Validar%20com%20nossa%20equipe*%0A%0A*%F0%9F%92%B2%20Custos*%0APre%C3%A7o%20dos%20produtos%3A%20R%24%20{pedido.PedidoTotal}%0APre%C3%A7o%20de%20entrega%3A%20Valor%20a%20confirmar%0A*Total%20a%20pagar%3A%20R%24%2070%2C00*%0A%0A*%F0%9F%93%9D%20Pedido*%0A%0A-%20{TextoProduto}%0A%0A%F0%9F%91%86%20Ap%C3%B3s%20enviar%20o%20pedido%2C%20aguarde%20que%20j%C3%A1%20iremos%20lhe%20atender..%0A";
            //  var uri = new Uri(url);
            //return Redirect(uri.AbsoluteUri);
            //string url = "ggg";
            //exibe a view com dados do cliente e do pedido
            return Redirect(null);
        }
    }
}