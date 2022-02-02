using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurgerBuilder.WebApi.ViewModels.Order
{
	public class GetAllOrders
	{
		public List<OrderViewModel> list { get; set; }
		public int total_count { get; set; }

	}
}