using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Services.Interfaces
{
    public interface IInformacoesUsuariosService
    {
        Task CriarUsuario(LoginViewModel registroVM);

        EditLoginViewModel GetUserEdit(string id);

        Task AtualizarUser(EditLoginViewModel editLoginViewModel);

    }
}
