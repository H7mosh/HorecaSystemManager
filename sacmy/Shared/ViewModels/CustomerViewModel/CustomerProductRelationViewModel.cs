using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.CustomerViewModel
{
    public class CustomerProductRelationViewModel
    {
        public Guid Id { get; set; }
        public int CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public bool IsDiscounted { get; set; }
        public decimal? RaisePercentage { get; set; }
        public bool IsRaised { get; set; }
    }

}
