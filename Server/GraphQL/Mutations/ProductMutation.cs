using GraphQL;
using GraphQL.Types;
using Server.GraphQL.Types;
using Server.Models;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Server.GraphQL.Mutations
{
    public class ProductMutation : ObjectGraphType
    {
        public ProductMutation()
        {
            Field<ProductType>("createProduct")
                .Argument<NonNullGraphType<ProductInputType>>("product")
                .ResolveAsync(async context =>
                {
                    var dbContext = context.RequestServices!.GetRequiredService<ProductContext>();
                    var productInput = context.GetArgument<Dictionary<string, object>>("product");
                    var product = new Product
                    {
                        Name = productInput["name"].ToString() ?? "",
                        Description = productInput.TryGetValue("description", out var desc) ? desc.ToString() : "",
                        Price = Convert.ToDecimal(productInput["price"]),
                        CreatedAt = DateTime.UtcNow
                    };

                    dbContext.Products.Add(product);
                    await dbContext.SaveChangesAsync();
                    return product;
                });

            Field<ProductType>("updateProduct")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .Argument<NonNullGraphType<ProductInputType>>("product")
                .ResolveAsync(async context =>
                {
                    var dbContext = context.RequestServices!.GetRequiredService<ProductContext>();
                    var id = context.GetArgument<int>("id");
                    var productInput = context.GetArgument<Dictionary<string, object>>("product");

                    var product = await dbContext.Products.FindAsync(id);
                    if (product == null)
                        throw new ExecutionError("Product not found");

                    product.Name = productInput["name"].ToString() ?? product.Name;
                    product.Description = productInput.TryGetValue("description", out var desc) ? desc.ToString() : product.Description;
                    product.Price = Convert.ToDecimal(productInput["price"]);

                    await dbContext.SaveChangesAsync();
                    return product;
                });
            
            Field<BooleanGraphType>("deleteProduct")
                .Argument<NonNullGraphType<IntGraphType>>("id")
                .ResolveAsync(async context =>
                {
                    var dbContext = context.RequestServices!.GetRequiredService<ProductContext>();
                    var id = context.GetArgument<int>("id");
                    var product = await dbContext.Products.FindAsync(id);
                    
                    if (product == null)
                        return false;

                    dbContext.Products.Remove(product);
                    await dbContext.SaveChangesAsync();
                    return true;
                });
        }
    }
}
