using Api.Dtos.Orders;
using AutoMapper;
using Domain.Entities.Catalog;

namespace Api.Dtos.OrderItems
{
    public class OrderAutoMapper : Profile
    {
        public OrderAutoMapper()
        {
            CreateMap<CreateOrderDto, Order>();
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
