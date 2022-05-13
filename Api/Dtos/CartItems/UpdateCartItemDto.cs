using Domain.Base;

namespace Api.Dtos.CartItems
{
    public class UpdateCartItemDto : BaseCartItemDto, IEntity<int>
    {
        public int Id { get ; set ; }
    }
}
