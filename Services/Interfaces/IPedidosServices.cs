using VL_VendasLanches.Models;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Services.Interfaces
{
    public interface IPedidosServices
    {
        Task<IEnumerable<Pedido>> ObterListaDePedidosUsuario(string id);
        PedidoLancheViewModel ObterPedidoUsuarioDetalhe(Guid id);
        void CriarPedido(CheckoutViewModel pedidoViewModel);
    }
}
