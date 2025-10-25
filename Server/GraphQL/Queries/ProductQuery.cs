using GraphQL;
using GraphQL.Types;
using Server.GraphQL.Types;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Server.GraphQL.Queries
{
    public class ProductQuery : ObjectGraphType
    {
        public ProductQuery()
        {
            Field<ListGraphType<ProductType>>("products")
                .Argument<StringGraphType>("search", "Search by name")
                .ResolveAsync(async context =>
                {
                    var dbContext = context.RequestServices!.GetRequiredService<ProductContext>();
                    var search = context.GetArgument<string>("search");
                    var query = dbContext.Products.AsQueryable();

                    if (!string.IsNullOrEmpty(search))
                    {
                        query = query.Where(p => p.Name.Contains(search));
                    }

                    return await query.ToListAsync();
                });

            Field<ProductType>("product")
                .Argument<NonNullGraphType<IntGraphType>>("id", "Product ID")
                .ResolveAsync(async context =>
                {
                    var dbContext = context.RequestServices!.GetRequiredService<ProductContext>();
                    var id = context.GetArgument<int>("id");
                    return await dbContext.Products.FindAsync(id);
                });
        }
    }
}