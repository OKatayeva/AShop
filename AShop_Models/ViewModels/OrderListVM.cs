using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AShop_Models.ViewModels
{
    public class OrderListVM
    {
        public IEnumerable<OrderHeader> OrderHeaderList { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }

        public string Status { get; set; }

        
    }
}
