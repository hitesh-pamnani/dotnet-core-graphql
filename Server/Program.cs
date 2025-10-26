using Server.Data;
using Server.GraphQL.Queries;
using Server.GraphQL.Mutations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Hot Chocolate GraphQL services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<ProductQuery>()
    .AddMutationType<ProductMutation>()
    .RegisterDbContext<ProductContext>();

var app = builder.Build();

// Configure the HTTP request pipeline


app.UseAuthorization();
app.MapControllers();

// Configure Hot Chocolate GraphQL endpoint
app.MapGraphQL();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();