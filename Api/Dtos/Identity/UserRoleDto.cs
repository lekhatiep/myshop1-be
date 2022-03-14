using AutoMapper;
using Domain.Entities.Identity;

namespace Api.Dtos.Identity
{
    [AutoMap(typeof(UserRole))]
    public class UserRoleDto
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
