using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VL_VendasLanches.Services.Interfaces;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Controllers
{
    public class AccountController : MainController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IInformacoesUsuariosService _informacoesUsuariosService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
             IInformacoesUsuariosService informacoesUsuariosService,
               IHttpContextAccessor httpContextAccessor)
        {
            _informacoesUsuariosService = informacoesUsuariosService;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Login(string returnUrl)
        {
            return View(new RegistrarViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(RegistrarViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Falha ao realizar o login!!"); //Erro para vc:summary
            return View(loginVM);
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registroVM.email };
                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if (result.Succeeded)
                {
                    var userid = await _userManager.FindByNameAsync(registroVM.email);
                    registroVM.idaspuser = userid.Id;
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    await _informacoesUsuariosService.CriarUsuario(registroVM);
                    await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");
                }
            }
            return View(registroVM);
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult MudarSenha()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MudarSenha(SenhaViewModel senhaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(senhaViewModel);
            }
            var id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.ChangePasswordAsync(user, senhaViewModel.Passwordold, senhaViewModel.Password);
            return View(senhaViewModel);
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit()
        {
            var id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _informacoesUsuariosService.GetUserEdit(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditLoginViewModel editLoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editLoginViewModel);
            }
            await _informacoesUsuariosService.AtualizarUser(editLoginViewModel).ConfigureAwait(false);
            return View(editLoginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //Zerando o usuario e removendo todos os objetos da sessao
            HttpContext.Session.Clear();
            HttpContext.User = null;

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
