# .NET Core GraphQL API

A full-stack GraphQL application built with .NET 8, Hot Chocolate GraphQL, Entity Framework Core, and PostgreSQL. This project demonstrates a modern GraphQL implementation with a Hot Chocolate server and a StrawberryShake client, showcasing type-safe GraphQL development with automatic code generation, compile-time validation, and seamless integration between server and client components.

## 🏗️ Architecture

The solution consists of two main projects:

- **Server**: Hot Chocolate GraphQL API server with Entity Framework Core and PostgreSQL backend
- **Client**: ASP.NET Core Web API that acts as a GraphQL client using StrawberryShake for type-safe operations

### Why StrawberryShake?

StrawberryShake is Microsoft's official .NET GraphQL client that provides:
- **Compile-time safety** with generated strongly-typed C# code
- **Automatic synchronization** between GraphQL schema and client code
- **Optimal performance** with built-in caching and HTTP optimizations
- **Developer productivity** through IntelliSense and compile-time validation
- **Seamless integration** with .NET dependency injection and logging

## 🚀 Features

### Server Features
- **Hot Chocolate GraphQL API** with queries and mutations
- **Entity Framework Core** with PostgreSQL database
- **Product Management** (CRUD operations)
- **GraphQL Playground** for interactive API exploration
- **Docker Support** for containerized deployment

### Client Features
- **StrawberryShake GraphQL Client** with type-safe code generation
- **Compile-time GraphQL validation** and IntelliSense support
- **Automatic code generation** from GraphQL schema and operations
- **Built-in entity store** for caching and state management
- **RESTful API wrapper** that demonstrates GraphQL consumption
- **Search Functionality** for products by name
- **Strongly-typed models** and operation results

### StrawberryShake Advantages

✅ **Type Safety**: Compile-time validation prevents runtime GraphQL errors  
✅ **IntelliSense**: Full IDE support with auto-completion  
✅ **Performance**: Optimized HTTP transport and caching  
✅ **Maintainability**: Automatic code updates when schema changes  
✅ **Developer Experience**: No manual model mapping required  
✅ **Error Handling**: Structured error responses with detailed information

## 📋 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 12+](https://www.postgresql.org/download/)
- [Docker](https://www.docker.com/get-started) (optional)

## 🗄️ Database Setup

### PostgreSQL Installation and Configuration

1. **Install PostgreSQL** (if not already installed):

   ```bash
   # macOS (using Homebrew)
   brew install postgresql

   # Ubuntu/Debian
   sudo apt-get install postgresql postgresql-contrib

   # Windows - Download from https://www.postgresql.org/download/windows/
   ```

2. **Start PostgreSQL service**:

   ```bash
   # macOS
   brew services start postgresql

   # Ubuntu/Debian
   sudo systemctl start postgresql
   sudo systemctl enable postgresql
   ```

3. **Create database and user**:

   ```sql
   -- Connect to PostgreSQL as superuser
   sudo -u postgres psql

   -- Create database
   CREATE DATABASE "products-db";

   -- Create user
   CREATE USER "products-user" WITH PASSWORD 'Products@123';

   -- Grant database ownership to user
   ALTER DATABASE "products-db" OWNER TO "products-user";

   -- Grant all privileges on database
   GRANT ALL PRIVILEGES ON DATABASE "products-db" TO "products-user";

   -- Connect to the products database
   \c products-db

   -- Grant privileges on public schema
   GRANT ALL ON SCHEMA public TO "products-user";
   GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO "products-user";
   GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO "products-user";

   -- Grant default privileges for future objects
   ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO "products-user";
   ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON SEQUENCES TO "products-user";

   -- Exit PostgreSQL
   \q
   ```

4. **Verify connection**:
   ```bash
   psql -h localhost -U products-user -d products-db
   ```

## ⚙️ Configuration

### Server Configuration

Update `Server/appsettings.json` if needed:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=products-db;Username=products-user;Password=Products@123"
  }
}
```

### Client Configuration

Update `Client/appsettings.json` if needed:

```json
{
  "GraphQL": {
    "Endpoint": "http://localhost:4000/graphql"
  }
}
```

## 🚀 Getting Started

### Option 1: Run Locally

1. **Clone the repository**:

   ```bash
   git clone <repository-url>
   cd dotnet-core-graphql
   ```

2. **Restore dependencies**:

   ```bash
   # Server
   cd Server
   dotnet restore

   # Client
   cd ../Client
   dotnet restore
   ```

3. **Run the GraphQL Server**:

   ```bash
   cd Server
   dotnet run
   ```

   Server will be available at: `http://localhost:4000`

4. **Run the Client** (in a new terminal):
   ```bash
   cd Client
   dotnet run
   ```
   Client will be available at: `http://localhost:8080`

### Option 2: Using Docker

1. **Build and run with Docker Compose**:
   ```bash
   cd Server
   docker-compose up --build
   ```

## 📊 Database Schema

The application uses a simple Product entity:

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

## 🔍 GraphQL API

