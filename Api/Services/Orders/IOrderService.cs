﻿using Api.Dtos.OrderItems;
using Api.Dtos.Orders;
using Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.Orders
{
    public interface IOrderService
    {
        Task<int> Checkout(CreateOrderDto createOrderDto);
        Task<List<OrderItemDto>> GetListHistoryOrderByUser(int userId, string status);
        Task<int> ProcessCheckoutOrder(int userId);
    }
}
