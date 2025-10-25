using System.Text.Json.Serialization;
using Client.Models;

namespace Client.Responses
{
    public class ProductsResponse
    {
        [JsonPropertyName("products")]
        public Product[] Products { get; set; } = Array.Empty<Product>();
    }
}