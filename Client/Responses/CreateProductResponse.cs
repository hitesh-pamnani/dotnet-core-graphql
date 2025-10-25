using System.Text.Json.Serialization;
using Client.Models;

namespace Client.Responses
{
    public class CreateProductResponse
    {
        [JsonPropertyName("createProduct")]
        public Product? Product { get; set; }
    }
}