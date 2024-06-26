using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.CustomerViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string? Branch { get; set; }

        public string? CostType { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public double? ProfitPercentage { get; set; }

        public double? ExtraProfitPercentage { get; set; }

        public double? ConstProfit { get; set; }

        public bool? Active { get; set; }

        public string? DeviceId { get; set; }

        public string? FirebaseToken { get; set; }

    }
}
