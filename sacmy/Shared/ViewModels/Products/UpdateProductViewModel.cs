using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.Products
{
    public class UpdateProductViewModel
    {
        /// <summary>
        /// The ID of the product to update (required).
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Optional: If null, do not update BrandId.
        /// </summary>
        public Guid? BrandId { get; set; }

        /// <summary>
        /// Optional: If null, do not update CollectionId.
        /// </summary>
        public string? CollectionId { get; set; }

        /// <summary>
        /// Optional: If null, do not update CategoryId.
        /// </summary>
        public string? CategoryId { get; set; }

        /// <summary>
        /// Optional: If null, do not update MaterialId.
        /// </summary>
        public string? MaterialId { get; set; }

        /// <summary>
        /// Optional: If null, do not update CatalogId.
        /// </summary>
        public Guid? CatalogId { get; set; }

        /// <summary>
        /// Optional: If null, do not update Sku.
        /// </summary>
        public string? Sku { get; set; }

        /// <summary>
        /// Optional: If null, do not update PatternNumber.
        /// </summary>
        public string? PatternNumber { get; set; }

        /// <summary>
        /// Optional: If null, do not update product Name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Optional: If null, do not update product Description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Optional: If null, do not update Price.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Optional: If null, do not update Points.
        /// </summary>
        public int? Points { get; set; }

        /// <summary>
        /// Optional: If null, do not update Quantity.
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Optional: If null, do not update InnerType.
        /// </summary>
        public string? InnerType { get; set; }

        /// <summary>
        /// Optional: If null, do not update InnerTypeCount.
        /// </summary>
        public int? InnerTypeCount { get; set; }

        /// <summary>
        /// Optional: If null, do not update OuterType.
        /// </summary>
        public string? OuterType { get; set; }

        /// <summary>
        /// Optional: If null, do not update OuterTypeCount.
        /// </summary>
        public int? OuterTypeCount { get; set; }

        /// <summary>
        /// Optional: If null, do not update Height.
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Optional: If null, do not update Diameter.
        /// </summary>
        public double? Diameter { get; set; }

        /// <summary>
        /// Optional: If null, do not update Top.
        /// </summary>
        public double? Top { get; set; }

        /// <summary>
        /// Optional: If null, do not update Base.
        /// </summary>
        public double? Base { get; set; }

        /// <summary>
        /// Optional: If null, do not update Volume.
        /// </summary>
        public decimal? Volume { get; set; }

        /// <summary>
        /// Optional: If null, do not update Weight.
        /// </summary>
        public double? Weight { get; set; }

        /// <summary>
        /// Optional: If null, do not update Area.
        /// </summary>
        public double? Area { get; set; }

        /// <summary>
        /// Optional: If null, do not update Upc.
        /// </summary>
        public string? Upc { get; set; }

        /// <summary>
        /// Optional: If null, do not update Ean.
        /// </summary>
        public string? Ean { get; set; }

        /// <summary>
        /// A list of image links to replace the existing images. 
        /// If null, the images are not touched. 
        /// If an empty list is passed, all images are removed.
        /// </summary>
        public List<string>? ImageLinks { get; set; }
    }

}
