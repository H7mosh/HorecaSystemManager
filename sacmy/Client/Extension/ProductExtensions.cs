using sacmy.Shared.ViewModels.Products;

namespace sacmy.Client.Extension
{
    public static class TableExtensions
    {
        public static IEnumerable<Product> OrderByDirection(this IEnumerable<Product> source, string column, bool ascending)
        {
            if (source == null) return Enumerable.Empty<Product>();

            return column.ToLower() switch
            {
                "sku" => ascending
                    ? source.OrderBy(p => p.Sku)
                    : source.OrderByDescending(p => p.Sku),

                "name" => ascending
                    ? source.OrderBy(p => p.Name)
                    : source.OrderByDescending(p => p.Name),

                "price" => ascending
                    ? source.OrderBy(p => p.Price ?? 0)
                    : source.OrderByDescending(p => p.Price ?? 0),

                "quantity" => ascending
                    ? source.OrderBy(p => p.Quantity ?? 0)
                    : source.OrderByDescending(p => p.Quantity ?? 0),

                _ => ascending
                    ? source.OrderBy(p => p.Name)
                    : source.OrderByDescending(p => p.Name)
            };
        }
    }
}
