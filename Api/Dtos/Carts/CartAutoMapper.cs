using Api.Dtos.CartItems;
using AutoMapper;
using Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Carts
{
    public class CartAutoMapper : Profile
    {
        public CartAutoMapper()
        {
            CreateMap<CreateCartDto, Cart>();
            CreateMap<CreateCartItemDto, CartItem>();
            CreateMap<UpdateCartItemDto, CartItem>();
        }
    }
}
