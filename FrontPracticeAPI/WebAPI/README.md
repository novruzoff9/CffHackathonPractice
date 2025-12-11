# E-Commerce API with Authentication & Authorization

A RESTful API built with .NET 10, ASP.NET Core Identity, JWT Authentication, and Entity Framework Core with PostgreSQL.

## ?? Quick Start

### Prerequisites
- .NET 10.0 SDK
- PostgreSQL database

### Setup
```bash
cd WebAPI
dotnet restore
dotnet ef migrations add AddIdentityAndOrders
dotnet ef database update
dotnet run
```

API will be available at: `http://localhost:5000`

---

## ?? Pre-Seeded Test Accounts

The application automatically creates these users on startup - **ready to use immediately!**

### ?? Admin Account
```
Email:    admin@test.com
Password: Admin123
Role:     Admin
```

### ?? User Account
```
Email:    user@test.com
Password: User123
Role:     User
```

---

## ?? Features

### Authentication & Authorization
- ? JWT Bearer token authentication
- ? Role-based authorization (Admin, User)
- ? ASP.NET Core Identity integration
- ? Automatic user seeding for testing

### Product Catalog Management
- ? Categories CRUD operations
- ? Products CRUD operations
- ? Product-Category relationships
- ? Public read access for browsing

### Order Management
- ? Users can create orders
- ? Users can view their order history
- ? Admins can view all orders
- ? Order tracking with timestamps

### Security
- ? Password validation and hashing
- ? Protected endpoints with [Authorize]
- ? Role-specific access control
- ? JWT token expiration (24 hours)

---

## ?? API Endpoints

### Public (No Authentication)
```
GET  /api/Categories           - List all categories
GET  /api/Categories/{id}      - Get category by ID
GET  /api/Products             - List all products
GET  /api/Products/{id}        - Get product by ID
GET  /api/Products/category/{id} - Products by category
POST /api/Auth/register        - Register new user
POST /api/Auth/login           - Login and get JWT token
```

### Authenticated Users
```
POST /api/Orders              - Create new order
GET  /api/Orders/my-orders    - View own orders
```

### Admin Only
```
POST   /api/Categories          - Create category
DELETE /api/Categories/{id}     - Delete category
POST   /api/Products            - Create product
DELETE /api/Products/{id}       - Delete product
GET    /api/Orders/all          - View all orders
```

---

## ?? Quick Test Examples

### 1. Login as Admin
```bash
curl -X POST http://localhost:5000/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@test.com","password":"Admin123"}'
```

### 2. Create Category (Admin)
```bash
curl -X POST http://localhost:5000/api/Categories \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d '{"name":"Electronics"}'
```

### 3. Create Product (Admin)
```bash
curl -X POST http://localhost:5000/api/Products \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d '{"name":"iPhone 15","description":"Latest model","price":1299.99,"categoryId":"CATEGORY_ID"}'
```

### 4. Login as User
```bash
curl -X POST http://localhost:5000/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"user@test.com","password":"User123"}'
```

### 5. Create Order (User)
```bash
curl -X POST http://localhost:5000/api/Orders \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d '{"productId":"PRODUCT_ID","quantity":2}'
```

---

## ?? Documentation

- **[QUICK_START.md](QUICK_START.md)** - Get started in 3 steps
- **[AUTHENTICATION_GUIDE.md](AUTHENTICATION_GUIDE.md)** - Complete authentication guide
- **[API_ENDPOINTS.md](API_ENDPOINTS.md)** - All endpoints reference
- **[SETUP_AND_TESTING_GUIDE.md](SETUP_AND_TESTING_GUIDE.md)** - Detailed testing guide
- **[PROJECT_STRUCTURE.md](PROJECT_STRUCTURE.md)** - Architecture and code structure
- **[IMPLEMENTATION_SUMMARY.md](IMPLEMENTATION_SUMMARY.md)** - What was implemented
- **[MIGRATION_INSTRUCTIONS.md](MIGRATION_INSTRUCTIONS.md)** - Database migration guide

---

## ??? Architecture

### Technology Stack
- **Framework:** .NET 10
- **Authentication:** ASP.NET Core Identity + JWT
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core 10.0
- **API Documentation:** Scalar (OpenAPI)

