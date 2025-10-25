using System.Text.Json.Serialization;
using Client.Models;

namespace Client.Responses
{
    public class ProductResponse
    {
        [JsonPropertyName("product")]
        public Product? Product { get; set; }
    }
}