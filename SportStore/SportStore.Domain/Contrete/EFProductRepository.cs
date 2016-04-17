using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using System.Linq;

namespace SportStore.Domain.Contrete
{
    public class EFProductRepository : IProuctRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Product> Products { get { return context.Products; } }
    }
}
