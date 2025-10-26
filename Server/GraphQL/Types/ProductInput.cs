namespace Server.GraphQL.Types
{
    public class ProductInput
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}