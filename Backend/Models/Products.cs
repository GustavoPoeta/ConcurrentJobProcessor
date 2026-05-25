using CsvHelper.Configuration.Attributes;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ConcurrentJobProcessor.Models
{
    public sealed class Products
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public List<string> Categories { get; set; } = new List<string>();
        public decimal Price { get; set; }

        public Products() { }

        public Products(string name, List<string> categories, decimal price)
        {
            Name = name;
            Categories = categories;
            Price = price;
        }
    }
}
