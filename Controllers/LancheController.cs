using Microsoft.AspNetCore.Mvc;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.Services.Interfaces;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Controllers
{
    public class LancheController : Controller
    {
        private readonly IProdutoService _produtoService;

        public LancheController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> List(string categoria)
        {
            #region
            //ViewData["Titulo"] = "Todos os lanches";    //atribuindo valor na ViewData para ser recuperado na view List 
            //ViewData["Data"] = DateTime.Now;
            //ViewBag.TotalLanches = "Total Lanches:";    //atribuindo valor na ViewBag para ser recuperado na view List , viewbag eh mais usado, porem viewData um pouco mais rapido
            //ViewBag.TotalLanchesCount = lanches.Count();
            #endregion
            return View(await _produtoService.ObterListaProdutoPorCategoria(categoria).ConfigureAwait(false));
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Details(Guid lancheId) =>
             View(await _produtoService.ObterProdutoPorId(lancheId).ConfigureAwait(false));

        [AutoValidateAntiforgeryToken]
        //viewResult retorna espeficiamente uma view
        public async Task<ViewResult> Search(string palavraChave)
        {
            //~ significa raiz, e escolhendo a view que sera passada
            return View("~/Views/Lanche/List.cshtml", await _produtoService.PesquisarProduto(palavraChave).ConfigureAwait(false));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CarrinhoAtualizar(ProdutoCarrinhoAtualizarViewModel produtoAtualizarCarrinhoViewModel)
        {
            var produto = await _produtoService.ObterProdutoPorId(produtoAtualizarCarrinhoViewModel.ProdutoId).ConfigureAwait(false);
            produto.observacao = produtoAtualizarCarrinhoViewModel.Observacao;
            produto.TamanhoId = produtoAtualizarCarrinhoViewModel.TamanhoId;
            produto.quantidade = produtoAtualizarCarrinhoViewModel.Quantidade;
            return View("~/Views/Lanche/Details.cshtml", produto);
        }
    }
}