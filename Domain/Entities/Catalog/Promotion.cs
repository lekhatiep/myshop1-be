using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Catalog
{
    public class Promotion : Entity<Guid>
    {
        public string Name { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool ApplyForAll { get; set; }

        public decimal DiscountPercent { get; set; }

        public decimal DiscountAmount { get; set; }

        public string Status { get; set; }

        public int ProductId { get; set; }

        public int ProductCategoryId { get; set; }


        public virtual Product Product { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
