﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.CustomerTracker
{
    public class DeptCustomerViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalTransTotalN { get; set; }
        public string HasRecentReceipt { get; set; }
    }

}