### Endpoint

- **GraphQL Playground**: `http://localhost:4000/graphql`

### Queries

**Get all products**:

```graphql
query {
  products {
    id
    name
    price
    description
    createdAt
  }
}
```

**Search products by name**:

```graphql
query {
  products(search: "laptop") {
    id
    name
    price
    description
  }
}
```

**Get product by ID**:

```graphql
query {
  product(id: 1) {
    id
    name
    price
    description
    createdAt
  }
}
```

### Mutations

**Create product**:

```graphql
mutation {
  createProduct(
    product: {
      name: "Laptop"
      price: 999.99
      description: "High-performance laptop"
    }
  ) {
    id
    name
    price
    description
    createdAt
  }
}
```

**Update product**:

```graphql
mutation {
  updateProduct(
    id: 1
    product: {
      name: "Updated Laptop"
      price: 1199.99
      description: "Updated description"
    }
  ) {
    id
    name
    price
    description
  }
}
```

**Delete product**:

```graphql
mutation {
  deleteProduct(id: 1)
}
```

## 🛠️ Development

### Project Structure

```
├── Server/                     # GraphQL API Server
│   ├── Data/                  # Entity Framework DbContext
│   ├── GraphQL/               # GraphQL implementation
│   │   ├── Mutations/         # GraphQL mutations
│   │   ├── Queries/           # GraphQL queries
│   │   ├── Schemas/           # GraphQL schemas
│   │   └── Types/             # GraphQL types
│   ├── Models/                # Data models
│   └── Program.cs             # Application entry point
├── Client/                    # REST API Client
│   ├── .config/               # dotnet tools configuration
│   ├── Controllers/           # API controllers
│   ├── Generated/             # StrawberryShake generated code
│   ├── GraphQL/               # GraphQL operations and schema
│   │   ├── Operations/        # GraphQL queries and mutations
│   │   ├── schema.graphql     # GraphQL schema
│   │   └── schema.extensions.graphql # Schema extensions
│   ├── Models/                # Client models
│   ├── Services/              # GraphQL client services
│   └── .graphqlrc.json        # StrawberryShake configuration
└── README.md
```

### Key Dependencies

**Server**:

- `HotChocolate.AspNetCore` (13.9.0) - Hot Chocolate GraphQL server
- `HotChocolate.Data.EntityFramework` (13.9.0) - Entity Framework integration
- `Npgsql.EntityFrameworkCore.PostgreSQL` (9.0.4) - PostgreSQL provider
- `Microsoft.EntityFrameworkCore.Design` (9.0.10) - EF Core tools

**Client**:

- `StrawberryShake.Transport.Http` (15.1.11) - StrawberryShake GraphQL client
- `Microsoft.Extensions.Http` (9.0.10) - HTTP client factory
- `Microsoft.Extensions.DependencyInjection` (9.0.10) - Dependency injection

### Adding Migrations

```bash
cd Server
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### StrawberryShake Code Generation

The client uses StrawberryShake for type-safe GraphQL client generation:

```bash
cd Client
# Install StrawberryShake tools
dotnet tool restore

# Generate client code from GraphQL schema
dotnet graphql generate
```

**GraphQL Operations** (located in `Client/GraphQL/Operations/`):
- `GetProducts.graphql` - Query all products with optional search
- `GetProduct.graphql` - Query single product by ID
- `CreateProduct.graphql` - Create new product
- `UpdateProduct.graphql` - Update existing product
- `DeleteProduct.graphql` - Delete product by ID

**Configuration** (`.graphqlrc.json`):
```json
{
  "schema": "GraphQL/schema.graphql",
  "documents": "GraphQL/**/*.graphql",
  "extensions": {
    "strawberryShake": {
      "name": "ProductsClient",
      "namespace": "Client",
      "url": "http://localhost:4000/graphql"
    }
  }
}
```

## 🍓 StrawberryShake GraphQL Client

### Overview

The client project uses **StrawberryShake**, a .NET GraphQL client that generates strongly-typed C# code from GraphQL operations. This provides compile-time safety, IntelliSense support, and eliminates runtime errors from typos in GraphQL queries.

### Key Features

- **Type-Safe Code Generation**: Automatically generates C# classes from GraphQL schema
- **Compile-Time Validation**: Catches GraphQL errors at build time
- **IntelliSense Support**: Full IDE support for GraphQL operations
- **Entity Store**: Built-in caching and state management
- **HTTP Transport**: Optimized HTTP client for GraphQL requests

### Generated Code Structure

```
Client/Generated/
└── ProductsClient.Client.cs    # Generated client code
    ├── IProductsClient         # Main client interface
    ├── ProductsClient          # Client implementation
    ├── Query/Mutation classes  # Operation-specific classes
    ├── Result classes          # Strongly-typed results
    └── Input classes           # Input type definitions
```

### Usage Example

```csharp
// Dependency injection setup (Program.cs)
builder.Services
    .AddProductsClient()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("http://localhost:4000/graphql");
    });

