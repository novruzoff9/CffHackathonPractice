# ?? Test Credentials - Quick Reference

## ?? Copy & Paste Ready

### ?? Admin Login
```json
{
  "email": "admin@test.com",
  "password": "Admin123"
}
```

### ?? User Login
```json
{
  "email": "user@test.com",
  "password": "User123"
}
```

---

## ?? Quick Test Flow

### Step 1: Login as Admin
```bash
curl -X POST http://localhost:5000/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@test.com","password":"Admin123"}'
```
**? Copy the `token` from response**

### Step 2: Create Category
```bash
curl -X POST http://localhost:5000/api/Categories \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer PASTE_ADMIN_TOKEN_HERE" \
  -d '{"name":"Electronics"}'
```
**? Copy the `id` from response**

### Step 3: Create Product
```bash
curl -X POST http://localhost:5000/api/Products \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer PASTE_ADMIN_TOKEN_HERE" \
  -d '{"name":"iPhone 15","description":"Latest iPhone","price":1299.99,"categoryId":"PASTE_CATEGORY_ID_HERE"}'
```
**? Copy the `id` from response**

### Step 4: Login as User
```bash
curl -X POST http://localhost:5000/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"user@test.com","password":"User123"}'
```
**? Copy the `token` from response**

### Step 5: Create Order
```bash
curl -X POST http://localhost:5000/api/Orders \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer PASTE_USER_TOKEN_HERE" \
  -d '{"productId":"PASTE_PRODUCT_ID_HERE","quantity":2}'
```

### Step 6: View Orders
```bash
# User views their orders
curl -X GET http://localhost:5000/api/Orders/my-orders \
  -H "Authorization: Bearer PASTE_USER_TOKEN_HERE"

# Admin views all orders
curl -X GET http://localhost:5000/api/Orders/all \
  -H "Authorization: Bearer PASTE_ADMIN_TOKEN_HERE"
```

---

## ?? Postman Collection Variables

```
baseUrl:     http://localhost:5000
adminEmail:  admin@test.com
adminPass:   Admin123
userEmail:   user@test.com
userPass:    User123
```

---

## ?? Accounts Summary

| Role | Email | Password | Access Level |
|------|-------|----------|--------------|
| ?? Admin | `admin@test.com` | `Admin123` | Full access |
| ?? User | `user@test.com` | `User123` | Limited access |

---

## ? What Each Can Do

### Admin Account
```
? GET    /api/Categories
? GET    /api/Categories/{id}
? POST   /api/Categories          ? Admin only
? DELETE /api/Categories/{id}     ? Admin only
? GET    /api/Products
? GET    /api/Products/{id}
? POST   /api/Products             ? Admin only
? DELETE /api/Products/{id}        ? Admin only
? POST   /api/Orders
? GET    /api/Orders/my-orders
? GET    /api/Orders/all           ? Admin only
```

### User Account
```
? GET    /api/Categories
? GET    /api/Categories/{id}
? POST   /api/Categories          ? Admin only
? DELETE /api/Categories/{id}     ? Admin only
? GET    /api/Products
? GET    /api/Products/{id}
? POST   /api/Products             ? Admin only
? DELETE /api/Products/{id}        ? Admin only
? POST   /api/Orders
? GET    /api/Orders/my-orders
? GET    /api/Orders/all           ? Admin only
```

---

## ?? Token Format

After login, use this header format:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

---

## ?? Quick Tips

1. **Tokens expire in 24 hours** - Login again if expired
2. **Use admin account** for creating products/categories
3. **Use user account** to test user permissions
4. **Save tokens** in environment variables or Postman variables
5. **Check response** for IDs needed in subsequent requests

---

## ?? Troubleshooting

### Problem: 401 Unauthorized
**Solution:** Include token in Authorization header

### Problem: 403 Forbidden
**Solution:** Use admin account for admin-only endpoints

### Problem: 404 Not Found
**Solution:** Ensure resources (product/category) exist before referencing

### Problem: Token expired
**Solution:** Login again to get a new token

---

## ?? More Info

See full documentation in:
- `QUICK_START.md`
- `AUTHENTICATION_GUIDE.md`
- `API_ENDPOINTS.md`

---

**Print this and keep it handy! ??**
