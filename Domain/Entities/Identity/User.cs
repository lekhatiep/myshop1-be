using Domain.Base;
using System.Collections.Generic;

namespace Domain.Entities.Identity
{
    public class User : Entity<int>
    {
        private ICollection<UserRole> _userRoles;

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public ICollection<UserRole> UserRoles
        {
            get => _userRoles ??= new List<UserRole>();
            set => _userRoles = value;
        }
    }
}
