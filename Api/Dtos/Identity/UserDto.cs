using AutoMapper;
using Domain.Entities.Identity;
using System.Collections.Generic;

namespace Api.Dtos.Identity
{
    [AutoMap(typeof(User))]
    public class UserDto
    {
        private IList<UserRole> _userRoles;
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public IList<UserRole> UserRoles
        {
            get => _userRoles ??= new List<UserRole>();
            set => _userRoles = value;
        }
    }
}
