using Microsoft.EntityFrameworkCore;
using ProductService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.SQLiteDB
{
    public class StoreDBContext:DbContext
    {
        public StoreDBContext():base()
        {}

        public DbSet<NewProductCommand> Products { get; set; }
    }
}
