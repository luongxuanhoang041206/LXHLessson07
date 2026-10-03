using System.Collections.Generic;

namespace NetCoreMVCLAB5.Models;

public static class MockData
{
    public static List<Category> Categories { get; } = new List<Category>
    {
        new Category { Id = 1, Name = "Điện thoại & Phụ kiện" },
        new Category { Id = 2, Name = "Máy tính & Laptop" },
        new Category { Id = 3, Name = "Thiết bị điện tử" },
        new Category { Id = 4, Name = "Đồng hồ thông minh" }
    };

    public static List<Product> Products { get; } = new List<Product>
    {
        new Product
        {
            Id = 1,
            Name = "Điện thoại iPhone 15 Pro Max 256GB",
            Image = "/products/iphone15.jpg",
            Price = 30000000,
            SalePrice = 26500000,
            Description = "Điện thoại thông minh cao cấp cấu hình mạnh mẽ thiết kế sang trọng",
            CategoryId = 1
        },
        new Product
        {
            Id = 2,
            Name = "Laptop MacBook Pro 14 M3 Pro",
            Image = "/products/macbook.jpg",
            Price = 45000000,
            SalePrice = 39900000,
            Description = "Máy tính xách tay chuyên nghiệp dành cho lập trình viên và đồ họa",
            CategoryId = 2
        }
    };
}
