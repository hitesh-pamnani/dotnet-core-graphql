# .NET Core GraphQL API

A full-stack GraphQL application built with .NET 8, Entity Framework Core, and PostgreSQL. The project demonstrates a complete GraphQL implementation with both server and client components for managing products.

## 🏗️ Architecture

The solution consists of two main projects:

- **Server**: GraphQL API server with Entity Framework Core and PostgreSQL
- **Client**: Web API client that consumes the GraphQL server

## 🚀 Features

- **GraphQL API** with queries and mutations
- **Entity Framework Core** with PostgreSQL database
- **Product Management** (CRUD operations)
- **Docker Support** for containerized deployment
- **RESTful Client** that demonstrates GraphQL consumption
- **Search Functionality** for products by name

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
   Client will be available at: `http://localhost:5000`

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
  createProduct(product: {
    name: "Laptop"
    price: 999.99
    description: "High-performance laptop"
  }) {
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
  updateProduct(id: 1, product: {
    name: "Updated Laptop"
    price: 1199.99
    description: "Updated description"
  }) {
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
│   ├── Controllers/           # API controllers
│   ├── Models/                # Client models
│   ├── Services/              # GraphQL client services
│   └── Responses/             # Response models
└── README.md
```

### Key Dependencies

**Server**:
- `GraphQL` (8.7.0) - GraphQL implementation
- `GraphQL.Server.Transports.AspNetCore` (8.3.2) - ASP.NET Core integration
- `Npgsql.EntityFrameworkCore.PostgreSQL` (9.0.4) - PostgreSQL provider
- `Microsoft.EntityFrameworkCore.Design` (9.0.10) - EF Core tools

**Client**:
- `GraphQL.Client` (6.1.0) - GraphQL client
- `GraphQL.Client.Serializer.SystemTextJson` (6.1.0) - JSON serialization

### Adding Migrations

```bash
cd Server
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 🧪 Testing

### Using Postman

Import the provided Postman collection:
- `Server/GraphQL-API.postman_collection.json`

### Manual Testing

1. Start the server
2. Navigate to `http://localhost:4000/graphql`
3. Use the GraphQL Playground to test queries and mutations

## 🐳 Docker Deployment

The server includes Docker support:

```dockerfile
# Build and run
docker build -t graphql-server .
docker run -p 4000:4000 graphql-server
```

## 📝 API Documentation

- **GraphQL Playground**: Available at `/graphql` when running in development
- **Swagger UI**: Available for the client API
- **Postman Collections**: Included in both Server and Client projects

## 🔧 Troubleshooting

### Common Issues

1. **Database Connection Issues**:
   - Verify PostgreSQL is running
   - Check connection string in `appsettings.json`
   - Ensure database and user exist with proper permissions

2. **Port Conflicts**:
   - Server runs on port 4000
   - Client runs on port 5000
   - Modify `launchSettings.json` if needed

3. **Entity Framework Issues**:
   - Run `dotnet ef database update` to apply migrations
   - Check database permissions for the user

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