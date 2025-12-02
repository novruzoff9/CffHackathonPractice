# SQL Library Management System
1. Aşağıdakı cədvəllər yaradılacaq:
    - Members
        - Id (Primary Key)
        - FirstName - maksimum 20 simvol, 
        - LastName - maksimum 25 simvol, 
        - RegistrationDate - gələcək zaman ola bilməz,
        - Email - təkrarlanmamalıdır
    - Books
        - Id (Primary Key)
        - Title - maksimum 100 simvol, təkrarlanmamalıdır,
        - GenreId (Foreign Key) 
    - Genres
        - Id (Primary Key)
        - Name - maksimum 50 simvol, təkrarlanmamalıdır,
    - Authors
        - Id (Primary Key)
        - FirstName - maksimum 30 simvol,
        - LastName - maksimum 30 simvol
    - BookAuthors
        - BookId (Foreign Key)
        - AuthorId (Foreign Key)
    - Loans
        - Id (Primary Key)
        - MemberId (Foreign Key)
        - BookId (Foreign Key)
        - LoanDate - gələcək zaman ola bilməz,
        - ReturnDate - LoanDate-dən kiçik ola bilməz,
2. Qurulmalı olan relationlar:
    - Hər bir kitabın bir neçə müəllifi ola bilər (Many to Many)
    - Hər bir müəllif bir neçə kitaba aid ola bilər (Many to Many)
    - Hər bir üzv bir neçə kitab götürə bilər (One to Many)
    - Hər bir janr bir neçə kitaba aid ola bilər (One to Many)
    - Hər bir kitab yalnız bir janra aid ola bilər (Many to One)

3. Hər cədvəl üçün bir neçə insert sorğusu
4. Join sorğuları:
    - Hər bir üzvün götürdüyü kitabların siyahısı
    - Hər bir kitabın müəlliflərinin siyahısı
    - Hər bir janr üzrə olan kitabların siyahısı
    - Hər bir müəllifin yazdığı kitabların siyahısı
5. Sql query-lər:
    - Hər bir üzvün neçə kitab götürdüyünü göstərən sorğu
    - Hər bir müəllifin neçə kitab yazdığını göstərən sorğu
    - Hər bir janr üzrə neçə kitab olduğunu göstərən sorğu
    - Ən çox kitab götürən üzvü göstərən sorğu
    - Ən çox kitab yazan müəllifi göstərən sorğu