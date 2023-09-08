using Microsoft.AspNetCore.Mvc;
using VL_VendasLanches.Repositories.Interfaces;

namespace VL_VendasLanches.Components
{
    [ViewComponent]
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaMenu(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categorias = await _categoriaRepository.ObterTodos().ConfigureAwait(false);
            return View(categorias);
        }
    }
}
