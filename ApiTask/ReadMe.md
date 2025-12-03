# Generic Repository API Task
.NET 8 və ya .NET 10 ilə bir API layihəsi yaradın və Generic Repository Pattern-i tətbiq edin. Aşağıdakı tələbləri yerinə yetirin:
1. Aşağdakı table-lar olacaq:
    - Authors
        - Id (Primary Key)
        - FirstName - maksimum 30 simvol,
        - LastName - maksimum 30 simvol
    - Books
        - Id (Primary Key)
        - Title - maksimum 100 simvol, təkrarlanmamalıdır,
        - PublishedYear - 4 rəqəmli il formatında olmalıdır,
        - Price - mənfi ola bilməz,
        - Pages - mənfi ola bilməz,
        - AuthorId (Foreign Key)
2. Qurulmalı olan relationlar:
    - Hər bir müəllif bir neçə kitaba aid ola bilər (One to Many)
    - Hər bir kitab yalnız bir müəllifə aid ola bilər (Many to One)
3. Generic Repository Pattern-i tətbiq edin və aşağıdakı əməliyyatları həyata keçirən metodlar yaradın:
    - GetAllAsync
    - GetByIdAsync
    - AddAsync
    - UpdateAsync
    - DeleteAsync
4. Custom Exception-lar yaradın və onları repository metodlarında istifadə edin.
5. Bütün CRUD əməliyyatları üçün müvafiq API endpoint-lər yaradın.