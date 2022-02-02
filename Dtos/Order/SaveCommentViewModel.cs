using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurgerBuilder.WebApi.ViewModels.Order
{
	public class SaveCommentViewModel
	{
		public int order_number { get; set; }
		public string comment { get; set; }
	}
}