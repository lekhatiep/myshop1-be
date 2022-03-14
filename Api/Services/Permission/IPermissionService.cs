using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Permission
{
    public interface IPermissionService
    {
        List<string> GetAllPermissionByRoleId(int id);
    }
}
