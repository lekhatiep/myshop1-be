using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Contanst
{
    public static class NamePermissions
    {
        public static List<string> GenPermissionForModule(string moduleName)
        {
            return new List<string>()
            {
                $"Permission.{moduleName}.Create",
                $"Permission.{moduleName}.View",
                $"Permission.{moduleName}.Edit",
                $"Permission.{moduleName}.Delete",
            };
        }

        //Module Products
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }
    }
}
