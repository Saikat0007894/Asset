using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Data
{
    public class AssetContext : DbContext
    {
       
            public AssetContext(DbContextOptions<AssetContext> options) : base(options)
        {

        }
        public DbSet<EmployeeTable> Employee { get; set; }
        public DbSet<OrderTable> Order { get; set; }
        public DbSet<SystemTable> System { get; set; }
    }
}
