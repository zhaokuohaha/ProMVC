using System.Data.Entity;
using SportStore.Domain.Entities;
namespace SportStore.Domain.Contrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
