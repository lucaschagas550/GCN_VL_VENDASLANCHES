using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VL_VendasLanches.Services.Interfaces;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ICarrinhoCompraService _carrinhoCompraService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarrinhoCompraController(ICarrinhoCompraService carrinhoCompraService,
            IHttpContextAccessor httpContextAccessor)
        {
            _carrinhoCompraService = carrinhoCompraService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Carrinho()
        {
            var carrinho = await _carrinhoCompraService.RecuperarCarrinho().ConfigureAwait(false);
            return View(carrinho);
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AdicionarItemNoCarrinhoCompra(ProdutoViewModel produtoViewModel)
        {
            await _carrinhoCompraService.AdicionarItemNoCarrinhoCompra(produtoViewModel).ConfigureAwait(false);
            return RedirectToAction("Carrinho");
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RemoverItemDoCarrinhoCompra(Guid lancheId)
        {
            await _carrinhoCompraService.RemoverItemCarrinhoCompra(lancheId).ConfigureAwait(false);
            return RedirectToAction("Carrinho");
        }
    }
}