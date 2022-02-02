using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurgerBuilder.WebApi.ViewModels.Order
{
	public class AddOrderResultViewModel:BaseStatusViewModel
	{
		public int order_number { get; set; }
	}
}