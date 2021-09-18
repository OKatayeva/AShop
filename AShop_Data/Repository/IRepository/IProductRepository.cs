using System;
using System.Collections.Generic;
using AShop_Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AShop_Data.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
        IEnumerable<SelectListItem> GetAllDropDownList(string obj);
    }
}
