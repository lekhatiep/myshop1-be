using Api.Dtos.Orders;
using Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.Orders
{
    public interface IOrderService
    {
        Task<int> Checkout(CreateOrderDto createOrderDto);
        Task<List<Order>> GetListHistoryOrderByUser(int userId);
        Task<int> ProcessCheckoutOrder(int userId);
    }
}
