using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AShop.Data;
using AShop.Models;
using AShop.Models.ViewModels;
using AShop_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly AshopDB _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public ProductUserViewModel ProductUserViewModel { get; set; }

        public CartController(AshopDB context, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
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

        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exists
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName ("Index")]
        public IActionResult IndexPost()
        {
            
            return RedirectToAction(nameof(Summary));
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //var userId = User.FindFirstValue(ClaimTypes.Name);
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exists
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> productList = _context.Product.Where(u => prodInCart.Contains(u.Id));
            ProductUserViewModel = new ProductUserViewModel()
            {
                ApplicationUser = _context.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value),
                ProductList = productList.ToList()
            };

            return View(ProductUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task <IActionResult> SummaryPost(ProductUserViewModel productUserViewModel)
        {

            var pathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                + "templates" + Path.DirectorySeparatorChar.ToString() +
                "inquiry.html";
            var subject = "New inquiry";
            var htmlBody = "";
            using (StreamReader stream = System.IO.File.OpenText(pathToTemplate))
            {
                htmlBody = stream.ReadToEnd();
            }

            StringBuilder productList = new StringBuilder();
            foreach (var product in ProductUserViewModel.ProductList)
            {
                productList.Append($" - Name: {product.Name} <span style='font-size:14px;'>(ID: {product.Id})</span><br/>");
            }

            string messageBody = string.Format(htmlBody,
                ProductUserViewModel.ApplicationUser.FullName,
                ProductUserViewModel.ApplicationUser.Email,
                ProductUserViewModel.ApplicationUser.PhoneNumber,
                productList.ToString());
            await _emailSender.SendEmailAsync(WC.EmailAdmin, subject, messageBody);
            return RedirectToAction(nameof(InquiryConfirmation));
        }

       
        public IActionResult InquiryConfirmation(ProductUserViewModel productUserViewModel)
        {

            HttpContext.Session.Clear();
            return View();
        }
    }
}
