# API Endpoints Quick Reference

## ?? Public Endpoints (No Authentication Required)

### Authentication
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Auth/register` | Register new user |
| POST | `/api/Auth/login` | Login and get JWT token |

### Categories
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Categories` | Get all categories |
| GET | `/api/Categories/{id}` | Get category by ID |

### Products
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Products` | Get all products |
| GET | `/api/Products/{id}` | Get product by ID |
| GET | `/api/Products/category/{id}` | Get products by category |

---

## ?? Authenticated User Endpoints (User or Admin)

### Orders
| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| POST | `/api/Orders` | Create new order | Bearer Token |
| GET | `/api/Orders/my-orders` | Get user's orders | Bearer Token |

---

## ?? Admin Only Endpoints

### Categories
| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| POST | `/api/Categories` | Create category | Bearer Token (Admin) |
| DELETE | `/api/Categories/{id}` | Delete category | Bearer Token (Admin) |

### Products
| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| POST | `/api/Products` | Create product | Bearer Token (Admin) |
| DELETE | `/api/Products/{id}` | Delete product | Bearer Token (Admin) |

### Orders
| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| GET | `/api/Orders/all` | Get all orders | Bearer Token (Admin) |

---

## ?? Request Examples

### Register User
```json
POST /api/Auth/register
{
  "email": "user@example.com",
  "password": "Password123",
  "userName": "johndoe"
}
```

### Login
```json
POST /api/Auth/login
{
  "email": "user@example.com",
  "password": "Password123"
}
```

### Create Order
```json
POST /api/Orders
Authorization: Bearer <your-jwt-token>
{
  "productId": "product-guid-id",
  "quantity": 2
}
```

### Create Category (Admin)
```json
POST /api/Categories
Authorization: Bearer <admin-jwt-token>
{
  "name": "Electronics"
}
```

### Create Product (Admin)
```json
POST /api/Products
Authorization: Bearer <admin-jwt-token>
{
  "name": "iPhone 15",
  "description": "Latest iPhone model",
  "price": 1299.99,
  "categoryId": "category-guid-id"
}
```

---

## ?? Authorization Header Format

For all authenticated endpoints, include the JWT token in the Authorization header:

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

---

## ?? Response Format

All responses follow this structure:

```json
{
  "data": <response-data>,
  "isSuccess": true/false,
  "statusCode": 200,
  "errors": []
}
```

---

## ?? HTTP Status Codes

| Code | Description |
|------|-------------|
| 200 | OK - Success |
| 201 | Created - Resource created successfully |
| 400 | Bad Request - Invalid input |
| 401 | Unauthorized - Missing or invalid token |
| 403 | Forbidden - Insufficient permissions |
| 404 | Not Found - Resource not found |
| 409 | Conflict - Resource already exists |
| 500 | Internal Server Error |

---

## ?? User Roles

| Role | Permissions |
|------|-------------|
| **User** | Browse products/categories, create orders, view own orders |
| **Admin** | All User permissions + Create/Delete products/categories, view all orders |

---

## ?? Typical User Flows

### Regular User Flow
1. Register ? `POST /api/Auth/register`
2. Login ? `POST /api/Auth/login` (get token)
3. Browse Products ? `GET /api/Products`
4. Create Order ? `POST /api/Orders` (with token)
5. View Orders ? `GET /api/Orders/my-orders` (with token)

### Admin Flow
1. Login as Admin ? `POST /api/Auth/login` (get admin token)
2. Create Category ? `POST /api/Categories` (with admin token)
3. Create Product ? `POST /api/Products` (with admin token)
4. View All Orders ? `GET /api/Orders/all` (with admin token)
5. Manage Inventory ? `DELETE /api/Products/{id}` (with admin token)
