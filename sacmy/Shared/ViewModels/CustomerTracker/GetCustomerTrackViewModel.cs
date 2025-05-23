﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.CustomerTracker
{
    public class GetCustomerTrackViewModel
    {
        public int CustomerId { get; set; }
        public Guid TrackId { get; set; }
        public string Type { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
