using day3_DaoThanhDat.Models;

namespace day3_DaoThanhDat.Data
{
    public class SeedData
    {
        public static List<Category> categories = new List<Category>
        {
            new Category(){Id=1, Name="Category 1", Description="Description 1"},
            new Category(){Id=2, Name="Category 2", Description="Description 2"},
            new Category(){Id=3, Name="Category 3", Description="Description 3"},
        };

        public static List<Product> products = new List<Product>
        {
            new Product(){Id=1, Name="Product 1", Description="Description Product 1", Price=100, CategoryId=1},
            new Product(){Id=2, Name="Product 2", Description="Description Product 2", Price=200, CategoryId=2},
            new Product(){Id=3, Name="Product 3", Description="Description Product 3", Price=300, CategoryId=3},
        };
    }
}