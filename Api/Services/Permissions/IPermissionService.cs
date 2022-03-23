using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Permissions
{
    public interface IPermissionService
    {
        Task<List<string>> GetAllPermissionByRoleId(int id);
    }
}
