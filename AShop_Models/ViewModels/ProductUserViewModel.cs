using System;
using System.Collections;
using System.Collections.Generic;

namespace AShop_Models.ViewModels
{
    public class ProductUserViewModel
    {
        public ProductUserViewModel()
        {
            ProductList = new List<Product>();
        }


        public ApplicationUser ApplicationUser { get; set; }

        public IList<Product> ProductList { get; set; }
    }
}
