using Api.Contanst.Catalogs;
using Api.Dtos.OrderItems;
using Api.Dtos.Orders;
using AutoMapper;
using Domain.Entities.Catalog;
using Infastructure.Repositories.Catalogs.OrderRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<int> Checkout(CreateOrderDto createOrderDto)
        {
            var orderItems = new List<OrderItemDto>();

            foreach (var item in createOrderDto.OrderItems)
            {
                orderItems.Add(new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                });
            }

            var order = _mapper.Map<OrderDto>(createOrderDto);
            order.Status = CatalogConst.OrderStatus.Processing;
            order.OrderItems = _mapper.Map<List<OrderItem>>(orderItems);

            await _orderRepository.Insert(order);
            await _orderRepository.Save();

            return order.Id;

        }

        public Task<List<OrderDto>> GetListHistoryOrderByUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
