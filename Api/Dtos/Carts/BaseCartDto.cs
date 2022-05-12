using AutoMapper;
using Domain.Entities.Catalog;

namespace Api.Dtos.Carts
{
    [AutoMap(typeof(Cart))]
    public class BaseCartDto
    {
    }
}
