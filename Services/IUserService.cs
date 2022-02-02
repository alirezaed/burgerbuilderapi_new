using BurgerBuilder.WebApi.ViewModels.User;

namespace BurgerBuilderApi.Services
{
    public interface IUserService
    {
        string ConfirmMail(string email, string secretkey);
        RecoverStatusModel IsTokenValid(TokenViewModel tokenViewModel);
        LoginStatusViewModel Login(LoginViewModel loginViewModel);
        RecoverStatusModel RecoverPassword(RecoverEmailViewModel recoverEmailViewModel);
        SingUpStatusViewModel SignUp(SingUpViewModel singUpViewModel);
    }
}