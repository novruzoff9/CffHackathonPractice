# WebAPI Documentation

Bu API, kategori və məhsul idarəetməsi üçün RESTful endpointlər təqdim edir. API .NET Core ilə yaradılmışdır və PostgreSQL verilənlər bazası istifadə edir.

## Base URL
```
http://13.61.183.66:5000/api
```

## 🔐 Authentication (İstifadəçi Girişi)

API-də authentication sistemi mövcuddur. Bəzi əməliyyatlar üçün giriş tələb olunur.

### Test İstifadəçiləri

Sınaq məqsədləri üçün hazır istifadəçilər:

#### 👤 Admin İstifadəçisi
- **Email:** `admin@test.com`
- **Şifrə:** `Admin123`
- **İstifadəçi adı:** `admin`
- **Rol:** Admin
- **İcazələr:** Bütün əməliyyatlara tam giriş

#### 👤 Adi İstifadəçi
- **Email:** `user@test.com`
- **Şifrə:** `User123`
- **İstifadəçi adı:** `user`
- **Rol:** User
- **İcazələr:** Məhsullara baxmaq və sifariş yaratmaq

### Giriş (Login)

**POST** `/api/Auth/login`

**Request Body:**
```json
{
  "email": "admin@test.com",
  "password": "Admin123"
}
```

**Response:**
```json
{
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "email": "admin@test.com",
    "userName": "admin",
    "userId": "user-guid-id"
  },
  "isSuccess": true,
  "statusCode": 200,
  "errors": []
}
```

### Qeydiyyat (Register)

**POST** `/api/Auth/register`

**Request Body:**
```json
{
  "email": "yeni@istifadeci.com",
  "password": "Password123",
  "userName": "istifadeci_adi"
}
```

### Token-dən İstifadə

Giriş etdikdən sonra aldığınız token-i bütün qorunan endpointlər üçün istifadə edin:

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### İcazə Tələb Edən Əməliyyatlar

**Adi İstifadəçilər (User):**
- Sifariş yaratmaq (POST /api/Orders)
- Öz sifarişlərini görmək (GET /api/Orders/my-orders)

**Yalnız Admin:**
- Kategoriya yaratmaq/silmək (POST/DELETE /api/Categories)
- Məhsul yaratmaq/silmək (POST/DELETE /api/Products)
- Bütün sifarişləri görmək (GET /api/Orders/all)

**Hamı Üçün Açıq (Authentication tələb olunmur):**
- Kategoriyalara baxmaq (GET /api/Categories)
- Məhsullara baxmaq (GET /api/Products)
- Giriş və Qeydiyyat (POST /api/Auth/login, POST /api/Auth/register)

---

## Response Formatı

Bütün API cavabları aşağıdakı formatda qaytarılır:

```json
{
  "data": <T>, // API-nin qaytardığı məlumat
  "isSuccess": true/false, // Əməliyyatın uğurlu olub-olmaması
  "statusCode": 200, // HTTP status kod
  "errors": [] // Xətalar (əgər varsa)
}
```

### Uğurlu Cavab Nümunəsi:
```json
{
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174000",
    "name": "Elektronika"
  },
  "isSuccess": true,
  "statusCode": 200,
  "errors": []
}
```

### Xətalı Cavab Nümunəsi:
```json
{
  "data": null,
  "isSuccess": false,
  "statusCode": 404,
  "errors": ["Resource not found."]
}
```

## Error Kodları

| Status Kod | Təsvir |
|-----------|--------|
| 200 | OK - Uğurlu əməliyyat |
| 201 | Created - Yeni resurs yaradıldı |
| 400 | Bad Request - Yanlış sorğu formatı |
| 404 | Not Found - Resurs tapılmadı |
| 409 | Conflict - Konflikt (məs: dublikat məlumat) |
| 500 | Internal Server Error - Daxili server xətası |

---

# Categories API

Kategoriyaları idarə etmək üçün endpointlər.

## 1. Bütün Kategoriyaları Al

**GET** `/api/Categories`

### Request
Heç bir parametr tələb olunmur.

