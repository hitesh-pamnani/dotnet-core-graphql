using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Client.Models;
using Client.Responses;

namespace Client.Services
{
    public class ProductsService : IProductsService, IDisposable
    {
        private readonly GraphQLHttpClient _client;
        private readonly ILogger<ProductsService> _logger;

        public ProductsService(IConfiguration configuration, ILogger<ProductsService> logger)
        {
            var endpoint = configuration["GraphQL:Endpoint"] ?? throw new ArgumentNullException("GraphQL endpoint is not configured.");
            
            var options = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(endpoint)
            };
            
            _client = new GraphQLHttpClient(options, new SystemTextJsonSerializer());
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(string? search = null)
        {
            try
            {
                var query = @"
                    query GetProducts($search: String) {
                        products(search: $search) {
                            id
                            name
                            description
                            price
                            createdAt
                        }
                    }";

                var request = new GraphQLRequest
                {
                    Query = query,
                    Variables = new Dictionary<string, object>
                    {
                        ["search"] = search ?? ""
                    }
                };

                var response = await _client.SendQueryAsync<ProductsResponse>(request);

                if (response.Errors != null && response.Errors.Length > 0)
                {
                    var errorMessage = string.Join(", ", response.Errors.Select(e => e.Message));
                    _logger.LogError("GraphQL errors: {Errors}", errorMessage);
                    throw new Exception($"GraphQL Error: {errorMessage}");
                }

                return response.Data?.Products ?? Enumerable.Empty<Product>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products");
                throw;
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                var query = @"
                    query GetProduct($id: Int!) {
                        product(id: $id) {
                            id
                            name
                            description
                            price
                            createdAt
                        }
                    }";

                var request = new GraphQLRequest
                {
                    Query = query,
                    Variables = new Dictionary<string, object>
                    {
                        ["id"] = id
                    }
                };

                var response = await _client.SendQueryAsync<ProductResponse>(request);

                if (response.Errors != null && response.Errors.Length > 0)
                {
                    var errorMessage = string.Join(", ", response.Errors.Select(e => e.Message));
                    _logger.LogError("GraphQL errors: {Errors}", errorMessage);
                    throw new Exception($"GraphQL Error: {errorMessage}");
                }

                return response.Data?.Product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching product with ID {Id}", id);
                throw;
            }
        }

        public async Task<Product?> CreateProductAsync(ProductInput productInput)
        {
            try
            {
                var mutation = @"
                    mutation CreateProduct($product: ProductInput!) {
                        createProduct(product: $product) {
                            id
                            name
                            description
                            price
                            createdAt
                        }
                    }";

                var request = new GraphQLRequest
                {
                    Query = mutation,
                    Variables = new Dictionary<string, object>
                    {
                        ["product"] = productInput
                    }
                };

                var response = await _client.SendMutationAsync<CreateProductResponse>(request);
                
                if (response.Errors != null && response.Errors.Length > 0)
                {
                    var errorMessage = string.Join(", ", response.Errors.Select(e => e.Message));
                    _logger.LogError("GraphQL errors: {Errors}", errorMessage);
                    throw new Exception($"GraphQL Error: {errorMessage}");
                }

                return response.Data?.Product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                throw;
            }
        }

        public async Task<Product?> UpdateProductAsync(int id, ProductInput productInput)
        {
            try
            {
                var mutation = @"
                    mutation UpdateProduct($id: Int!, $product: ProductInput!) {
                        updateProduct(id: $id, product: $product) {
                            id
                            name
                            description
                            price
                            createdAt
                        }
                    }";

                var request = new GraphQLRequest
                {
                    Query = mutation,
                    Variables = new Dictionary<string, object>
                    {
                        ["id"] = id,
                        ["product"] = productInput
                    }
                };

                var response = await _client.SendMutationAsync<UpdateProductResponse>(request);

                if (response.Errors != null && response.Errors.Length > 0)
                {
                    var errorMessage = string.Join(", ", response.Errors.Select(e => e.Message));
                    _logger.LogError("GraphQL errors: {Errors}", errorMessage);
                    throw new Exception($"GraphQL Error: {errorMessage}");
                }

                return response.Data?.Product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var mutation = @"
                    mutation DeleteProduct($id: Int!) {
                        deleteProduct(id: $id)
                    }";

                var request = new GraphQLRequest
                {
                    Query = mutation,
                    Variables = new Dictionary<string, object>
                    {
                        ["id"] = id
                    }
                };

                var response = await _client.SendMutationAsync<DeleteProductResponse>(request);

                if (response.Errors != null && response.Errors.Length > 0)
                {
                    var errorMessage = string.Join(", ", response.Errors.Select(e => e.Message));
                    _logger.LogError("GraphQL errors: {Errors}", errorMessage);
                    throw new Exception($"GraphQL Error: {errorMessage}");
                }

                return response.Data?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID {Id}", id);
                throw;
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}