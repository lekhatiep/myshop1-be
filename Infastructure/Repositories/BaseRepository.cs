using Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Repositories
{
    public class BaseRepository<T> where T : class
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> table;
        public BaseRepository(AppDbContext context)
        {
            this.context = context;
            this.table = context.Set<T>();
        }
    }
}
