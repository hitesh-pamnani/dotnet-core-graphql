using Server.Data;
using Server.Models;
using Server.GraphQL.Types;

namespace Server.GraphQL.Mutations
{
    public class ProductMutation
    {
        public async Task<Product> CreateProduct(ProductContext context, ProductInput product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description ?? string.Empty,
                Price = product.Price,
                CreatedAt = DateTime.UtcNow
            };

            context.Products.Add(newProduct);
            await context.SaveChangesAsync();
            return newProduct;
        }

        public async Task<Product?> UpdateProduct(ProductContext context, int id, ProductInput product)
        {
            var existingProduct = await context.Products.FindAsync(id);
            if (existingProduct == null)
                return null;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description ?? string.Empty;
            existingProduct.Price = product.Price;

            await context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> DeleteProduct(ProductContext context, int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
                return false;

            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return true;
        }
    }
}