using Api.Dtos.CartItems;
using Api.Dtos.Carts;
using Api.Dtos.Categories;
using Api.Dtos.Products;
using Domain.Common.Paging;
using Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Carts
{
    public interface ICartService
    {
        Task<List<CartItemDto>> AddToCart(CreateCartItemDto createCartItem);

        Task<int> CreateNewCart(CreateCartDto createCartDto);

        Task<int> CheckUserExistCart(int cartId);

        Task<List<CartItemDto>> GetUserListCartItem(int cartId);

        Task<Cart> GetCartUserById(int userId);
        
    }
}
