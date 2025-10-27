using Client;
using Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add StrawberryShake client
builder.Services
    .AddProductsClient()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["GraphQL:Endpoint"]!);
    });

// Register ProductsService
builder.Services.AddScoped<IProductsService, ProductsService>();

var app = builder.Build();

var client = app.Services.GetRequiredService<IProductsClient>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
