using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Audit
{
    public interface IAudit
    {
        public DateTime CreateTime { get; set; }

        public DateTime ModifyTime { get; set; }

        public bool IsDelete { get; set; }
    }
}
