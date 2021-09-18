using System;
using System.Collections.Generic;
using System.Linq;
using AShop_Data.Repository.IRepository;
using AShop_Models;
using AShop_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AShop_Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AshopDB _context;
        public ProductRepository(AshopDB context): base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetAllDropDownList(string obj)
        {
            if(obj == WC.CategoryName)
            {
                return _context.Category.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

            }
            if (obj == WC.ApplicationTypeName)
            {
                return _context.ApplicationType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return null;
           
        }

        public void Update(Product obj)
        {
            _context.Product.Update(obj);

        }
    }
}
