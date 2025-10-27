using Client;
using Client.Models;

namespace Client.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsClient _client;
    private readonly ILogger<ProductsService> _logger;

    public ProductsService(IProductsClient client, ILogger<ProductsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(string? search = null)
    {
        try
        {
            var result = await _client.GetProducts.ExecuteAsync(search);
            if (result.Errors?.Any() == true)
                throw new Exception($"Failed to fetch products: {string.Join(", ", result.Errors.Select(e => e.Message))}");
            
            if (result.Data == null)
                throw new Exception("No data returned from GraphQL query");

            return result.Data.Products?.Select(MapToProduct) ?? Enumerable.Empty<Product>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching products with search: {Search}", search);
            throw;
        }
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        try
        {
            var result = await _client.GetProduct.ExecuteAsync(id);
            if (result.Errors?.Any() == true)
                throw new Exception($"Failed to fetch product {id}: {string.Join(", ", result.Errors.Select(e => e.Message))}");
            
            if (result.Data == null)
                throw new Exception("No data returned from GraphQL query");

            return result.Data.Product != null ? MapToProduct(result.Data.Product) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching product with ID: {Id}", id);
            throw;
        }
    }

    public async Task<Product?> CreateProductAsync(ProductInput productInput)
    {
        try
        {
            var result = await _client.CreateProduct.ExecuteAsync(productInput);
            if (result.Errors?.Any() == true)
                throw new Exception($"Failed to create product: {string.Join(", ", result.Errors.Select(e => e.Message))}");
            
            if (result.Data == null)
                throw new Exception("No data returned from GraphQL mutation");

            return result.Data.CreateProduct != null ? MapToProduct(result.Data.CreateProduct) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product: {@Product}", productInput);
            throw;
        }
    }

    public async Task<Product?> UpdateProductAsync(int id, ProductInput productInput)
    {
        try
        {
            var result = await _client.UpdateProduct.ExecuteAsync(id, productInput);
            if (result.Errors?.Any() == true)
                throw new Exception($"Failed to update product {id}: {string.Join(", ", result.Errors.Select(e => e.Message))}");
            
            if (result.Data == null)
                throw new Exception("No data returned from GraphQL mutation");

            return result.Data.UpdateProduct != null ? MapToProduct(result.Data.UpdateProduct) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product {Id}: {@Product}", id, productInput);
            throw;
        }
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        try
        {
            var result = await _client.DeleteProduct.ExecuteAsync(id);
            if (result.Errors?.Any() == true)
                throw new Exception($"Failed to delete product {id}: {string.Join(", ", result.Errors.Select(e => e.Message))}");
            
            if (result.Data == null)
                throw new Exception("No data returned from GraphQL mutation");

            return result.Data.DeleteProduct == true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product with ID: {Id}", id);
            throw;
        }
    }

    private static Product MapToProduct(dynamic product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        CreatedAt = ((DateTimeOffset)product.CreatedAt).DateTime
    };
}