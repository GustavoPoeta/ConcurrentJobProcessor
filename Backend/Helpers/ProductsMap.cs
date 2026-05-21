using ConcurrentJobProcessor.Models;
using CsvHelper.Configuration;

namespace ConcurrentJobProcessor.Helpers
{
    public sealed class ProductsMap : ClassMap<Products>
    {
        public ProductsMap()
        {
            Map(m => m.Name).Name("name");

            Map(m => m.Categories)
                .Name("categories")
                .TypeConverter<CategoryConverter>();

            Map(m => m.Price).Name("price");
        }
    }
}
