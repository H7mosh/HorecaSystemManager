using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.Products
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public string ImageLink { get; set; }
    }

    public class UpdateProductViewModel
    {
        public Guid ProductId { get; set; }
        public Guid? BrandId { get; set; }
        public string? CollectionId { get; set; }
        public string? CategoryId { get; set; }
        public Guid? CatalogId { get; set; }
        public string? Sku { get; set; }
        public string? PatternNumber { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public int? Points { get; set; }          
        public int? Quantity { get; set; }        
        public string? InnerType { get; set; }
        public int? InnerTypeCount { get; set; }  
        public string? OuterType { get; set; }
        public int? OuterTypeCount { get; set; }  
        public double? Height { get; set; }
        public double? Diameter { get; set; }
        public double? Top { get; set; }
        public double? Base { get; set; }
        public double? Volume { get; set; }
        public double? Weight { get; set; }
        public double? Area { get; set; }
        public string? Ean { get; set; }
    }
}
