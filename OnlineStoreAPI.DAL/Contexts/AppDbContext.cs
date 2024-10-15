using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.DAL.Extensions;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Contexts
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemPhoto> ItemPhotos { get; set; }
        public DbSet<ItemPriceHistory> ItemPriceHistories { get; set; }
        public DbSet<ItemProperty> ItemProperties { get; set; }
        public DbSet<ItemProperyValue> ItemProperyValues { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.FluentOptions();
            base.OnModelCreating(modelBuilder);
        }
    }
}
