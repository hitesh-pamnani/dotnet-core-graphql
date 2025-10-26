using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.GraphQL.Queries
{
    public class ProductQuery
    {
        public async Task<List<Product>> GetProducts(ProductContext context, string? search = null)
        {
            var query = context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Product?> GetProduct(ProductContext context, int id)
        {
            return await context.Products.FindAsync(id);
        }
    }
}