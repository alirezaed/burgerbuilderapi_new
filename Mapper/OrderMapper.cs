using BurgerBuilder.WebApi.ViewModels.Order;
using BurgerBuilderApi.Database;
using BurgerBuilderApi.Enumerations;

namespace BurgerBuilderApi.Mapper
{
	public static class OrderMapper
	{
		#region ToDateModel
		public static OrderModel ToDataModel(this AddOrderViewModel addOrderViewModel)
		{
			return new OrderModel
			{
				Cheese = addOrderViewModel.cheese,
				Comment = "",
				CreateDate = DateTime.Now,
				Meat = addOrderViewModel.meat,
				Rate = 0,
				Salad = addOrderViewModel.salad,
				Status = OrderStatus.Pending,
				TotalPrice = addOrderViewModel.total_price,
				UserId = 0
			};

		}
		#endregion

		#region ToViewModel
		public static OrderViewModel ToViewModel(this OrderModel OrderModel)
		{
			return new OrderViewModel
			{
				cheese = OrderModel.Cheese,
				comment = OrderModel.Comment,
				create_date = OrderModel.CreateDate,
				meat = OrderModel.Meat,
				rate = OrderModel.Rate,
				salad = OrderModel.Salad,
				status = Enum.GetName(typeof(OrderStatus), ((OrderStatus)OrderModel.Status)),
				total_price = OrderModel.TotalPrice,
				order_number = OrderModel.Id
			};
		}
		public static IEnumerable<OrderViewModel> ToViewModel(this IEnumerable<OrderModel> OrderModel)
		{
			return OrderModel.Select(c => c.ToViewModel());
		}
		#endregion
	}
}
