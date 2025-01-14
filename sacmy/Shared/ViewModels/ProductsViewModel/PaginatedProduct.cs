﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.ProductsViewModel
{

    public class ProductsPaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1000;
    }

    // Then, create a pagination response model
    public class ProductsPaginatedResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}
