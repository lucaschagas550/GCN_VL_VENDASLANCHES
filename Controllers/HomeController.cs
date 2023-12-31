﻿using Microsoft.AspNetCore.Mvc;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Controllers;

public class HomeController : Controller
{
    private readonly IProdutoRepository _lancheRepositorys;

    public HomeController(IProdutoRepository lancheRepositorys) =>
        _lancheRepositorys = lancheRepositorys;

    [AutoValidateAntiforgeryToken]
    public IActionResult Index()
    {
        #region
        //TempData["Nome"] = "Lucas"; 
        // TempData é um valor temporario que existe até ser recuperado, pode ser enviado para outra controller ou action, view para controller ou inverso
        #endregion

        var homeViewModel = new HomeViewModel
        {
            LanchesPreferidos = _lancheRepositorys.LanchesPreferidos,
        };

        return View(homeViewModel);
    }

    [Route("sistema-indisponivel")]
    public IActionResult SistemaIndisponivel()
    {
        var modelErro = new ErrorViewModel
        {
            Mensagem = "O sistema está temporariamente indisponível, isto pode ocorrer em momentos de sobrecarga de usuários.",
            Titulo = "Sistema indisponível.",
            ErroCode = 500
        };

        return View("Error", modelErro);
    }

    [HttpGet]
    [Route("erro/{id:length(3,3)}")]
    public IActionResult Error(int id)
    {
        var modelErro = new ErrorViewModel();

        if (id == 500)
        {
            modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
            modelErro.Titulo = "Ocorreu um erro!";
            modelErro.ErroCode = id;
        }
        else if (id == 404)
        {
            modelErro.Mensagem =
                "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
            modelErro.Titulo = "Ops! Página não encontrada.";
            modelErro.ErroCode = id;
        }
        else if (id == 403)
        {
            modelErro.Mensagem = "Você não tem permissão para fazer isto.";
            modelErro.Titulo = "Acesso Negado";
            modelErro.ErroCode = id;
        }
        else
        {
            return StatusCode(404);
        }

        return View("Error", modelErro);
    }
}