using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VL_VendasLanches.Controllers;
using VL_VendasLanches.Models;
using VL_VendasLanches.Services;
using VL_VendasLanches.Services.Interfaces;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")] // tem que estar com perfi de admin
    public class AdminLanchesController : MainController
    {
        private readonly ITamanhoService _tamanhoService;
        private readonly IProdutoService _produtoService;
        private readonly IComplementoService _complementoService;

        public AdminLanchesController(ITamanhoService tamanhoService, IProdutoService produtoService, IComplementoService complementoService)
        {
            _tamanhoService = tamanhoService;
            _produtoService = produtoService;
            _complementoService = complementoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _produtoService.ObterListaProdutoComCategoriaComTamanho().ConfigureAwait(false));

        [HttpGet]
        public async Task<IActionResult> Details(Guid id) =>
            View(await _produtoService.ObterProdutoComCategoriaComTamanhoPorId(id).ConfigureAwait(false));

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["TodosTamanhos"] = await _produtoService.ObterTamanhos().ConfigureAwait(false);
            ViewData["Complementos"] = await _complementoService.ObterComplementos().ConfigureAwait(false);
            await ObterCategorias().ConfigureAwait(false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
            {
                await RecarregarCriacaoProdutoErroValidacao(produtoViewModel).ConfigureAwait(false);
                return View(produtoViewModel);
            }

            if (await _produtoService.VerificaNomeExiste(produtoViewModel).ConfigureAwait(false))
            {
                AdicionarErroValidacao($"Já existe um produto com o nome {produtoViewModel.Nome}.");
                await RecarregarCriacaoProdutoErroValidacao(produtoViewModel).ConfigureAwait(false);
                return View(produtoViewModel);
            }

            await _produtoService.CriarProduto(produtoViewModel).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await _produtoService.ObterProdutoViewModelParaAtualizar(id).ConfigureAwait(false);
            await ObterCategorias(produtoViewModel).ConfigureAwait(false);
            return View(produtoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
            {
                await ObterCategorias(produtoViewModel).ConfigureAwait(false);
                produtoViewModel.TodosTamanhos = await _tamanhoService.ObterTamanhos().ConfigureAwait(false);

                foreach (var tamanhoSelecionado in produtoViewModel.TamanhosSelecionados)
                {
                    var tamanhoProduto = new TamanhoProduto();
                    tamanhoProduto.TamanhoId = Guid.Parse(tamanhoSelecionado);
                    produtoViewModel.Tamanhos.Add(tamanhoProduto);
                }
                return View(produtoViewModel);
            }

            await _produtoService.AtualizarProduto(produtoViewModel).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await _produtoService.ObterProdutoComCategoriaComTamanhoPorId(id).ConfigureAwait(false);
            return View(produtoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _produtoService.DeletarProduto(id).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        private async Task ObterCategorias() =>
            ViewData["CategoriaId"] = new SelectList(await _produtoService.ObterCategorias().ConfigureAwait(false), "Id", "CategoriaNome");

        private async Task ObterCategorias(ProdutoViewModel produtoViewModel) =>
            ViewData["CategoriaId"] = new SelectList(await _produtoService.ObterCategorias().ConfigureAwait(false), "Id", "CategoriaNome", produtoViewModel.CategoriaId);

        private async Task RecarregarCriacaoProdutoErroValidacao(ProdutoViewModel produtoViewModel)
        {
            await ObterCategorias().ConfigureAwait(false);
            if (produtoViewModel.TamanhosSelecionados.Any())
            {
                foreach (var item in produtoViewModel.TamanhosSelecionados)
                {
                    var produto = new TamanhoProduto();
                    produto.TamanhoId = Guid.Parse(item);
                    produtoViewModel.Tamanhos.Add(produto);
                }
            }

            if (produtoViewModel.ComplementosSelecionados.Any())
            {
                foreach (var item in produtoViewModel.ComplementosSelecionados)
                {
                    var complemento = new ComplementoProduto();
                    complemento.Id = Guid.Parse(item);
                    produtoViewModel.Complementos.Add(complemento);
                }
            }

            ViewData["TodosTamanhos"] = await _produtoService.ObterTamanhos().ConfigureAwait(false);
            ViewData["Complementos"] = await _complementoService.ObterComplementos().ConfigureAwait(false);
        }
    }
}