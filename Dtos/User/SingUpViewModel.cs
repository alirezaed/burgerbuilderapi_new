using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurgerBuilder.WebApi.ViewModels.User
{
	public class SingUpViewModel
	{
		public string username { get; set; }
		public string password { get; set; }
		public string fullname { get; set; }
		public string email { get; set; }
	}
}