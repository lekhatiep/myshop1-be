using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.CartItems
{
    public class CartItemDto : BaseCartItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImgPath { get; set; }
    }
}