// Service implementation (ProductsService.cs)
public class ProductsService : IProductsService
{
    private readonly IProductsClient _client;

    public ProductsService(IProductsClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(string? search = null)
    {
        var result = await _client.GetProducts.ExecuteAsync(search);
        return result.Data.Products?.Select(MapToProduct) ?? Enumerable.Empty<Product>();
    }
}
```

### GraphQL Operations

**Query Operations**:
- `GetProducts` - Retrieve all products with optional search filter
- `GetProduct` - Retrieve single product by ID

**Mutation Operations**:
- `CreateProduct` - Create new product
- `UpdateProduct` - Update existing product
- `DeleteProduct` - Delete product by ID

### Configuration Files

**`.graphqlrc.json`** - StrawberryShake configuration:
```json
{
  "schema": "GraphQL/schema.graphql",
  "documents": "GraphQL/**/*.graphql",
  "extensions": {
    "strawberryShake": {
      "name": "ProductsClient",
      "namespace": "Client",
      "url": "http://localhost:4000/graphql",
      "records": {
        "inputs": false,
        "entities": false
      }
    }
  }
}
```

**`.config/dotnet-tools.json`** - Required tools:
```json
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "strawberryshake.tools": {
      "version": "15.1.11",
      "commands": ["dotnet-graphql"]
    }
  }
}
```

### Code Generation Commands

```bash
# Install tools
dotnet tool restore

# Download schema from server
dotnet graphql download http://localhost:4000/graphql

# Generate client code
dotnet graphql generate

# Build project (triggers automatic generation)
dotnet build
```

### Development Workflow

1. **Start the GraphQL Server**:
   ```bash
   cd Server
   dotnet run
   ```

2. **Update GraphQL Operations** (if needed):
   - Modify `.graphql` files in `Client/GraphQL/Operations/`
   - Add new queries or mutations as required

3. **Regenerate Client Code**:
   ```bash
   cd Client
   dotnet graphql generate
   ```

4. **Update Service Layer**:
   - Modify `ProductsService.cs` to use new operations
   - Update mapping logic if schema changes

5. **Build and Test**:
   ```bash
   dotnet build
   dotnet run
   ```

### Schema Evolution

When the GraphQL schema changes:

1. **Download Updated Schema**:
   ```bash
   dotnet graphql download http://localhost:4000/graphql
   ```

2. **Review Changes**:
   - Check `schema.graphql` for modifications
   - Update operations if breaking changes exist

3. **Regenerate and Fix**:
   ```bash
   dotnet graphql generate
   dotnet build  # Fix any compilation errors
   ```

## 🧪 Testing

### Using Postman

Import the provided Postman collection:

- `Server/GraphQL-API.postman_collection.json`
- `Client/Client.postman_collection.json`

### Manual Testing

1. Start the server
2. Navigate to `http://localhost:4000/graphql`
3. Use the GraphQL Playground to test queries and mutations
4. Test the client API at `http://localhost:8080/swagger`

## 🐳 Docker Deployment

The server includes Docker support:

```dockerfile
# Build and run
docker build -t graphql-server .
docker run -p 4000:4000 graphql-server
```

## 📝 API Documentation

- **GraphQL Playground**: Available at `/graphql` when running in development
- **Swagger UI**: Available for the client API at `/swagger`
- **Postman Collections**: Included in both Server and Client projects
- **Generated Documentation**: StrawberryShake generates comprehensive XML documentation
- **Schema Introspection**: Full GraphQL schema available via introspection queries

### API Endpoints

**GraphQL Server** (`http://localhost:4000`):
- `/graphql` - GraphQL endpoint
- `/graphql` - GraphQL Playground (development only)

**REST Client** (`http://localhost:8080`):
- `GET /api/products` - Get all products (with optional search)
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product
- `/swagger` - Swagger UI documentation

## 🔧 Troubleshooting

### Common Issues

1. **Database Connection Issues**:

   - Verify PostgreSQL is running
   - Check connection string in `appsettings.json`
   - Ensure database and user exist with proper permissions

2. **Port Conflicts**:

   - Server runs on port 4000
   - Client runs on port 8080
   - Modify `launchSettings.json` if needed

3. **Entity Framework Issues**:
   - Run `dotnet ef database update` to apply migrations
   - Check database permissions for the user

4. **StrawberryShake Issues**:
   - Ensure server is running before generating client code
   - Run `dotnet tool restore` to install required tools
   - Delete `Generated/` folder and regenerate if schema changes
   - Check `.graphqlrc.json` configuration
   - Verify GraphQL operations syntax in `.graphql` files

5. **Code Generation Issues**:
   ```bash
   # Clean and regenerate
   dotnet clean
   rm -rf Generated/
   dotnet graphql generate
   dotnet build
   ```

## 📄 License

This project is licensed under the Apache License 2.0 - see the [LICENSE](LICENSE) file for details.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## 📞 Support

For issues and questions:

- Create an issue in the repository
- Check existing documentation
- Review the GraphQL Playground for API exploration
