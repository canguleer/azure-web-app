using azure_web_app.Models;

namespace azure_web_app.Services
{
    /// <summary>
    /// Service demonstrating the difference between ToList() and AsEnumerable()
    /// This service is for educational/demonstration purposes to show LINQ best practices.
    /// </summary>
    public class ProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 1200, Category = "Electronics", IsAvailable = true },
                new Product { Id = 2, Name = "Mouse", Price = 25, Category = "Electronics", IsAvailable = true },
                new Product { Id = 3, Name = "Keyboard", Price = 75, Category = "Electronics", IsAvailable = false },
                new Product { Id = 4, Name = "Monitor", Price = 300, Category = "Electronics", IsAvailable = true },
                new Product { Id = 5, Name = "Desk", Price = 200, Category = "Furniture", IsAvailable = true }
            };
        }

        /// <summary>
        /// Example using ToList() - Use when you need to materialize the query results
        /// and avoid multiple enumerations or when you need to modify the collection
        /// </summary>
        public List<Product> GetAvailableProductsWithToList()
        {
            // ToList() immediately executes the query and creates a new List in memory
            // Use this when:
            // 1. You'll enumerate the results multiple times
            // 2. You need to modify the collection after querying
            // 3. You want to ensure the query executes at this point
            var result = _products
                .Where(p => p.IsAvailable)
                .OrderBy(p => p.Price)
                .ToList();

            return result;
        }

        /// <summary>
        /// Example using AsEnumerable() - Use when switching from IQueryable to IEnumerable
        /// for client-side evaluation or when you want deferred execution
        /// </summary>
        public IEnumerable<Product> GetAvailableProductsWithAsEnumerable()
        {
            // AsEnumerable() doesn't execute the query immediately (deferred execution)
            // Use this when:
            // 1. You want to switch from IQueryable to IEnumerable (client-side evaluation)
            // 2. You want deferred execution (query runs when you enumerate)
            // 3. You only need to enumerate once and don't need to modify the collection
            var result = _products
                .Where(p => p.IsAvailable)
                .OrderBy(p => p.Price)
                .AsEnumerable();

            return result;
        }

        /// <summary>
        /// Example showing when ToList() is necessary to avoid multiple enumeration issues
        /// </summary>
        public (int count, decimal total) GetProductStatsWithToList()
        {
            // Without ToList(), the query would execute twice (once for Count, once for Sum)
            var availableProducts = _products
                .Where(p => p.IsAvailable)
                .ToList();

            return (availableProducts.Count, availableProducts.Sum(p => p.Price));
        }

        /// <summary>
        /// Example showing deferred execution with AsEnumerable()
        /// This is efficient when you only enumerate once
        /// </summary>
        public IEnumerable<string> GetProductNamesWithAsEnumerable()
        {
            // The query won't execute until the result is enumerated
            return _products
                .Where(p => p.IsAvailable)
                .AsEnumerable()
                .Select(p => p.Name);
        }
    }
}