### Response
```json
{
  "data": [
    {
      "id": "123e4567-e89b-12d3-a456-426614174000",
      "name": "Elektronika"
    },
    {
      "id": "987f6543-d21c-43b2-a123-987654321000",
      "name": "Geyim"
    }
  ],
  "isSuccess": true,
  "statusCode": 200,
  "errors": []
}
```

### Mümkün Xətalar
- `500` - Server xətası

---

## 2. ID-yə görə Kategori Al

**GET** `/api/Categories/{id}`

### URL Parametrləri
| Parametr | Tip | Tələb | Təsvir |
|----------|-----|-------|--------|
| id | string | Bəli | Kategoriyasının GUID ID-si |

### Request Nümunəsi
```
GET /api/Categories/123e4567-e89b-12d3-a456-426614174000
```

### Response
```json
{
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174000",
    "name": "Elektronika"
  },
  "isSuccess": true,
  "statusCode": 200,
  "errors": []
}
```

### Mümkün Xətalar
- `404` - Kategori tapılmadı
- `500` - Server xətası

---

## 3. Yeni Kategori Yarat

**POST** `/api/Categories`

### Request Body
```json
{
  "name": "Yeni Kategori"
}
```

### Request Model (CategoryCreateDto)
| Sahə | Tip | Tələb | Təsvir |
|------|-----|-------|--------|
| name | string | Bəli | Kategoriyasının adı |

### Response
```json
{
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174000",
    "name": "Yeni Kategori"
  },
  "isSuccess": true,
  "statusCode": 201,
  "errors": []
}
```

### Mümkün Xətalar
- `400` - Yanlış məlumat formatı
- `409` - Kategori artıq mövcuddur
- `500` - Server xətası

---

## 4. Kategori Sil

**DELETE** `/api/Categories/{id}`

### URL Parametrləri
| Parametr | Tip | Tələb | Təsvir |
|----------|-----|-------|--------|
| id | string | Bəli | Silinəcək kategoriyasının GUID ID-si |

### Request Nümunəsi
```
DELETE /api/Categories/123e4567-e89b-12d3-a456-426614174000
```

### Response
```json
{
  "data": "Category deleted successfully",
  "isSuccess": true,
  "statusCode": 200,
  "errors": []
}
```

### Mümkün Xətalar
- `404` - Kategori tapılmadı
- `409` - Kategori silinə bilməz (məhsulları var)
- `500` - Server xətası

---

# Products API

Məhsulları idarə etmək üçün endpointlər.

## 1. Bütün Məhsulları Al

**GET** `/api/Products`

### Request
Heç bir parametr tələb olunmur.

### Response
```json
{
  "data": [
    {
      "id": "123e4567-e89b-12d3-a456-426614174000",
      "name": "iPhone 15",
      "description": "Ən yeni iPhone modeli",
      "price": 1299.99,
      "categoryName": "Elektronika"
    },
    {
      "id": "987f6543-d21c-43b2-a123-987654321000",
      "name": "Samsung TV",
      "description": "4K Smart TV",
      "price": 899.99,
      "categoryName": "Elektronika"
    }
  ],
  "isSuccess": true,
  "statusCode": 200,
  "errors": []
}
```

### Mümkün Xətalar
- `500` - Server xətası

---

## 2. ID-yə görə Məhsul Al

**GET** `/api/Products/{id}`

### URL Parametrləri
| Parametr | Tip | Tələb | Təsvir |
|----------|-----|-------|--------|
| id | string | Bəli | Məhsulun GUID ID-si |

### Request Nümunəsi
```
GET /api/Products/123e4567-e89b-12d3-a456-426614174000
```

### Response
```json
{
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174000",
    "name": "iPhone 15",
    "description": "Ən yeni iPhone modeli",
    "price": 1299.99,
    "categoryName": "Elektronika"
  },
  "isSuccess": true,
  "statusCode": 200,
  "errors": []
}
```

### Mümkün Xətalar
- `404` - Məhsul tapılmadı
- `500` - Server xətası

---

## 3. Yeni Məhsul Yarat

**POST** `/api/Products`

