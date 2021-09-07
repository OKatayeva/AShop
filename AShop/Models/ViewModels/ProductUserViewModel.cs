using System;
using System.Collections;
using System.Collections.Generic;

namespace AShop.Models.ViewModels
{
    public class ProductUserViewModel
    {
        public ProductUserViewModel()
        {
            ProductList = new List<Product>();
        }


        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<Product> ProductList { get; set; }
    }
}
