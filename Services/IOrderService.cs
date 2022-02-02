using BurgerBuilder.WebApi.ViewModels.Order;
using BurgerBuilder.WebApi.ViewModels.User;

namespace BurgerBuilderApi.Services
{
    public interface IOrderService
    {
        AddOrderResultViewModel AddOrder(AddOrderViewModel addOrderViewModel);
        List<OrderViewModel> GetAllOrders();
        GetAllOrders GetAllOrders(GetAllOrdersViewModel getAllOrdersViewModel, int? userId = null);
        OrderViewModel GetOrder(GetOrderViewModel getOrderViewModel);
        RecoverStatusModel SaveComment(SaveCommentViewModel saveCommentViewModel);
    }
}