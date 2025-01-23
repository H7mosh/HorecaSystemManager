using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.Products
{
    public class BrandResponse
    {
        public string Id { get; set; }
        public string BrandEn { get; set; }
        public string BrandAr { get; set; }
        public string BrandTr { get; set; }
        public string BrandKr { get; set; }
        public string Image { get; set; }
        public BrandData Data { get; set; }
    }

    public class BrandData
    {
        public List<Advertise> Advertises { get; set; }
        public List<Category> Categories { get; set; }
        public List<Collection> Collections { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Advertise
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string Image { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string NameTr { get; set; }
        public string NameKr { get; set; }
        public string Image { get; set; }
    }

    public class Collection
    {
        public string Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string NameTr { get; set; }
        public string NameKr { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }
        public string SeriesOrCollectionId { get; set; }
        public string Sku { get; set; }
        public string PatternNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DinarPrice { get; set; }
        public int Points { get; set; }
        public int Quantity { get; set; }
        public int BoxCount { get; set; }
        public int PieceCount { get; set; }
        public decimal Volume { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsNew { get; set; }
        public bool IsDiscounted { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool IsRaised { get; set; }
        public decimal RaisedPercentage { get; set; }
        public string Image { get; set; }
    }
}
