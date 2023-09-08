using AutoMapper;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.Services.Interfaces;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Services
{
    public class PedidosServices : IPedidosServices
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidosServices(IPedidoRepository pedidoRepository, IMapper mapper, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
            _carrinhoCompra = carrinhoCompra;
        }

        public async Task<IEnumerable<Pedido>> ObterListaDePedidosUsuario(string id)
        {
            return await _pedidoRepository.Pedidos(id);
        }

        public PedidoLancheViewModel ObterPedidoUsuarioDetalhe(Guid id)
        {
            return _pedidoRepository.ObterPedidoUsuarioDetalhe(id);
        }

        public async void CriarPedido(CheckoutViewModel pedidoViewModel)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);

            var reult = _pedidoRepository.CriarPedido(pedido);

        }
    }
}
