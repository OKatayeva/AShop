﻿using System;
using System.Collections.Generic;

namespace AShop_Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}