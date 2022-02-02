using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurgerBuilder.WebApi.ViewModels.Order
{
	public class AddOrderViewModel
	{
		public string token { get; set; }
		public int meat { get; set; }
		public int salad { get; set; }
		public int cheese { get; set; }
		public int total_price { get; set; }
	}
}