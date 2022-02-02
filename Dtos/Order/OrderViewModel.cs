using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurgerBuilder.WebApi.ViewModels.Order
{
	public class OrderViewModel
	{
		public int order_number { get; set; }
		public DateTime create_date { get; set; }
		public string status { get; set; } 
		public int meat { get; set; }
		public int salad { get; set; }
		public int cheese { get; set; }
		public int total_price { get; set; }
		public string comment { get; set; }
		public int rate { get; set; }
	}
}