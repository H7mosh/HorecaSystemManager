using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.ProductsViewModel
{
    public class GetProductsViewModel
    {
        public Guid Id { get; set; }
        public string? BrandName { get; set; }
        public string? CollectionName { get; set; }
        public string Sku { get; set; }
        public string PatternNumber { get; set; }
        public int BoxCount { get; set; }
        public int PieceCount { get; set; }
        public string Image { get; set; }
    }
}
