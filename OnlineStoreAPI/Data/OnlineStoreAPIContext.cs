using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.Data
{
    public class OnlineStoreAPIContext : DbContext
    {
        public OnlineStoreAPIContext (DbContextOptions<OnlineStoreAPIContext> options)
            : base(options)
        {
        }

        public DbSet<OnlineStoreAPI.Domain.Entities.Item> Item { get; set; } = default!;
    }
}
