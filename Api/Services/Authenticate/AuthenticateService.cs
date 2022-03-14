using Api.Dtos.Identity;
using Api.Dtos.TokenDto;
using Api.Extensions;
using Api.Helper;
using Domain.Entities.Identity;
using Infastructure.Data;
using Infastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Authenticate
{
    public class AuthenticateService : IAuthenticateService
    {

        //private readonly IGenericRepository<User> _userRepository;
        //private readonly IGenericRepository<Role> _roleRepository;
        //private readonly IGenericRepository<RolePermission> _rolePermissionRepository;
        //private readonly IGenericRepository<Domain.Entities.Identity.Permission> _permissionRepository;
        private readonly AppDbContext _context;

        public AuthenticateService(
            //IGenericRepository<User> userRepository,
            //IGenericRepository<Role> roleRepository,
            //IGenericRepository<RolePermission> rolePermissionRepository,
            //IGenericRepository<Domain.Entities.Identity.Permission> permissionRepository,
            AppDbContext context
            )
        {
            //_userRepository = userRepository;
            //_roleRepository = roleRepository;
            //_rolePermissionRepository = rolePermissionRepository;
            //_permissionRepository = permissionRepository;
            _context = context;
        }
        public async Task<AuthReponseDto> Authenticate(LoginDto login)
        {

            string SECRET_KEY = "KeyOfMyshop10121994"; // in appsettings.json
            SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

            var queryUser = _context.Users;
            var queryRole = _context.Role;
            var queryRolePermission = _context.RolePermissions;
            var queryPermission = _context.Permissions;

            var user =  queryUser
                      .Include(x => x.UserRoles)
                      .ThenInclude(x => x.Role)
                      .WhereIf(!string.IsNullOrEmpty(login.UserName), x => x.UserName.ToLower().Contains(login.UserName.ToLower()))
                      .WhereIf(!string.IsNullOrEmpty(login.Password), x => x.Password.ToLower().Contains(login.Password.ToLower()))
                      .FirstOrDefault();

            var roles = new List<Role>();

            foreach (var userRole in user.UserRoles)
            {
                var role = queryRole.Where(x => x.Id == userRole.RoleId).FirstOrDefault();
                roles.Add(role);
            }

            //Also note that sercurity length should be > 256b
            //Tao chung chi + kieu ma hoa cho chu ky 
            var credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);

            //Finally create a Token
            //Header of JWT
            var header = new JwtHeader(credentials);

            //Token will be good for 1 minutes + refresh_token

            DateTime Expriry = DateTime.UtcNow.AddMinutes(9000);
            int ts = (int)(Expriry - new DateTime(1970, 1, 1)).TotalSeconds;

            //Some Payload that contain infomation about customer/client/user

            /* Options 1: Using payload */

            //var payLoad = new JwtPayload()
            //{
            //    { "sub" , "testSubject" },
            //    { "name", "user Josh" },
            //    { "email", "josh@gmail.com" },
            //    { "exp", ts },
            //    { "iss", "adminMyshop@gmail.com" },//The party generating the JWT
            //    { "aud", "adminMyshop@gmail.com" },//Address off resource
            //};

            /* Options 2: Using claim */
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            // var permisions = "Permission.Product.View Permission.Home.View";
            var permisions = string.Empty;

            //claims.Add(new Claim("Permission", permisions));
            var listPermissionName = new List<string>();

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));

                var rolePermission = queryRolePermission.Where(x=>x.RoleId == role.Id).ToList();

                //  .Where(x => x.RoleId == role.Id).ToList();
                if (rolePermission != null)
                {

                    foreach (var permission in rolePermission)
                    {
                        var pers = queryPermission.Where(x => x.Id == permission.PermissionId).SingleOrDefault();

                        if (!listPermissionName.Any(x => x.Contains(pers.Name)))
                        {
                            listPermissionName.Add(pers.Name);
                            permisions += pers.Name + " ";
                        }

                    }
                   
                }
            }

            claims.Add(new Claim("Permission", permisions.Trim()));

            var secToken = new JwtSecurityToken(
                AppSettings.ISSUER,
                AppSettings.AUDIENCE,
                claims,
                expires: DateTime.UtcNow.AddMinutes(9000),
                signingCredentials: credentials
            );

            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);//Sercurity Token

            return new AuthReponseDto
            {
                Token = tokenString,
                TokenType = "Bearer",
                TotalSecond = ts
            };
        }
    }
}
