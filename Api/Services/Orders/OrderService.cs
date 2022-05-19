using Api.Contanst.Catalogs;
using Api.Dtos.OrderItems;
using Api.Dtos.Orders;
using Api.Services.Carts;
using AutoMapper;
using Domain.Entities.Catalog;
using Infastructure.Repositories.Catalogs.CartRepos;
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
        private readonly ICartService _cartService;
        private readonly ICartRepository _cartRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IMapper mapper,
            ICartService cartService,
            ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cartService = cartService;
            _cartRepository = cartRepository;
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

            var order = _mapper.Map<Order>(createOrderDto);
            order.Status = CatalogConst.OrderStatus.Processing;
            order.OrderItems = _mapper.Map<List<OrderItem>>(orderItems);

            await _orderRepository.Insert(order);
            await _orderRepository.Save();

            return order.Id;

        }

        public Task<List<Order>> GetListHistoryOrderByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ProcessCheckoutOrder(int userId)
        {
            var cart = await _cartService.GetCartUserById(userId);
            var listCart = await _cartService.GetUserListCartItem(cart.Id);

            var orderItems = new List<OrderItemDto>();
            var createOrderDto = new CreateOrderDto();


            double subTotal = 0;
            double total = 0;
            double grandTotal = 0;
            double shipping = 0;
            double discountShop = 0; //Discount of Shop

            foreach (var item in listCart)
            {
                subTotal += Convert.ToDouble(item.Quantity * item.Price);
                total += (subTotal - item.Discount);

                orderItems.Add(new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Discount = item.Discount //Discount supplier
                });
            }

            createOrderDto.OrderItems = orderItems;
            createOrderDto.GrandTotal = (total + shipping - discountShop);

            var order = _mapper.Map<Order>(createOrderDto);
            order.Status = CatalogConst.OrderStatus.Processing;
            order.OrderItems = _mapper.Map<List<OrderItem>>(orderItems);
            order.UserId = cart.UserId;

            await _orderRepository.Insert(order);
            await _orderRepository.Save();

            //Update cart
            cart.Status = CatalogConst.CartStatus.SUCCESS;
            await _cartRepository.Update(cart, cart.Id);

            return order.Id;
        }
    }
}
