using System.Text.Json.Serialization;
using Client.Models;

namespace Client.Responses
{
    public class UpdateProductResponse
    {
        [JsonPropertyName("updateProduct")]
        public Product? Product { get; set; }
    }
}