using linq1.DB;
using linq1.Models;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Seed(AppDbContext db)
    {
        if (db.Customers.Any()) return; // يمنع تكرار الداتا

        var now = DateTime.UtcNow;

        // ---------- Categories ----------
        var laptops = new Category { Name = "Laptops" };
        var accessories = new Category { Name = "Accessories" };
        var phones = new Category { Name = "Phones" };
        var monitors = new Category { Name = "Monitors" };
        var books = new Category { Name = "Books" };      // مفيش عليها مبيعات

        // ---------- Products ----------
        var dell = new Product { Name = "Dell XPS 13", Price = 45000, Stock = 10, Category = laptops };
        var think = new Product { Name = "Lenovo ThinkPad", Price = 38000, Stock = 8, Category = laptops };
        var hp = new Product { Name = "HP Pavilion", Price = 28000, Stock = 15, Category = laptops };
        var mac = new Product { Name = "MacBook Air", Price = 60000, Stock = 5, Category = laptops };

        var mouse = new Product { Name = "Wireless Mouse", Price = 500, Stock = 100, Category = accessories };
        var keyboard = new Product { Name = "Mechanical Keyboard", Price = 1800, Stock = 3, Category = accessories };
        var hub = new Product { Name = "USB-C Hub", Price = 900, Stock = 4, Category = accessories };
        var bag = new Product { Name = "Laptop Bag", Price = 700, Stock = 20, Category = accessories };
        var webcam = new Product { Name = "Webcam", Price = 1200, Stock = 2, Category = accessories };

        var iphone = new Product { Name = "iPhone 15", Price = 40000, Stock = 12, Category = phones };
        var galaxy = new Product { Name = "Galaxy S24", Price = 35000, Stock = 9, Category = phones };
        var redmi = new Product { Name = "Redmi Note 13", Price = 9000, Stock = 30, Category = phones };   // عمره ما اتطلب

        var mon27 = new Product { Name = "27 inch Monitor", Price = 8000, Stock = 14, Category = monitors };
        var mon24 = new Product { Name = "24 inch Monitor", Price = 5000, Stock = 18, Category = monitors };

        var csharp = new Product { Name = "C# in Depth", Price = 900, Stock = 25, Category = books };    // عمره ما اتطلب
        var clean = new Product { Name = "Clean Code", Price = 750, Stock = 25, Category = books };    // عمره ما اتطلب

        // ---------- Customers ----------
        var ahmed = new Customer { FullName = "Ahmed Hassan", Email = "ahmed@mail.com", City = "Cairo", RegisteredAt = new DateTime(2024, 3, 10) };
        var sara = new Customer { FullName = "Sara Ali", Email = "sara@mail.com", City = "Alexandria", RegisteredAt = new DateTime(2024, 5, 2) };
        var omar = new Customer { FullName = "Omar Khaled", Email = "omar@mail.com", City = "Cairo", RegisteredAt = new DateTime(2024, 6, 15) };
        var mona = new Customer { FullName = "Mona Said", Email = "mona@mail.com", City = "Mansoura", RegisteredAt = new DateTime(2024, 8, 21) };
        var khaled = new Customer { FullName = "Khaled Youssef", Email = "khaled@mail.com", City = "Mansoura", RegisteredAt = new DateTime(2024, 9, 5) };
        var nour = new Customer { FullName = "Nour Ibrahim", Email = "nour@mail.com", City = "Giza", RegisteredAt = new DateTime(2024, 11, 30) };
        var youssef = new Customer { FullName = "Youssef Adel", Email = "youssef@mail.com", City = "Cairo", RegisteredAt = new DateTime(2025, 1, 12) };
        var hana = new Customer { FullName = "Hana Mostafa", Email = "hana@mail.com", City = "Alexandria", RegisteredAt = now.AddDays(-10) };  // جديد وليه طلب
        var karim = new Customer { FullName = "Karim Fathy", Email = "karim@mail.com", City = "Giza", RegisteredAt = now.AddDays(-20) };  // جديد وليه طلب
        var laila = new Customer { FullName = "Laila Nabil", Email = "laila@mail.com", City = "Mansoura", RegisteredAt = now.AddDays(-5) };   // جديد من غير طلب
        var tamer = new Customer { FullName = "Tamer Samir", Email = "tamer@mail.com", City = "Cairo", RegisteredAt = new DateTime(2025, 2, 1) };   // من غير طلب
        var dina = new Customer { FullName = "Dina Ramy", Email = "dina@mail.com", City = "Alexandria", RegisteredAt = new DateTime(2025, 3, 3) };   // من غير طلب

        var customers = new[] { ahmed, sara, omar, mona, khaled, nour, youssef, hana, karim, laila, tamer, dina };

        // ---------- Orders ----------
        // الـ UnitPrice بياخد سعر المنتج، والـ Discount نسبة (0.10 = 10%)
        static Order O(Customer c, DateTime date, OrderStatus s, params (Product p, int qty, decimal disc)[] items) => new()
        {
            Customer = c,
            OrderDate = date,
            Status = s,
            Items = items.Select(i => new OrderItem
            {
                Product = i.p,
                Quantity = i.qty,
                UnitPrice = i.p.Price,
                Discount = i.disc
            }).ToList()
        };

        const OrderStatus D = OrderStatus.Delivered;
        const OrderStatus S = OrderStatus.Shipped;
        const OrderStatus P = OrderStatus.Pending;
        const OrderStatus C = OrderStatus.Cancelled;

        var orders = new List<Order>
        {
            // ----- 2025: Delivered (للتقرير الشهري والإيرادات) -----
            // أحمد اشترى كل منتجات Laptops (سؤال 16)
            O(ahmed, new DateTime(2025, 1, 15), D, (dell, 1, 0m), (mouse, 2, 0m)),
            O(ahmed, new DateTime(2025, 2, 10), D, (think, 1, 0.05m), (bag, 1, 0m)),
            O(ahmed, new DateTime(2025, 3, 20), D, (hp, 1, 0m), (hub, 1, 0.10m)),
            O(ahmed, new DateTime(2025, 4, 5),  D, (mac, 1, 0.10m)),

            O(sara,  new DateTime(2025, 1, 25), D, (iphone, 1, 0m), (bag, 1, 0m)),
            O(sara,  new DateTime(2025, 3, 8),  D, (mon27, 2, 0.10m)),
            O(sara,  new DateTime(2025, 5, 14), D, (keyboard, 1, 0m), (mouse, 1, 0m)),

            O(omar,  new DateTime(2025, 2, 3),  D, (galaxy, 1, 0m)),
            O(omar,  new DateTime(2025, 4, 18), D, (dell, 1, 0.05m), (mon24, 1, 0m)),
            O(omar,  new DateTime(2025, 6, 2),  D, (mouse, 3, 0.10m), (keyboard, 1, 0.10m)),

            O(mona,  new DateTime(2025, 3, 12), D, (hp, 1, 0m)),
            O(mona,  new DateTime(2025, 5, 22), D, (mon24, 2, 0m), (hub, 2, 0m)),
            O(mona,  new DateTime(2025, 7, 9),  D, (iphone, 1, 0.05m)),

            O(khaled, new DateTime(2025, 4, 27), D, (mouse, 1, 0m)),
            O(khaled, new DateTime(2025, 8, 16), D, (think, 1, 0m), (bag, 2, 0m)),

            O(nour,  new DateTime(2025, 6, 30), D, (galaxy, 1, 0.10m), (hub, 1, 0m)),
            O(nour,  new DateTime(2025, 9, 11), D, (mon27, 1, 0m)),
            O(nour,  new DateTime(2025, 10, 5), D, (mouse, 2, 0m)),

            O(youssef, new DateTime(2025, 7, 21), D, (dell, 1, 0m)),
            O(youssef, new DateTime(2025, 11, 2), D, (keyboard, 1, 0m), (mouse, 1, 0m)),

            // ----- 2026: Delivered / Shipped -----
            O(ahmed,  new DateTime(2026, 1, 9),  D, (iphone, 1, 0m), (webcam, 1, 0m)),
            O(sara,   new DateTime(2026, 2, 14), D, (mac, 1, 0.05m)),
            O(omar,   new DateTime(2026, 3, 3),  D, (mouse, 2, 0m), (bag, 1, 0m)),
            O(mona,   new DateTime(2026, 4, 19), S, (galaxy, 1, 0m)),
            O(nour,   new DateTime(2026, 5, 25), S, (mon27, 1, 0.10m), (mon24, 1, 0.10m)),

            // ----- عملاء جدد (آخر 30 يوم) -----
            O(hana,  now.AddDays(-8), D, (mouse, 1, 0m), (bag, 1, 0m)),
            O(karim, now.AddDays(-15), S, (hub, 1, 0m)),

            // ----- Pending (سؤال 13: Webcam و Keyboard) -----
            O(ahmed, now.AddDays(-3), P, (webcam, 2, 0m), (keyboard, 2, 0m)),
            O(sara,  now.AddDays(-2), P, (webcam, 3, 0m), (hub, 2, 0m)),
            O(youssef, now.AddDays(-1), P, (keyboard, 2, 0m)),

            // ----- Cancelled -----
            // عمر ألغى 3 طلبات (سؤال 6)، واتنين منهم أقدم من سنة (سؤال 25)
            O(omar,   new DateTime(2025, 2, 20), C, (hp, 1, 0m)),
            O(omar,   new DateTime(2025, 4, 25), C, (mon27, 1, 0m)),
            O(omar,   now.AddDays(-40),          C, (mouse, 1, 0m)),
            // خالد ألغى طلبين بس
            O(khaled, new DateTime(2025, 6, 10), C, (iphone, 1, 0m)),
            O(khaled, now.AddDays(-60),          C, (bag, 1, 0m)),
            O(mona,   now.AddDays(-30),          C, (mon24, 1, 0m)),
        };

        // ---------- Reviews ----------
        var reviews = new List<Review>();

        // Wireless Mouse: 10 تقييمات بمتوسط 4.8 (سؤال 11)
        // وأغلب المقيّمين عمرهم ما اشتروه، فهتظهر في سؤال 17
        var mouseRatings = new[] { 5, 5, 5, 4, 5, 5, 4, 5, 5, 5 };
        for (int i = 0; i < mouseRatings.Length; i++)
        {
            reviews.Add(new Review
            {
                Product = mouse,
                Customer = customers[i],
                Rating = mouseRatings[i],
                Comment = $"Mouse review #{i + 1}",
                CreatedAt = now.AddDays(-100 + i * 5)
            });
        }

        // باقي التقييمات
        Review R(Product p, Customer c, int rating, string comment, int daysAgo) => new()
        {
            Product = p,
            Customer = c,
            Rating = rating,
            Comment = comment,
            CreatedAt = now.AddDays(-daysAgo)
        };

        reviews.AddRange(new[]
        {
            R(dell,  ahmed,  5, "Excellent laptop",        300),
            R(dell,  omar,   4, "Good performance",        250),
            R(mac,   sara,   5, "Worth every penny",       120),
            R(iphone, mona,  4, "Great camera",            200),
            R(keyboard, sara, 3, "A bit loud",             180),
            R(hub,   nour,   2, "Got hot quickly",         150),
            R(webcam, tamer, 1, "Never bought but heard",   90),   // تقييم بدون شراء
            R(mon27, dina,   4, "Looks nice at the shop",   60),   // تقييم بدون شراء
            R(csharp, karim, 5, "Great book",               20),   // تقييم بدون شراء
        });

        // ---------- Save ----------
        db.Categories.AddRange(laptops, accessories, phones, monitors, books);
        db.Customers.AddRange(customers);
        db.Orders.AddRange(orders);
        db.Reviews.AddRange(reviews);
        db.SaveChanges();
    }
}