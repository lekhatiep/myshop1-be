﻿using AutoMapper;
using Domain.Entities.Catalog;

namespace Api.Dtos.OrderItems
{
    [AutoMap(typeof(OrderItem))]
    public class BaseOrderItemDto
    {
        public int ProductId { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double Quantity { get; set; }

    }
}