### Design Patterns
- Repository Pattern (via EF Core)
- Service Layer Pattern
- DTO Pattern
- Dependency Injection
- Middleware Pattern

### Project Structure
```
WebAPI/
??? Controllers/      # API endpoints
??? Services/         # Business logic
??? Models/          # Entity models
??? Dtos/            # Data transfer objects
??? Contexts/        # EF Core DbContext
??? Exceptions/      # Custom exceptions
??? Middlewares/     # Custom middleware
??? ResponseModels/  # Standardized responses
```

---

## ?? User Permissions

### ?? Admin
- ? Full access to all endpoints
- ? Manage products and categories (CRUD)
- ? View all orders from all users
- ? Create orders
- ? View own orders

### ?? Regular User
- ? Browse products and categories
- ? Create orders
- ? View own orders
- ? Cannot manage products/categories
- ? Cannot view other users' orders

### ?? Anonymous
- ? Browse products and categories
- ? Register new account
- ? Login
- ? Cannot create orders
- ? No access to protected endpoints

---

## ??? Database Schema

### Identity Tables (Auto-generated)
- AspNetUsers
- AspNetRoles
- AspNetUserRoles
- AspNetUserClaims
- AspNetUserLogins
- AspNetUserTokens
- AspNetRoleClaims

### Application Tables
- **Categories** (Id, Name)
- **Products** (Id, Name, Description, Price, CategoryId)
- **Orders** (Id, UserId, ProductId, Quantity, OrderDate)

### Relationships
- Category ? Products (One-to-Many)
- Product ? Orders (One-to-Many)
- User ? Orders (One-to-Many)

---

## ?? Security Features

1. **JWT Authentication**
   - Tokens expire after 24 hours
   - Includes user ID, email, username, and roles
   - Secure token validation

2. **Password Security**
   - Minimum 6 characters
   - Requires at least one digit
   - Requires at least one lowercase letter
   - Passwords are hashed using Identity framework

3. **Role-Based Authorization**
   - Endpoint-level authorization
   - Role-specific access control
   - Automatic role creation on startup

4. **Exception Handling**
   - Global exception middleware
   - Standardized error responses
   - Custom exception types

---

## ?? NuGet Packages

```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="10.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="10.0.0" />
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0.0" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="10.0.0" />
<PackageReference Include="Scalar.AspNetCore" Version="2.11.0" />
<PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="8.3.1" />
```

---

## ??? Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "default": "Server=host;Port=5432;Database=db;User Id=user;Password=pass;"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyForJWTTokenGeneration123456789",
    "Issuer": "WebAPI",
    "Audience": "WebAPIUsers"
  }
}
```

---

## ?? Deployment

### Production Checklist
- [ ] Change JWT secret key to a strong random value
- [ ] Use environment variables for sensitive data
- [ ] Enable HTTPS only
- [ ] Configure CORS properly (not AllowAnyOrigin)
- [ ] Add rate limiting
- [ ] Implement refresh tokens
- [ ] Add email confirmation
- [ ] Set up logging and monitoring
- [ ] Configure health checks
- [ ] Remove or secure seeded test accounts

---

## ?? Testing

### Run Tests
```bash
# Build the project
dotnet build

# Run tests (if you add them)
dotnet test
```

### Manual Testing
Use the pre-seeded accounts:
- Admin: `admin@test.com` / `Admin123`
- User: `user@test.com` / `User123`

Or use the provided curl commands in `QUICK_START.md`

---

## ?? Contributing

This project was created for learning and demonstration purposes.

---

## ?? License

This project is open source and available under the MIT License.

---

## ?? Support

For questions or issues, please refer to the documentation files or create an issue in the repository.

---

## ? Features Implemented

- [x] Complete authentication system with JWT
- [x] Role-based authorization (Admin, User)
- [x] Product catalog management
- [x] Order system
- [x] Pre-seeded test accounts
- [x] Global exception handling
- [x] Standardized API responses
- [x] PostgreSQL database integration
- [x] Entity Framework Core migrations
- [x] OpenAPI/Swagger documentation

---

**Happy Coding! ??**
