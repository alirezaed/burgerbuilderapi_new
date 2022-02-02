using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurgerBuilder.WebApi.ViewModels.Order
{
	public class GetAllOrdersViewModel
	{
		public string token { get; set; }
		public string sort_field { get; set; }
		public string sort_order { get; set; }
		public int page_index { get; set; }
		public int page_size { get; set; }

	}
}