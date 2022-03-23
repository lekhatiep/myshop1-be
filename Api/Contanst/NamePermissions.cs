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
            public const string View = "Permission.Products.View";
            public const string Create = "Permission.Products.Create";
            public const string Edit = "Permission.Products.Edit";
            public const string Delete = "Permission.Products.Delete";
        }
    }
}
