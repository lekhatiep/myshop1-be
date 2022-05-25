using Api.Dtos.Identity;
using Domain.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.Users
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<List<string>> GetAllPermissionByUserId(int id);
        Task<bool> IsExistsUser(CreateUserDto createUserDto);
    }
}
