using Microsoft.AspNetCore.Mvc;
using VL_VendasLanches.Controllers;
using VL_VendasLanches.Models;
using Microsoft.AspNetCore.Authorization;
using VL_VendasLanches.Services.Interfaces;

namespace VL_VendasLanches.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")] // tem que estar com perfi de admin
    public class AdminComplementosController : MainController
    {
        private readonly IComplementoService _complementoServices;

        public AdminComplementosController(IComplementoService complementoServices)
        {
            _complementoServices = complementoServices;
        }

        public async Task<IActionResult> Index() =>
           View(await _complementoServices.ObterComplementos().ConfigureAwait(false));

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var complemento = await _complementoServices.ObterComplementoPorId(id).ConfigureAwait(false);
            return View(complemento);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var complemento = await _complementoServices.ObterComplementoPorId(id).ConfigureAwait(false);
            return View(complemento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Complemento complemento)
        {
            if (!ModelState.IsValid)
            {
                return View(complemento);
            }

            if (await _complementoServices.VerificaNomeExiste(complemento).ConfigureAwait(false))
            {
                AdicionarErroValidacao($"Já existe um produto com o nome {complemento.Nome}.");
                return View(complemento);
            }

            await _complementoServices.CriarComplemento(complemento).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Complemento complemento)
        {
            if (!ModelState.IsValid)
            {
                return View(complemento);
            }

            await _complementoServices.AtualizarComplemento(complemento).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _complementoServices.DeletarComplemento(id).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

    }
}
