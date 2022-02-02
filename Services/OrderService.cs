using BurgerBuilder.WebApi.ViewModels.Order;
using BurgerBuilder.WebApi.ViewModels.User;
using BurgerBuilderApi.Database;
using BurgerBuilderApi.Mapper;
using System.Linq.Expressions;
using static BurgerBuilderApi.Tools.Helper;

namespace BurgerBuilderApi.Services
{
    public class OrderService : IOrderService
    {
        BBContext db;
        public OrderService(BBContext bBContext)
        {
            this.db = bBContext;
        }
        public AddOrderResultViewModel AddOrder(AddOrderViewModel addOrderViewModel)
        {
            AddOrderResultViewModel result;
            try
            {
                if (addOrderViewModel.total_price + addOrderViewModel.meat + addOrderViewModel.cheese + addOrderViewModel.salad + addOrderViewModel.salad == 0)
                {
                    return new AddOrderResultViewModel { message = "input is not correct", status = false };
                }

                if (5000 + addOrderViewModel.meat * 5000 + addOrderViewModel.salad * 2000 + addOrderViewModel.cheese * 4000 != addOrderViewModel.total_price)
                {
                    result = new AddOrderResultViewModel
                    {
                        status = false,
                        message = "total price is incorrect"
                    };
                    return result;
                }
                var datamodel = new OrderModel
                {
                    Cheese = addOrderViewModel.cheese,
                    Salad = addOrderViewModel.salad,
                    CreateDate = DateTime.Now,
                    Meat = addOrderViewModel.meat,
                    TotalPrice = addOrderViewModel.total_price,
                    Status = Enumerations.OrderStatus.Pending,
                    Comment = String.Empty,
                    Rate = 0
                };
                db.Orders.Add(datamodel);
                db.SaveChanges();

                if (true) /*validate token*/
                {
                    datamodel.UserId = 0;
                };

                int orderNumber = datamodel.Id;
                result = new AddOrderResultViewModel
                {
                    order_number = orderNumber,
                    status = true,
                    message = "successfully added"
                };
            }
            catch (Exception ex)
            {
                result = new AddOrderResultViewModel
                {
                    status = false,
                    message = "error on add order : " + ex.Message
                };
            }
            return result;
        }
        public List<OrderViewModel> GetAllOrders()
        {
            return db.Orders.ToViewModel().ToList();
        }
        public GetAllOrders GetAllOrders(GetAllOrdersViewModel getAllOrdersViewModel, int? userId = null)
        {
            Dictionary<string, Expression<Func<OrderModel, object>>> _orders = new Dictionary<string, Expression<Func<OrderModel, object>>>()
            {
                {"Cheese", x => x.Cheese}, //string
				{"Comment", x => x.Comment}, //string
				{"Rate", x => x.Rate}, //decimal
				{"Salad", x => x.Salad}, //decimal
				{"TotalPrice", x => x.TotalPrice }, //bool
				{"Status", x => x.Status}
            };
            var x = db.Orders.Where(c => !userId.HasValue || userId.Equals(c.UserId)).OrderBy(_orders[getAllOrdersViewModel.sort_field], getAllOrdersViewModel.sort_order == "asc")
                .Skip(getAllOrdersViewModel.page_index * getAllOrdersViewModel.page_size)
                .Take(getAllOrdersViewModel.page_size).ToList();
            return new GetAllOrders
            {
                list = x.ToViewModel().ToList(),
                total_count = db.Orders.Where(c => !userId.HasValue || userId.Equals(c.UserId)).Count()
            };
        }


        public OrderViewModel GetOrder(GetOrderViewModel getOrderViewModel)
        {
            return db.Orders.FirstOrDefault(c => c.Id.Equals(getOrderViewModel.order_number))?.ToViewModel();
        }

        public RecoverStatusModel SaveComment(SaveCommentViewModel saveCommentViewModel)
        {
            var order = db.Orders.FirstOrDefault(c => c.Id == saveCommentViewModel.order_number);
            if (order == null) throw new Exception("Order Not found");
            order.Comment = saveCommentViewModel.comment;
            db.SaveChanges();
            return new RecoverStatusModel { status = true, message = "" };
        }
    }
}
