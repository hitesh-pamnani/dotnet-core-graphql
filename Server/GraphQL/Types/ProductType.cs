using GraphQL.Types;
using Server.Models;

namespace Server.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Price);
            Field(x => x.Description, nullable: true);
            Field(x => x.CreatedAt);
        }
    }
}