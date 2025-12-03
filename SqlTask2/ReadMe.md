# SQL E-commerce Order Management System

1. Aşağıdakı cədvəllər yaradılacaq:
    - Customers
        - Id (Primary Key)
        - FirstName - maksimum 30 simvol, 
        - LastName - maksimum 30 simvol, 
        - Email - təkrarlanmamalıdır,
        - Phone - maksimum 15 simvol,
        - RegistrationDate - gələcək zaman ola bilməz
    - Products
        - Id (Primary Key)
        - Name - maksimum 150 simvol, təkrarlanmamalıdır,
        - Price - müsbət olmalıdır,
        - Stock - 0 və ya müsbət olmalıdır,
        - CategoryId (Foreign Key)
    - Categories
        - Id (Primary Key)
        - Name - maksimum 50 simvol, təkrarlanmamalıdır,
        - Description - maksimum 200 simvol
    - Suppliers
        - Id (Primary Key)
        - CompanyName - maksimum 100 simvol,
        - ContactPerson - maksimum 50 simvol,
        - Email - təkrarlanmamalıdır,
        - Phone - maksimum 15 simvol
    - ProductSuppliers
        - ProductId (Foreign Key)
        - SupplierId (Foreign Key)
        - SupplyPrice - müsbət olmalıdır
    - Orders
        - Id (Primary Key)
        - CustomerId (Foreign Key)
        - OrderDate - gələcək zaman ola bilməz,
        - DeliveryDate - OrderDate-dən kiçik ola bilməz,
        - TotalAmount - müsbət olmalıdır,
        - Status - ('Pending', 'Processing', 'Shipped', 'Delivered', 'Cancelled')
    - OrderItems
        - Id (Primary Key)
        - OrderId (Foreign Key)
        - ProductId (Foreign Key)
        - Quantity - müsbət olmalıdır,
        - UnitPrice - müsbət olmalıdır

2. Qurulmalı olan relationlar:
    - Hər bir məhsulun bir neçə təchizatçısı ola bilər (Many to Many)
    - Hər bir təchizatçı bir neçə məhsula təchizat edə bilər (Many to Many)
    - Hər bir müştəri bir neçə sifariş verə bilər (One to Many)
    - Hər bir kateqoriya bir neçə məhsula aid ola bilər (One to Many)
    - Hər bir məhsul yalnız bir kateqoriyaya aid ola bilər (Many to One)
    - Hər bir sifariş bir neçə məhsul ehtiva edə bilər (One to Many)
    - Hər bir sifariş elementi yalnız bir sifarişə aid ola bilər (Many to One)

3. Hər cədvəl üçün bir neçə insert sorğusu

4. Join sorğuları:
    - Hər bir müştərinin verdiyi sifarişlərin siyahısı
    - Hər bir məhsulun təchizatçılarının siyahısı
    - Hər bir kateqoriya üzrə olan məhsulların siyahısı
    - Hər bir təchizatçının təchiz etdiyi məhsulların siyahısı
    - Hər bir sifarişdə olan məhsulların ətraflı siyahısı

5. SQL query-lər:
    - Hər bir müştərinin neçə sifariş verdiyini göstərən sorğu
    - Hər bir təchizatçının neçə məhsula təchizat etdiyini göstərən sorğu
    - Hər bir kateqoriya üzrə neçə məhsul olduğunu göstərən sorğu
    - Ən çox sifariş verən müştərini göstərən sorğu
    - Ən çox satılan məhsulu göstərən sorğu
    - Ən yüksək ciro edən müştərini göstərən sorğu
    - Hər bir sifarişin ümumi dəyərini hesablayan sorğu
    - Stokda olmayan məhsulları göstərən sorğu
