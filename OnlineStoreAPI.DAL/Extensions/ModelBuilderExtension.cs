using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Extensions
{
    internal static class ModelBuilderExtension
    {
        public static void FluentOptions(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOne(l => l.Parent)
                .WithMany(p => p.Childrens)
                .HasForeignKey(l => l.ParentId);

            modelBuilder.Entity<ItemPriceHistory>()
                .HasKey(l => l.Id);
        }
    }
}
