using BurgerBuilder.WebApi.ViewModels.User;
using BurgerBuilderApi.Database;
using BurgerBuilderApi.Tools;

namespace BurgerBuilderApi.Services
{
    public class UserService : IUserService
    {
        BBContext db;
        SecurityHelper securityHelper;
        public UserService(BBContext bBContext)
        {
            this.db = bBContext;
            this.securityHelper = new SecurityHelper();
        }
        public LoginStatusViewModel Login(LoginViewModel loginViewModel)
        {
            var result = db.Users.FirstOrDefault(c => c.UserName.Equals(loginViewModel.username) && c.Password.Equals(loginViewModel.password));
            if (result != null)
            {
                if (result.Confirmed)
                {
                    string refreshToken = Guid.NewGuid().ToString();
                    result.RefreshToken = refreshToken;
                    db.SaveChanges();
                    string token = securityHelper.GetToken(result.UserName, result.FullName, refreshToken);
                    return new LoginStatusViewModel { status = true, message = token };
                }
                return new LoginStatusViewModel { status = false, message = "your registration is not confirmed.please check your email for registration link" };
            }
            return new LoginStatusViewModel { status = false, message = "invalid user pass" };
        }
        public SingUpStatusViewModel SignUp(SingUpViewModel singUpViewModel)
        {
            var result = new SingUpStatusViewModel();
            if (db.Users.Any(c => c.Email.Equals(singUpViewModel.email)))
            {
                result = new SingUpStatusViewModel { status = false, message = "you already joined. if you forget your password please recover it" };
                return result;
            }
            UserModel saveModel = new UserModel
            {
                UserName = singUpViewModel.username,
                Password = singUpViewModel.password,
                CreateDate = DateTime.Now,
                Confirmed = false,
                Email = singUpViewModel.email,
                FullName = singUpViewModel.fullname,
                RefreshToken = Guid.NewGuid().ToString(),
                SecretKey = Guid.NewGuid().ToString()
            };
            db.Users.Add(saveModel);
            db.SaveChanges();
            MailHelper mailHelper = new MailHelper();
            string registrationLink = $"http://aedalat.ir/User/ConfirmMail?mail={saveModel.Email}&secretkey={saveModel.SecretKey}";
            string mailContent = "Please click below link to confirm your registration:";
            mailContent += Environment.NewLine;
            mailContent += registrationLink;
            mailHelper.SendMail("Confirm Registration", mailContent, saveModel.Email);
            result = new SingUpStatusViewModel { status = true, message = "check your email for confirm" };
            return result;
        }
        public RecoverStatusModel RecoverPassword(RecoverEmailViewModel recoverEmailViewModel)
        {
            var user = db.Users.FirstOrDefault(c => c.Email.Equals(recoverEmailViewModel.email));
            if (user == null)
            {
                return new RecoverStatusModel { message = "your email does not found. please sign up.", status = true };
            }

            new MailHelper().SendMail("Recover Password", $"Your password is : {user.Password}", user.Email);
            return new RecoverStatusModel { message = "your password was sent to your email address", status = false };
        }
        public RecoverStatusModel IsTokenValid(TokenViewModel tokenViewModel)
        {
            var isValid = securityHelper.ValidateToken(tokenViewModel.token);
            if (isValid)
            {
                return new RecoverStatusModel { message = tokenViewModel.token, status = true };
            }
            return new RecoverStatusModel { message = "invalid token", status = false };
        }
        public string ConfirmMail(string email, string secretkey)
        {
            var x = db.Users.FirstOrDefault(c => c.SecretKey.Equals(secretkey) && c.Email.Equals(email));
            if (x != null)
            {
                x.Confirmed = true;
                db.SaveChanges();
                return "confirmed";
            }
            return "not confirmed";
        }
    }
}
