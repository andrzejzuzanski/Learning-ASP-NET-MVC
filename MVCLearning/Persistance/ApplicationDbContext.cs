using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCLearning.Models;

namespace MVCLearning.Persistance
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Item> Items { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>(builder =>
            {
                builder
                .Property(i => i.Name)
                .IsRequired();

                builder
                .Property(i => i.Price)
                .IsRequired();

                builder
                .Property(i => i.Quantity)
                .IsRequired();

                builder
                .HasData(
                    new Item() { Id = 1, Name = "Test item", Quantity = 2 });
            });
        }
    }
}
