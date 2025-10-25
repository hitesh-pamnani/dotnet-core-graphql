using Client.Models;

namespace Client.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(string? search = null);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> CreateProductAsync(ProductInput productInput);
        Task<Product?> UpdateProductAsync(int id, ProductInput productInput);
        Task<bool> DeleteProductAsync(int id);
    }
}