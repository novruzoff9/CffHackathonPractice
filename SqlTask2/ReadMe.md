# SQL Online Shop System

1. Aşağıdakı cədvəllər yaradılacaq:
    - Customers
        - Id (Primary Key)
        - Name - maksimum 50 simvol, 
        - Email - təkrarlanmamalıdır
    - Products
        - Id (Primary Key)
        - Name - maksimum 100 simvol, təkrarlanmamalıdır,
        - Price - müsbət olmalıdır,
        - CategoryId (Foreign Key)
    - Categories
        - Id (Primary Key)
        - Name - maksimum 30 simvol, təkrarlanmamalıdır
    - Orders
        - Id (Primary Key)
        - CustomerId (Foreign Key)
        - ProductId (Foreign Key)
        - OrderDate - gələcək zaman ola bilməz,
        - Quantity - müsbət olmalıdır

2. Qurulmalı olan relationlar:
    - Hər bir müştəri bir neçə sifariş verə bilər (One to Many)
    - Hər bir kateqoriya bir neçə məhsula aid ola bilər (One to Many)
    - Hər bir məhsul yalnız bir kateqoriyaya aid ola bilər (Many to One)

3. Hər cədvəl üçün bir neçə insert sorğusu

4. Join sorğuları:
    - Hər bir müştərinin verdiyi sifarişlərin siyahısı
    - Hər bir kateqoriya üzrə olan məhsulların siyahısı

5. SQL query-lər:
    - Hər bir müştərinin neçə sifariş verdiyini göstərən sorğu
    - Hər bir kateqoriya üzrə neçə məhsul olduğunu göstərən sorğu
    - Ən çox sifariş verən müştərini göstərən sorğu
