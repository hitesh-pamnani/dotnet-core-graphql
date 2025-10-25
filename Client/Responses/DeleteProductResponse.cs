using System.Text.Json.Serialization;

namespace Client.Responses
{
    public class DeleteProductResponse
    {
        [JsonPropertyName("deleteProduct")]
        public bool Success { get; set; }
    }
}