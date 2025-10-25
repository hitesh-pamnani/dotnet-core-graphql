using GraphQL.Types;
using Server.GraphQL.Mutations;
using Server.GraphQL.Queries;

namespace Server.GraphQL.Schemas
{
    public class ProductSchema : Schema
    {
        public ProductSchema()
        {
            Query = new ProductQuery();
            Mutation = new ProductMutation();
        }
    }
}