using Domain.Entities.Identity;
using Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Users
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<string>> GetAllPermissionByUserId(int id)
        {
            var userQuery = _context.Users;
            var roleQuery = _context.Role;
            var permissionQuery = _context.Permissions;

            var role = from r in roleQuery
                       join ur in _context.UserRoles on r.Id equals ur.RoleId
                       join u in userQuery on ur.UserId equals u.Id
                       where u.Id == id
                       select r;

            if (role is null)
            {
                return null;
            }

            var rolePermission = from r in role
                                 join rp in _context.RolePermissions on r.Id equals rp.RoleId
                                 select rp;

            var permission = await (from rp in rolePermission
                                    join p in permissionQuery on rp.PermissionId equals p.Id
                              select p).ToListAsync();

            var listPermission = permission.Select(x => x.Name).ToList();

            return listPermission;
        }

        public async Task<User> GetUserById(int id)
        {
            var userQuery = _context.Users;
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();

            return user;
        }
    }
}
