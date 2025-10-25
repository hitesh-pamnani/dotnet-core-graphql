using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.SystemTextJson;
using Server.Data;
using Server.GraphQL;
using Server.GraphQL.Types;
using Server.GraphQL.Schemas;
using Server.GraphQL.Queries;
using Server.GraphQL.Mutations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add GraphQL services
builder.Services.AddSingleton<ProductType>();
builder.Services.AddSingleton<ProductInputType>();
builder.Services.AddSingleton<ProductQuery>();
builder.Services.AddSingleton<ProductMutation>();
builder.Services.AddSingleton<ProductSchema>();

builder.Services.AddGraphQL(b => b
    .AddSystemTextJson()
    .AddSchema<ProductSchema>()
    .AddGraphTypes(typeof(ProductSchema).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline


app.UseAuthorization();
app.MapControllers();

// Configure GraphQL endpoint
app.UseGraphQL<ProductSchema>();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();