### Request Body
```json
{
  "name": "Yeni Məhsul",
  "description": "Məhsulun təsviri",
  "price": 199.99,
  "categoryId": "123e4567-e89b-12d3-a456-426614174000"
}
```

### Request Model (ProductCreateDto)
| Sahə | Tip | Tələb | Təsvir |
|------|-----|-------|--------|
| name | string | Bəli | Məhsulun adı |
| description | string | Bəli | Məhsulun təsviri |
| price | decimal | Bəli | Məhsulun qiyməti |
| categoryId | string | Bəli | Kateqoriyanın GUID ID-si |

### Response
```json
{
  "data": {
    "id": "456f7890-a12b-34c5-d678-123456789000",
    "name": "Yeni Məhsul",
    "description": "Məhsulun təsviri",
    "price": 199.99,
    "categoryName": "Elektronika"
  },
  "isSuccess": true,
  "statusCode": 201,
  "errors": []
}
```

### Mümkün Xətalar
- `400` - Yanlış məlumat formatı (məs: mənfi qiymət)
- `404` - Göstərilən kategori tapılmadı
- `409` - Məhsul artıq mövcuddur
- `500` - Server xətası

---

## 4. Kategoriyaya görə Məhsulları Al

**GET** `/api/Products/category/{id}`

### URL Parametrləri
| Parametr | Tip | Tələb | Təsvir |
|----------|-----|-------|--------|
| id | string | Bəli | Kategoriyasının GUID ID-si |

### Request Nümunəsi
```
GET /api/Products/category/123e4567-e89b-12d3-a456-426614174000
```

### Response
```json
{
  "data": [
    {
      "id": "123e4567-e89b-12d3-a456-426614174000",
      "name": "iPhone 15",
      "description": "Ən yeni iPhone modeli",
      "price": 1299.99,
      "categoryName": "Elektronika"
    },
    {
      "id": "987f6543-d21c-43b2-a123-987654321000",
      "name": "Samsung TV",
      "description": "4K Smart TV",
      "price": 899.99,
      "categoryName": "Elektronika"
    }
  ],
  "isSuccess": true,
  "statusCode": 200,
  "errors": []
}
```

### Mümkün Xətalar
- `404` - Kategori tapılmadı
- `500` - Server xətası

---

## 5. Məhsul Sil

**DELETE** `/api/Products/{id}`

### URL Parametrləri
| Parametr | Tip | Tələb | Təsvir |
|----------|-----|-------|--------|
| id | string | Bəli | Silinəcək məhsulun GUID ID-si |

### Request Nümunəsi
```
DELETE /api/Products/123e4567-e89b-12d3-a456-426614174000
```

### Response
```json
{
  "data": "Product deleted successfully",
  "isSuccess": true,
  "statusCode": 200,
  "errors": []
}
```

### Mümkün Xətalar
- `404` - Məhsul tapılmadı
- `500` - Server xətası

---

## Data Modelləri

### CategoryReturnDto
```json
{
  "id": "string", // GUID formatında
  "name": "string" // Kategori adı
}
```

### CategoryCreateDto
```json
{
  "name": "string" // Kategori adı (tələb olunur)
}
```

### ProductReturnDto
```json
{
  "id": "string", // GUID formatında
  "name": "string", // Məhsul adı
  "description": "string", // Məhsul təsviri
  "price": 0.00, // Qiymət (decimal)
  "categoryName": "string" // Kategoriya adı
}
```

### ProductCreateDto
```json
{
  "name": "string", // Məhsul adı (tələb olunur)
  "description": "string", // Məhsul təsviri (tələb olunur)
  "price": 0.00, // Qiymət (tələb olunur, müsbət olmalıdır)
  "categoryId": "string" // Kategoriya ID-si (tələb olunur, GUID formatında)
}
```

---

## Qeydlər

1. Bütün ID-lər GUID formatındadır
2. Qiymətlər decimal tipindədir və müsbət olmalıdır
3. Bütün string sahələri boş ola bilməz
4. API JSON formatında məlumat qəbul edir və qaytarır
5. Xəta hallarında `isSuccess` false olur və `errors` massivində xəta mesajları gəlir
6. HTTP status kodları response obyektinin `statusCode` sahəsində də mövcuddur