﻿using VL_VendasLanches.ViewModels;
using VL_VendasLanches.Models;

namespace VL_VendasLanches.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        bool CriarPedido(Pedido pedido);
        IQueryable<Pedido> Get();
        Task<PagedViewModel<Pedido>> ObterTodos(IQueryable<Pedido> source, int pageNumber, int pageSize, string query);
        Task<IEnumerable<Pedido>> Pedidos(string iduser);
        PedidoLancheViewModel ObterPedidoUsuarioDetalhe(Guid idpedido);
    }
}
