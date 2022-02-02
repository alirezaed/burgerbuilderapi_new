using System.ComponentModel.DataAnnotations;

namespace BurgerBuilderApi.Database
{
    public class UserModel
    {
		[Key]
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public DateTime CreateDate { get; set; }
		public bool Confirmed { get; set; }
		public string SecretKey { get; set; }
		public string RefreshToken { get; set; }
	}
}
