# Karqo Sistemi:
1. Yaradılmalı olan class-lar aşağıdakılardır. Mütləq şəkildə instance-lar `constructor` ilə yaradılmalıdır
    - `Customer`
        - Id (statik olaraq artacaq şəkildə olmalıdır)
        - Adı
        - Şəhəri
    - `Courier`
        - Id (statik olaraq artacaq şəkildə olmalıdır)
        - Adı
        - IsAvailable (bu property-i kuryerin hal hazirda sifariş qəbul etməyə uyğun olub olmamasını göstərir. Bununla bağlı əməliyyatları aşağıda görəcəksiniz)
    - `CargoOrder`
        - Id (statik olaraq artacaq şəkildə olmalıdır)
        - CustomerId (Sifarişin hansı müştəriyə getdiyi)
        - CourierId (Hansı kuryer tərəfindən çatdırıldığı)
        - Ümumi məbləğ
        - OrderStatus (enum-dır, 2-ci tapşırıqda ətraflı verilib) (default dəyəri `Created` olmalıdır)
        - CreatedAt (nə vaxt yarandığı, default olaraq indiki vaxtı göstərsin)
        - `UpdateStatus()` metodu:
            Bu metod `OrderStatus` tipindən bir dəyər qəbul edəcək. Sifarişin statusunu yeniləmək üçün istifadə olunacaq. Lakin burda əlavə yoxlama aparılacaq ki, əgər sifariş ləğv edilibsə və ya tamamlanıbsa statusu dəyi.mək mümkün olmasın. 
2. `OrderStatus` adında enum yaradılmalıdır. Alacağı dəyərlər (Created, OnTheWay, Delivered, Cancelled)
3. `ICargoService` interfeysi:
    Özündə `Customer`, `Courier` və `CargoOrder` listləri saxyalacaq
    - `AddCustomer()`: Bir `Customer` qəbul edib müştəri listinə əlavə edəcək.
    - `AddCourier()`: Bir `Courier` qəbul edib müştəri listinə əlavə edəcək.
    - `CreateOrder()`: Buraya gələcək olan `CargoOrder` obyekti daxilində olan - `CustomerId` və `CourierId` dəyərləri yoxlanılacaq ki, bu dəyərlərə uyğun məlumat varmı (əgər tapılmasa exception göndərilsin). Əgər hər ikisi varsa daha sonra tapılan kuryerin sifarişə uyğun olub olmaması yoxlanılacaq yəni onun `IsAvialable` propertisi. Əgər kuryer sifarişə uyğun olmasa `Exception` atılacaq (custom yaratmağınız daha yaxşı olar). Əgər uyğundursa onu qeyd edəcək və həmin kuryer artıq sifariş qəbul edə bilməyəcək.
    - `CompleteOrder()`: id qəbul edəcək. Həmin id-də order yoxdursa xəta verəcək. Əgər varsa, həmin order-i artıq tamamlanmış olaraq (Delivered) qeyd edəcək. Həmçinin həmin sifariş üçün ayrılmış olan kuryeri artıq yeni sifariş qəbul etməyə uyğun formata gətirəcək.