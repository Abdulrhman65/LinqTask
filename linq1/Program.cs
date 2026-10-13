using linq1.DB;


namespace linq1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new AppDbContext();
            #region Level 1: Warm-up 
            #region 1.  For every delivered order, return OrderId, CustomerName, and Total, ordered by Total descending.
            //var DeliveredOrdersq1 = db.Orders.Where(o => o.Status == OrderStatus.Delivered)
            //                                 .Select(o => new
            //                                 {
            //                                     OrderId = o.Id,
            //                                     CustomerName = o.Customer.FullName,
            //                                     Total = o.Items.Sum(i => i.Quantity * i.UnitPrice * (1 - i.Discount))
            //                                 })
            //                                 .OrderByDescending(x => x.Total)
            //                                 .ToList();
            //foreach (var item in DeliveredOrdersq1)
            //{
            //    Console.WriteLine($"Order ID: {item.OrderId}, Customer Name: {item.CustomerName}, Total: {item.Total}");
            //}
            #endregion

            #region 2. Count how many orders exist per status, with the percentage of the whole. 
            //var OrderStatusCounts = db.Orders.GroupBy(o => o.Status)
            //                                .Select(g => new
            //                                {
            //                                    Status = g.Key,
            //                                    Count = g.Count(),
            //                                    Percentage = (double)g.Count() / db.Orders.Count() * 100
            //                                })
            //                                .ToList();
            //foreach (var item in OrderStatusCounts)
            //{
            //    Console.WriteLine($"Status: {item.Status}, Count: {item.Count}, Percentage: {item.Percentage:F2}%");
            //}
            #endregion

            #region 3. Return products whose price is above the average price of their own category. 
            //var AboveTheAveragePriceProducts = db.Products
            //                                     .Where(p => p.Price > p.Category.Products.Average(x => x.Price))
            //                                     .ToList();

            //foreach (var product in AboveTheAveragePriceProducts)
            //{
            //    Console.WriteLine($"Product ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Category ID: {product.CategoryId}");
            //}
            #endregion

            #region 4. Return products that were never ordered. 
            //var NeverOrderedProducts = db.Products.Where(p => !p.OrderItems.Any()).ToList();
            //foreach (var product in NeverOrderedProducts)
            //{
            //    Console.WriteLine($"Product ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Category ID: {product.CategoryId}");
            //}
            #endregion

            #region 5. Return customers who registered in the last 30 days and have at least one order.
            //var CustomersRegisteredLast30DaysWithOrders = db.Customers
            //    .Where(c => c.RegisteredAt >= DateTime.Now.AddDays(-30) && c.Orders.Any())
            //    .ToList();
            //foreach (var customer in CustomersRegisteredLast30DaysWithOrders)
            //{
            //    Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.FullName}, Registered At: {customer.RegisteredAt}");
            //}
            #endregion

            #region 6. Return customers who cancelled more than 2 orders, with the number of cancelled orders.
            //var CustomersWhocancelledMorethan2rders = db.Customers
            //                                             .Where(C => C.Orders.Count(O => O.Status == OrderStatus.Cancelled) > 2)
            //                                             .Select(C => new
            //                                             {
            //                                                 CustomerId = C.Id,
            //                                                 CustomerName = C.FullName,
            //                                                 CancelledOrdersCount = C.Orders.Count(O => O.Status == OrderStatus.Cancelled)

            //                                             });
            //foreach (var customer in CustomersWhocancelledMorethan2rders)
            //{
            //    Console.WriteLine($"Customer ID: {customer.CustomerId}, Name: {customer.CustomerName}, Cancelled Orders Count: {customer.CancelledOrdersCount}");
            //}
            #endregion
            #endregion

            #region Level 2: Grouping and aggregation
            #region 7. Top 5 customers by total spending: name, order count, total spent, average order value. 
            //var Top5CustomersByTotalSpending = db.Customers
            //                                     .Where(c => c.Orders.Any(o => o.Status == OrderStatus.Delivered))
            //                                     .Select(C => new
            //                                     {
            //                                         CustomerId = C.Id,
            //                                         CustomerName = C.FullName,
            //                                         OrderCount = C.Orders.Count(O => O.Status == OrderStatus.Delivered),
            //                                         TotalSpent = C.Orders
            //                                                       .Where(O => O.Status == OrderStatus.Delivered)
            //                                                       .Sum(O => O.Items.Sum(I => I.Quantity * I.UnitPrice * (1 - I.Discount))),
            //                                         AverageOrderValue = C.Orders
            //                                                              .Where(O => O.Status == OrderStatus.Delivered)
            //                                                              .Average(O => O.Items.Sum(I => I.Quantity * I.UnitPrice * (1 - I.Discount)))

            //                                     })
            //                                     .OrderByDescending(c => c.TotalSpent)
            //                                     .Take(5)
            //                                     .ToList();

            //foreach (var customer in Top5CustomersByTotalSpending)
            //{
            //    Console.WriteLine($"Customer ID: {customer.CustomerId}, Name: {customer.CustomerName}, Order Count: {customer.OrderCount}, Total Spent: {customer.TotalSpent}, Average Order Value: {customer.AverageOrderValue}");
            //}
            #endregion

            #region 8. Monthly report for 2025: Year, Month, OrderCount, Revenue, ordered by month. 
            //var MonthlyReportFor2025 = db.Orders
            //                             .Where(O => O.OrderDate.Year == 2025 && O.Status == OrderStatus.Delivered)
            //                             .GroupBy(O => new { O.OrderDate.Year, O.OrderDate.Month })
            //                             .Select(g => new
            //                             {
            //                                 g.Key.Year,
            //                                 g.Key.Month,
            //                                 OrderCount = g.Count(),
            //                                 Revenue = g.Sum(o => o.Items.Sum(i => i.Quantity * i.UnitPrice * (1 - i.Discount)))
            //                             })
            //                             .OrderBy(O => O.Month);
            //
            //foreach (var order in MonthlyReportFor2025)
            //{
            //    Console.WriteLine($"Year: {order.Year}, Month: {order.Month}, Order Count: {order.OrderCount}, Revenue: {order.Revenue}");
            //}
            #endregion

            #region 9. Revenue per category, ordered by revenue descending. 
            //var revenuePerCat = db.Categories
            //                      .Select(C => new
            //                      {
            //                          CategoryName = C.Name,
            //                          Revenue = C.Products
            //                                     .SelectMany(p => p.OrderItems)
            //                                     .Where(oi => oi.Order.Status == OrderStatus.Delivered)
            //                                     .Sum(OI => OI.Quantity * OI.UnitPrice * (1 - OI.Discount))
            //                      })
            //                      .OrderByDescending(C => C.Revenue)
            //                      .ToList();
            //foreach (var cat in revenuePerCat)
            //{
            //    Console.WriteLine($"Category: {cat.CategoryName}, Revenue: {cat.Revenue}");
            //}
            #endregion

            #region 10. Count customers who never placed an order, grouped by city, ordered by count descending.   
            //var CustomersNeverOrdered = db.Customers
            //                              .Where(C => !C.Orders.Any())
            //                              .GroupBy(C => C.City)
            //                              .Select(G => new
            //                              {
            //                                  City = G.Key,
            //                                  Count = G.Count()
            //                              })
            //                              .OrderByDescending(G => G.Count)
            //                              .ToList();
            //foreach (var item in CustomersNeverOrdered)
            //{
            //    Console.WriteLine($"City: {item.City}, Count: {item.Count}");
            //}
            #endregion

            #region 11. Products with at least 10 reviews and an average rating of 4.5 or more: name, category, review count, average rating.
            //var ProductsWithHighReviews = db.Products
            //                                .Where(p => p.Reviews.Count() >= 10 && p.Reviews.Average(r => r.Rating) >= 4.5)
            //                                .Select(P => new
            //                                {
            //                                    ProductName = P.Name,
            //                                    CategoryName = P.Category.Name,
            //                                    ReviewCount = P.Reviews.Count(),
            //                                    AverageRating = P.Reviews.Average(r => r.Rating)
            //                                })
            //                                .ToList();
            //foreach (var product in ProductsWithHighReviews)
            //{
            //    Console.WriteLine($"Product: {product.ProductName}, Category: {product.CategoryName}, Reviews: {product.ReviewCount}, Average Rating: {product.AverageRating}");
            //}
            #endregion

            #region 12. For each category, return the top 2 most expensive products.
            //var Highest2ProductsPerCategory = db.Categories
            //                            .Select(C => new
            //                            {
            //                                CategoryName = C.Name,
            //                                TopProducts = C.Products
            //                                               .OrderByDescending(P => P.Price)
            //                                               .Take(2)
            //                                               .Select(P => new
            //                                               {
            //                                                   ProductName = P.Name,
            //                                                   Price = P.Price
            //                                               })
            //                                               .ToList()
            //                            })
            //                            .ToList();
            //foreach (var item in Highest2ProductsPerCategory)
            //{
            //    Console.WriteLine($"Category: {item.CategoryName}");
            //    foreach (var product in item.TopProducts)
            //    {
            //        Console.WriteLine($"  Product: {product.ProductName}, Price: {product.Price}");
            //    }
            //}
            #endregion

            #region 13. Products whose stock is lower than the total quantity in Pending orders. Show name, stock, and pending quantity.
            //var LowStockProducts = db.Products
            //                         .Where(p => p.Stock < p.OrderItems
            //                                                .Where(p => p.Order.Status == OrderStatus.Pending)
            //                                                .Sum(p => p.Quantity))
            //                         .Select(p => new
            //                         {
            //                             ProductName = p.Name,
            //                             Stock = p.Stock,
            //                             PendingQuantity = p.OrderItems
            //                                                 .Where(p => p.Order.Status == OrderStatus.Pending)
            //                                                 .Sum(p => p.Quantity)

            //                         }).ToList();
            //foreach (var item in LowStockProducts)
            //{
            //    Console.WriteLine($"Product: {item.ProductName}, Stock: {item.Stock}, Pending: {item.PendingQuantity}");   
            //}
            #endregion

            #region 14. Customers whose total spending is above the average spending of all customers.
            //var spending = db.Customers.Select(c => new
            //{
            //    c.FullName,
            //    Spent = c.Orders
            //             .Where(o => o.Status == OrderStatus.Delivered)
            //             .Sum(o => o.Items.Sum(i => i.Quantity * i.UnitPrice * (1 - i.Discount)))
            //});
            //var avg = spending.Average(x => x.Spent);
            //var highSpenders = spending.Where(x => x.Spent > avg).ToList();

            //foreach (var item in highSpenders)
            //{
            //    Console.WriteLine($"Name: {item.FullName}, Total Spendings: {item.Spent}");
            //}
            #endregion
            #endregion

            #region Level 3: Joins, subqueries, set logic
            #region 15. For each category, return its best-selling product by quantity. Categories with no sales must still appear.
            //var BestSellingProduct = db.Categories
            //             .Select(c => new
            //             {
            //                 CategoryName = c.Name,
            //                 Best = c.Products
            //                     .Select(p => new
            //                     {
            //                         p.Name,
            //                         Qty = p.OrderItems
            //                                .Where(oi => oi.Order.Status == OrderStatus.Delivered)
            //                                .Sum(oi => oi.Quantity)
            //                     })
            //                     .Where(x => x.Qty > 0)
            //                     .OrderByDescending(x => x.Qty)
            //                     .ThenBy(x => x.Name)
            //                     .FirstOrDefault()
            //             })
            //             .ToList();
            //foreach (var item in BestSellingProduct)
            //{
            //    Console.WriteLine($"Category: {item.CategoryName}, Best: {item.Best?.Name ?? "None"}, Qty: {item.Best?.Qty ?? 0}");
            //}
            #endregion

            #region 16. Customers who bought every product in the category "Laptops". 
            //var CustomersWhoBoughtAllLaptopProducts = db.Customers
            //                    .Where(c => db.Products
            //                                  .Where(p => p.Category.Name == "Laptops")
            //                                  .All(p => c.Orders.Any(o => o.Status == OrderStatus.Delivered && o.Items.Any(i => i.ProductId == p.Id))))
            //                    .ToList();
            //foreach (var Customer in CustomersWhoBoughtAllLaptopProducts)
            //{
            //    Console.WriteLine($"Customer: {Customer.FullName}");
            //}
            #endregion

            #region 17. Reviews written by a customer for a product they never bought.
            //var ReviewsByCustomerForProductsNotBought = db.Reviews
            //                                            .Where(r => !r.Customer.Orders.Any(o => o.Items.Any(oi => oi.ProductId == r.ProductId)))
            //                                            .Select(r => new
            //                                            {
            //                                                CustomerName = r.Customer.FullName,
            //                                                ProductName = r.Product.Name,
            //                                                ReviewRating = r.Rating,
            //                                                ReviewComment = r.Comment
            //                                            })
            //                                            .ToList();
            //foreach (var review in ReviewsByCustomerForProductsNotBought)
            //{
            //    Console.WriteLine($"Customer: {review.CustomerName}, Product: {review.ProductName}, Rating: {review.ReviewRating}, Comment: {review.ReviewComment}");
            //}
            #endregion

            #region 18. Return all customers with their order count, including customers with zero orders (left join with GroupJoin).
            //var CustomersWithOrderCount = db.Customers
            //                                .GroupJoin(db.Orders,
            //                                           c => c.Id,
            //                                           o => o.CustomerId,
            //                                           (c, orders) => new
            //                                           {
            //                                               CustomerName = c.FullName,
            //                                               OrderCount = orders.Count()
            //                                           }).ToList();
            //foreach (var Customer in CustomersWithOrderCount)
            //{
            //    Console.WriteLine($"Customer: {Customer.CustomerName}, Order Count: {Customer.OrderCount}");
            //}
            #endregion

            #region 19. The top 5 product pairs that appear together in the same order most often.
            //var Top5ProductPairs = db.Orders
            //                        .SelectMany(o => o.Items.SelectMany(i1 => o.Items.Select(i2 => new { Product1Name = i1.Product.Name,Product1Id = i1.Product.Id, Product2Name = i2.Product.Name, Product2Id = i2.Product.Id })))
            //                        .Where(p => p.Product1Id < p.Product2Id)
            //                        .GroupBy(p => new { p.Product1Name, p.Product2Name })
            //                        .Select(g => new
            //                        {
            //                            Product1Name = g.Key.Product1Name,
            //                            Product2Name = g.Key.Product2Name,
            //                            Count = g.Count()
            //                        })
            //                        .OrderByDescending(g => g.Count)
            //                        .Take(5)
            //                        .ToList();
            //foreach (var product in Top5ProductPairs)
            //{
            //    Console.WriteLine($"Product 1: {product.Product1Name}, Product 2: {product.Product2Name}, Count: {product.Count}");
            //}                     
            #endregion

            #region 20. For each customer, return their name and their most recent order with each item's product name and quantity, in one query. 
            //var CustomersWithMostRecentOrder = db.Customers
            //                                     .Select(c => new
            //                                     {
            //                                         c.FullName,
            //                                         MostRecentOrder = c.Orders
            //                                                                .OrderByDescending(o => o.OrderDate)
            //                                                                .Where(o => o.Status == OrderStatus.Delivered)
            //                                                                .Select(o => new
            //                                                                {
            //                                                                    OrderId = o.Id,
            //                                                                    o.OrderDate,
            //                                                                    Items = o.Items.Select(i => new
            //                                                                    {
            //                                                                        ProductName = i.Product.Name,
            //                                                                        i.Quantity
            //                                                                    }).ToList()
            //                                                                })
            //                                                                .FirstOrDefault()
            //                                     }).ToList();
            //foreach (var c in CustomersWithMostRecentOrder)
            //{
            //    Console.WriteLine($"Customer: {c.FullName}");
            //    if (c.MostRecentOrder == null) { Console.WriteLine("  No orders"); continue; }

            //    Console.WriteLine($"  OrderID ({c.MostRecentOrder.OrderId}), Date: {c.MostRecentOrder.OrderDate:yyyy-MM-dd}");
            //    foreach (var item in c.MostRecentOrder.Items)
            //        Console.WriteLine($"    {item.ProductName} x{item.Quantity}");
            //}
            #endregion

            #region 21. For each customer, return their favorite category (the category with the highest total quantity bought).
            //var FavoriteCatPerCustomer = db.Customers
            //                               .Select(C=> new
            //                               {
            //                                   C.FullName,
            //                                   FavCat = C.Orders
            //                                             .Where(O => O.Status == OrderStatus.Delivered)
            //                                             .SelectMany(O => O.Items)
            //                                             .GroupBy(I => I.Product.Category.Name)
            //                                             .Select(G => new
            //                                             {
            //                                                 CategoryName = G.Key,
            //                                                 TotalQuantity = G.Sum(I => I.Quantity)
            //                                             })
            //                                             .OrderByDescending(G => G.TotalQuantity)
            //                                             .FirstOrDefault()
            //                               }).ToList();
            //foreach (var Cat in FavoriteCatPerCustomer)
            //{
            //    Console.WriteLine($"Customer: {Cat.FullName}, Favorite Category: {Cat.FavCat?.CategoryName ?? "N/A"}, Total Quantity: {Cat.FavCat?.TotalQuantity ?? 0}");
            //}
            #endregion

            #region 22. Rank products by revenue (1, 2, 3 ...) and return Rank, Product, Revenue.
            //var OrderedByRevenueProducts = db.Products
            //                                .Select(p => new
            //                                {
            //                                    p.Name,
            //                                    Revenue = p.OrderItems
            //                                                .Where(oi => oi.Order.Status == OrderStatus.Delivered)
            //                                                .Sum(oi => oi.Quantity * oi.UnitPrice * (1 - oi.Discount))
            //                                })
            //                                .OrderByDescending(p => p.Revenue)
            //                                .ToList()
            //                                .Select((p, index) => new
            //                                {
            //                                    Rank = index + 1,
            //                                    p.Name,
            //                                    p.Revenue
            //                                })
            //                                .ToList();

            //foreach (var product in OrderedByRevenueProducts)
            //{
            //    Console.WriteLine($"{product.Rank} - {product.Name} - {product.Revenue}");
            //}
            #endregion
            #endregion

            #region Level 4: Real-world methods 
            #region 23.Write SearchProducts(ProductFilter f).It applies only the filters that were provided(text,category, min / max price, in-stock only), supports sorting by name, price, or rating, and returns a page of results with the total count.
            //Console.Write("Enter the search text: ");
            //var searchText = Console.ReadLine();
            //Console.Write("Enter the category ID (or press Enter to skip): ");
            //var categoryIdInput = Console.ReadLine();
            //Console.Write("Enter the minimum price (or press Enter to skip): ");
            //var minPriceInput = Console.ReadLine();
            //Console.Write("Enter the maximum price (or press Enter to skip): ");
            //var maxPriceInput = Console.ReadLine();
            //Console.Write("Enter 'T','true' for viewing only in-stock products, 'F','false' for all products (or press Enter to skip): ");
            //var inStockOnlyInput = Console.ReadLine();
            //Console.Write("Sort by (1.name, 2.price, 3.rating): ");
            //string op = Console.ReadLine();
            //Console.Write("1.asc 2.desc");
            //bool isAscending = Console.ReadLine() == "1";

            //ProductFilter filter = new()
            //{
            //    SearchText = string.IsNullOrWhiteSpace(searchText) ? null : searchText,
            //    CategoryId = int.TryParse(categoryIdInput, out var categoryId) ? categoryId : (int?)null,
            //    MinPrice = decimal.TryParse(minPriceInput, out var minPrice) ? minPrice : (decimal?)null,
            //    MaxPrice = decimal.TryParse(maxPriceInput, out var maxPrice) ? maxPrice : (decimal?)null,
            //    InStockOnly = inStockOnlyInput?.Trim().ToLower() is "true" or "y" or "yes" or "1",
            //    SortBy = op switch
            //    {
            //        "2" => SortOption.Price,
            //        "3" => SortOption.Rating,
            //        _ => SortOption.Name
            //    },
            //    IsAscending = isAscending,
            //    PageNumber = 1, 
            //    PageSize = 10
            //};

            //var (products, totalCount) = SearchProducts(filter, db.Products);
            //Console.WriteLine($"\nTotal Products Found: {totalCount}");
            //Console.WriteLine($"Showing Page {filter.PageNumber} (Up to {filter.PageSize} items):");
            //Console.WriteLine(new string('-', 40));
            //foreach (var p in products)
            //{
            //    Console.WriteLine($"ID: {p.Id} | Name: {p.Name} | Price: {p.Price} | Stock: {p.Stock}");
            //}
            #endregion

            #region 24. Increase the price by 10% for products in "Accessories" with stock below 5, without loading them into memory.
            //var accessoriesCategory = db.Products.Where(p => p.Category.Name == "Accessories" && p.Stock < 5).Select(p => new { p.Id, p.Name, p.Price });
            //foreach (var item in accessoriesCategory)
            //{
            //    Console.WriteLine($"{item.Id} - Name: {item.Name} | Price: {item.Price}");
            //}
            //db.Products.Where(p => p.Category.Name == "Accessories" && p.Stock < 5)
            //           .ExecuteUpdate(p => p.SetProperty(x => x.Price, x => x.Price * 1.10m));

            //foreach (var item in db.Products.Where(p => p.Category.Name == "Accessories" && p.Stock < 5).Select(p => new { p.Id, p.Name, p.Price }))
            //{
            //    Console.WriteLine($"{item.Id} - Name: {item.Name} | Price: {item.Price}");
            //}
            #endregion

            #region 25. Delete all cancelled orders older than 1 year without loading them into memory.
            //var cutoff = DateTime.UtcNow.AddYears(-1);

            //var oldCancelled = db.Orders
            //    .Where(o => o.Status == OrderStatus.Cancelled && o.OrderDate < cutoff);

            //Console.WriteLine($"Before: Orders = {db.Orders.Count()}, Items = {db.Set<OrderItem>().Count()}");

            //var deleted = oldCancelled.ExecuteDelete();

            //Console.WriteLine($"Deleted: {deleted}");
            //Console.WriteLine($"After:  Orders = {db.Orders.Count()}, Items = {db.Set<OrderItem>().Count()}");
            #endregion
            #endregion

            // Bonus
            /*
             a:
                var query = MonthlyReportFor2025.ToQueryString();
                Console.WriteLine(query + "\n");
             b:
                N+1 problem:
                    1 query to fetch the list + 1 extra query for each item in it.
                    So N items cost N+1 round trips to the database.
                    يعني كويري بتتنفذ اكتر من مرة لأنها داخل لووب
                in q20 (written with foreach):
                    1 query for customers, then a query inside the loop for each customer.
                    With 12 customers that is 13 queries at least (22 if the items are fetched in the loop too), instead of a single query.
                    لو كتبنا الكويري داخل اللوب هتتنفذ 12 مرة لو عندنا مثلا 12 عميل بالإضافة للكويري الي جابت العملاء كمان = 13 دا غير لو في كويري دا خل كويري في اللوب
                solve:
                    Use one query with Select and navigation properties,
                    or Include/ThenInclude to load the related data together.
                    بننفذ المطلوب في كويري واحدة بتتنفذ خارج اللوب
              c:
                Problem:
                    The LINQ expression could not be translated, because the sql has no index without (orderby)
                    انتتي فريم وورك مبيقدرش يترجمها لأن الكويري مبترجعش مجموعة مرتبة الا كويريز الترتيب 
                Solve:
                   order it, return it into a list, then use the list index
                    لازم ترجعها ف ليست + رتبها أولا عشان ميبقاش عشوائي وكدا تقدر توصل للإندكس
             */            
        }
        //public static (List<Product> Products, int TotalCount) SearchProducts(ProductFilter f, IQueryable<Product> products)
        //{
        //    var filtered = products;
        //    if (!string.IsNullOrEmpty(f.SearchText))
        //    {
        //        filtered = filtered.Where(p => p.Name.Contains(f.SearchText));
        //    }
        //    if (f.CategoryId.HasValue)
        //    {
        //        filtered = filtered.Where(p => p.CategoryId == f.CategoryId);
        //    }
        //    if (f.MinPrice.HasValue)
        //    {
        //        filtered = filtered.Where(p => p.Price >= f.MinPrice);
        //    }
        //    if (f.MaxPrice.HasValue)
        //    {
        //        filtered = filtered.Where(p => p.Price <= f.MaxPrice);
        //    }
        //    if (f.InStockOnly)
        //    {
        //        filtered = filtered.Where(p => p.Stock > 0);
        //    }
        //    int totalCount = filtered.Count();

        //    filtered = f.SortBy switch
        //    {
        //        SortOption.Name => f.IsAscending ? filtered.OrderBy(p => p.Name) : filtered.OrderByDescending(p => p.Name),
        //        SortOption.Price => f.IsAscending ? filtered.OrderBy(p => p.Price) : filtered.OrderByDescending(p => p.Price),
        //        SortOption.Rating => f.IsAscending
        //                                ? filtered.OrderBy(p => p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0)
        //                                          .ThenBy(p => p.Id) : filtered.OrderByDescending(p => p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0)
        //                                          .ThenBy(p => p.Id),
        //        _ => filtered.OrderBy(p => p.Id),
        //    };
        //    var result = filtered.Skip((f.PageNumber - 1) * f.PageSize)
        //                         .Take(f.PageSize)
        //                         .ToList(); 

        //    return (result, totalCount);
        //}
    }
}
