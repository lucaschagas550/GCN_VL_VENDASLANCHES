using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VL_VendasLanches.Controllers;
using VL_VendasLanches.Models;
using VL_VendasLanches.Services.Interfaces;

namespace VL_VendasLanches.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")] // tem que estar com perfi de admin
    public class AdminCategoriasController : MainController
    {
        private readonly ICategoriaService _categoriaService;

        public AdminCategoriasController(ICategoriaService categoriaService) =>
            _categoriaService = categoriaService;

        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _categoriaService.ObterCategorias().ConfigureAwait(false));
        
        [HttpGet]
        public async Task<IActionResult> Details(Guid id) =>
            View(await _categoriaService.ObterCategoriaPorId(id).ConfigureAwait(false));

        [HttpGet]
        public IActionResult Create() =>
          View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (!ModelState.IsValid) return View(categoria); 

            if (await _categoriaService.VerificaNomeExiste(categoria).ConfigureAwait(false))
            {
                AdicionarErroValidacao($"Já existe uma {nameof(Categoria)} com o nome {categoria.CategoriaNome}.");
                return View(categoria);
            }

            await _categoriaService.CriarCategoria(categoria).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) =>
            View(await _categoriaService.ObterCategoriaPorId(id).ConfigureAwait(false));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categoria categoria)
        {
            if (!ModelState.IsValid) return View(categoria);

            if (await _categoriaService.VerificaNomeExiste(categoria).ConfigureAwait(false))
            {
                AdicionarErroValidacao($"Já existe uma {nameof(Categoria)} com o nome {categoria.CategoriaNome}.");
                return View(categoria);
            }

            await _categoriaService.AtualizarCategoria(categoria).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id) =>
            View(await _categoriaService.ObterCategoriaPorId(id).ConfigureAwait(false));

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _categoriaService.DeletarCategoria(id).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }
    }
}