using System;
using System.Collections.Generic;
using System.Text;

namespace linq1.Models
{
    public class ProductFilter
    {
        // فلاتر البحث الاختيارية
        public string? SearchText { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool InStockOnly { get; set; }

        // إعدادات الترتيب
        public SortOption SortBy { get; set; }
        public bool IsAscending { get; set; } = true;

        // إعدادات تقسيم الصفحات (قيم افتراضية)
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public enum SortOption
    {
        Name,
        Price,
        Rating
    }
}
