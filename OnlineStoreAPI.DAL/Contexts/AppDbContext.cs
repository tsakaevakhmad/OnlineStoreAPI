using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Contexts
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ItemPriceHistory> ItemPriceHistories { get; set; }
        public DbSet<ItemCharacteristic> ItemCharacteristics { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CharacteristicValue> CharacteristicValues { get; set; }
        public DbSet<Characteristics> Characteristics { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
