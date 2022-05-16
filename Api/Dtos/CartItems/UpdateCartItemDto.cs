using Domain.Base;
using System;

namespace Api.Dtos.CartItems
{
    public class UpdateCartItemDto : BaseCartItemDto, IEntity<Guid>
    {
        public Guid Id { get ; set ; }
    }
}
