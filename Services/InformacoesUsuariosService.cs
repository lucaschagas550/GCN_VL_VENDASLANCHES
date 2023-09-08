using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.Services.Interfaces;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Services
{
    public class InformacoesUsuariosService : IInformacoesUsuariosService
    {
        private readonly IInformacoesUsuariosRepository _informacoesUsuariosRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public InformacoesUsuariosService(IInformacoesUsuariosRepository informacoesUsuariosRepository,
          UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _informacoesUsuariosRepository = informacoesUsuariosRepository;
            _mapper = mapper;

        }

        public async Task AtualizarUser(EditLoginViewModel editLoginViewModel)
        {
            var user = _mapper.Map<InformacoesUsuarios>(editLoginViewModel);
            await _informacoesUsuariosRepository.Atualizar(user).ConfigureAwait(false);
        }

        public async Task CriarUsuario(LoginViewModel registroVM)
        {
            var user = _mapper.Map<InformacoesUsuarios>(registroVM);
            await _informacoesUsuariosRepository.Adicionar(user).ConfigureAwait(false);
        }

        public EditLoginViewModel GetUserEdit(string id)
        {
            var user = _informacoesUsuariosRepository.IdAspuser(id);
            var userview = _mapper.Map<EditLoginViewModel>(user);
            return userview;
        }

    }
}
