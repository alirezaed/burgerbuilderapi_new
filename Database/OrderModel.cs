using BurgerBuilderApi.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace BurgerBuilderApi.Database
{
    public class OrderModel
    {
		[Key]
		public int Id { get; set; }
		public int UserId { get; set; }
		public DateTime CreateDate { get; set; }
		public OrderStatus Status { get; set; }
		public int Meat { get; set; }
		public int Salad { get; set; }
		public int Cheese { get; set; }
		public int TotalPrice { get; set; }
		public string Comment { get; set; }
		public int Rate { get; set; }
	}
}
