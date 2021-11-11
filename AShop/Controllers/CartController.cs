using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AShop_Data;
using AShop_Data.Repository.IRepository;
using AShop_Models;
using AShop_Models.ViewModels;
using AShop_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;



namespace AShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        private readonly IApplicationUserRepository _userRepo;
        private readonly IInquiryHeaderRepository _inqHeaderRepo;
        private readonly IInquiryDetailsRepository _inqDetailsRepo;

        private readonly IOrderHeaderRepository _orderHeaderRepo;
        private readonly IOrderDetailRepository _orderDetailRepo;
        private readonly IProductRepository _prodRepo;
        [BindProperty]
        public ProductUserViewModel ProductUserViewModel { get; set; }

        public CartController(IWebHostEnvironment webHostEnvironment, IEmailSender emailSender,
            IApplicationUserRepository userRepo, IInquiryHeaderRepository inqHeaderRepo,
            IInquiryDetailsRepository inqDetailsRepo, IProductRepository prodRepo,
            IOrderHeaderRepository orderHeaderRepo,
            IOrderDetailRepository orderDetailRepo)
        {
            
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _userRepo = userRepo;
            _inqHeaderRepo = inqHeaderRepo;
            _inqDetailsRepo = inqDetailsRepo;

            _orderHeaderRepo = orderHeaderRepo;
            _orderDetailRepo = orderDetailRepo;
            _prodRepo = prodRepo;
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
            
            IEnumerable<Product> productListTemp = _prodRepo.GetAll(u => prodInCart.Contains(u.Id));
            IList<Product> prodList = new List<Product>();
            foreach (var cartObj in shoppingCartList)
            {
                Product prodTemp = productListTemp.FirstOrDefault(u => u.Id == cartObj.ProductId);
                prodTemp.ProductQuantity = cartObj.ProdQuantity;
                prodList.Add(prodTemp);
            }
            return View(prodList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost(IEnumerable<Product> ProdList)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            foreach (Product prod in ProdList)
            {
                shoppingCartList.Add(new ShoppingCart { ProductId = prod.Id, ProdQuantity = prod.ProductQuantity });
            }

            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Summary));
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

        

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //ApplicationUser applicationUser;
            //applicationUser = _userRepo.FirstOrDefault(u => u.Id == claim.Value);
            //var userId = User.FindFirstValue(ClaimTypes.Name);
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                //session exists
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> productList = _prodRepo.GetAll(u => prodInCart.Contains(u.Id));
            ProductUserViewModel = new ProductUserViewModel()
            {
                ApplicationUser = _userRepo.FirstOrDefault(u => u.Id == claim.Value),
                //ProductList = productList.ToList()
            };
            foreach (var cartObj in shoppingCartList)
            {
                Product prodTemp = _prodRepo.FirstOrDefault(u => u.Id == cartObj.ProductId);
                prodTemp.ProductQuantity = cartObj.ProdQuantity;
                ProductUserViewModel.ProductList.Add(prodTemp);
            }
            return View(ProductUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task <IActionResult> SummaryPost(ProductUserViewModel productUserViewModel)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var orderTotal = 0.0;
            foreach(Product product in ProductUserViewModel.ProductList)
            {
                orderTotal += product.Price;
            }
            OrderHeader orderHeader = new OrderHeader()
            {
                CreatedByUserId = claim.Value,
                FinalOrderTotal = orderTotal,
                City = ProductUserViewModel.ApplicationUser.City,
                State = ProductUserViewModel.ApplicationUser.State,
                StreetAddress = ProductUserViewModel.ApplicationUser.StreetAddress,
                PostalCode = ProductUserViewModel.ApplicationUser.PostalCode,
                Email = ProductUserViewModel.ApplicationUser.Email,
                FullName = ProductUserViewModel.ApplicationUser.FullName,
                PhoneNumber= ProductUserViewModel.ApplicationUser.PhoneNumber,
                IsCompany = ProductUserViewModel.ApplicationUser.IsCompany,
                VATNumber = ProductUserViewModel.ApplicationUser.VATNumber,
                OrderDate = DateTime.Now,
                OrderStatus = WC.StatusPending,

            };

            _orderHeaderRepo.Add(orderHeader);
            _orderHeaderRepo.Save();

            foreach (var product in ProductUserViewModel.ProductList)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderHeaderId = orderHeader.Id,
                    Price = product.Price,
                    ProductId = product.Id,
                    Quantity = product.ProductQuantity,
                    
                };
                _orderDetailRepo.Add(orderDetail);
            }
            _orderDetailRepo.Save();
            TempData[WC.Success] = "Action completed!";
            return RedirectToAction(nameof(InquiryConfirmation), new { id = orderHeader.Id });
            //var pathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
            //    + "templates" + Path.DirectorySeparatorChar.ToString() +
            //    "inquiry.html";
            //var subject = "New inquiry";
            //var htmlBody = "";
            //using (StreamReader stream = System.IO.File.OpenText(pathToTemplate))
            //{
            //    htmlBody = stream.ReadToEnd();
            //}

            //StringBuilder productList = new StringBuilder();
            //foreach (var product in ProductUserViewModel.ProductList)
            //{
            //    productList.Append($" - Name: {product.Name} <span style='font-size:14px;'>(ID: {product.Id})</span><br/>");
            //}

            //string messageBody = string.Format(htmlBody,
            //    ProductUserViewModel.ApplicationUser.FullName,
            //    ProductUserViewModel.ApplicationUser.Email,
            //    ProductUserViewModel.ApplicationUser.PhoneNumber,
            //    productList.ToString());
            //await _emailSender.SendEmailAsync(WC.EmailAdmin, subject, messageBody);
            //InquiryHeader inquiryHeader = new InquiryHeader()
            //{
            //    ApplicationUserId = claim.Value,
            //    FullName = ProductUserViewModel.ApplicationUser.FullName,
            //    PhoneNumber = ProductUserViewModel.ApplicationUser.PhoneNumber,
            //    Email = ProductUserViewModel.ApplicationUser.Email,
            //    InquiryDate = DateTime.Now
            //};

            //_inqHeaderRepo.Add(inquiryHeader);
            //_inqHeaderRepo.Save();

            //foreach (var product in ProductUserViewModel.ProductList)
            //{
            //    InquiryDetails inquiryDetails = new InquiryDetails()
            //    {
            //        InquiryHeaderId = inquiryHeader.Id,
            //        ProductId = product.Id
            //    };
            //    _inqDetailsRepo.Add(inquiryDetails);
            //}
            //_inqDetailsRepo.Save();
            //TempData[WC.Success] = "Action completed!";
            //return RedirectToAction(nameof(InquiryConfirmation));
        }

     

        public IActionResult InquiryConfirmation(ProductUserViewModel productUserViewModel)
        {

            HttpContext.Session.Clear();
            return View();
        }
    }
}
