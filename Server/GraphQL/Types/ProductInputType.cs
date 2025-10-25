using GraphQL.Types;

namespace Server.GraphQL.Types
{
    public class ProductInputType : InputObjectGraphType
    {
        public ProductInputType()
        {
            Name = "ProductInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<DecimalGraphType>("price");
            Field<StringGraphType>("description");
        }
    }
}