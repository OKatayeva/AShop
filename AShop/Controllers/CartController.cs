using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AShop.Data;
using AShop.Models;
using AShop.Utility;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AShop.Controllers
{
    public class CartController : Controller
    {
        private readonly AshopDB _context;
        public CartController(AshopDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exists
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> productList = _context.Product.Where(u => prodInCart.Contains(u.Id));
            return View(productList);
        }

    }
}
