using Microsoft.AspNetCore.Mvc;
using VL_VendasLanches.Services.Interfaces;

namespace VL_VendasLanches.Components
{
    public class CarrinhoViewComponent : ViewComponent
    {
        private readonly ICarrinhoCompraService _carrinhoCompraService;

        public CarrinhoViewComponent(ICarrinhoCompraService carrinhoCompraService) =>
            _carrinhoCompraService = carrinhoCompraService;

        public async Task<IViewComponentResult> InvokeAsync() =>
            View(await _carrinhoCompraService.ObterQuantidadeCarrinho().ConfigureAwait(false));
    }
}