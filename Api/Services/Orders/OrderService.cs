using Api.Contanst.Catalogs;
using Api.Dtos.OrderItems;
using Api.Dtos.Orders;
using Api.Services.Carts;
using AutoMapper;
using Domain.Entities.Catalog;
using Infastructure.Repositories.Catalogs.CartRepos;
using Infastructure.Repositories.Catalogs.OrderItemRepos;
using Infastructure.Repositories.Catalogs.OrderRepos;
using Infastructure.Repositories.ProductRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        private readonly ICartRepository _cartRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IMapper mapper,
            ICartService cartService,
            ICartRepository cartRepository,
            IOrderItemRepository orderItemRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cartService = cartService;
            _cartRepository = cartRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
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

        public async Task<List<OrderItemDto>> GetListHistoryOrderByUser(int userId, string status)
        {
            var queryOrderItem = await (from o in _orderRepository.List()
                                   join oi in _orderItemRepository.List() on o.Id equals oi.OrderId into oio
                                   from oi in oio.DefaultIfEmpty()
                                   join p in _productRepository.List()
                                               .Include(x => x.ProductImages.Where(x => x.IsDefault == true && x.IsDelete)) on oi.ProductId equals p.Id
                                   where o.UserId == userId && o.Status.ToLower().Contains(status.ToLower().Trim()) && o.IsDelete == false && o.IsDelete == false
                                   

                                   select new { oi, p }
                             )

                             .Select(x => new OrderItemDto
                             {
                                 Id = x.oi.Id, 
                                 ProductId = x.p.Id,
                                 ImgPath = x.p.ProductImages.FirstOrDefault().ImagePath,
                                 Title = x.p.Title,
                                 Price = x.oi.Price,
                                 Quantity = x.oi.Quantity,
                                 Total = x.oi.Quantity * Convert.ToDouble(x.p.Price),
        
                             }).ToListAsync();

            return queryOrderItem;
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
                    Discount = item.Discount, //Discount supplier
                    CreateTime = DateTime.Now,
                    Price = item.Price,
                    Total = subTotal
                });
            }

            createOrderDto.OrderItems = orderItems;
            createOrderDto.GrandTotal = (total + shipping - discountShop);

            var order = _mapper.Map<Order>(createOrderDto);
            order.Status = CatalogConst.OrderStatus.Processing;
            order.OrderItems = _mapper.Map<List<OrderItem>>(orderItems);
            order.UserId = cart.UserId;
            order.CreateTime = DateTime.Now;

            await _orderRepository.Insert(order);
            await _orderRepository.Save();

            //Update cart
            cart.Status = CatalogConst.CartStatus.SUCCESS;
            await _cartRepository.Update(cart, cart.Id);

            return order.Id;
        }
    }
}
