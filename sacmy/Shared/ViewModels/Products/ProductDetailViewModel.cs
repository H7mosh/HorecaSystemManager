using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.Products
{
    public class ProductDetailViewModel
    {
        public string Id { get; set; }
        public string BrandId { get; set; }
        public string BrandName { get; set; }
        public string CollectionId { get; set; }
        public string CollectionName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string CatalogId { get; set; }
        public string CatalogName { get; set; }

        // Basic Info
        public string Sku { get; set; }
        public string PatternNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? Points { get; set; }
        public int? Quantity { get; set; }

        // Packaging Info
        public string InnerType { get; set; }
        public int InnerTypeCount { get; set; }
        public string InnerTypeImage { get; set; }
        public string OuterType { get; set; }
        public int OuterTypeCount { get; set; }
        public string OuterTypeImages { get; set; }

        // Measurements
        public double? Height { get; set; }
        public double? Diameter { get; set; }
        public double? Top { get; set; }
        public double? Base { get; set; }
        public double? Volume { get; set; }
        public double? Weight { get; set; }
        public double? Area { get; set; }

        // Product Identifiers
        public string Upc { get; set; }
        public string Ean { get; set; }

        // Metadata
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        // Related Collections
        public List<ProductImageViewModel> Images { get; set; } = new List<ProductImageViewModel>();
    }

    public class ProductImageViewModel
    {
        public string Id { get; set; }
        public string ImageLink { get; set; }
    }
}
