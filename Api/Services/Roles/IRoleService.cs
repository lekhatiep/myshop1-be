using System.Threading.Tasks;

namespace Api.Services.Roles
{
    public interface IRoleService
    {
        Task AssignRoleDefault(string roleName, int userId);
    }
}
