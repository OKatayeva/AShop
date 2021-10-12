using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AShop_Data;
using AShop_Models;
using Microsoft.AspNetCore.Authorization;
using AShop_Utility;
using AShop_Data.Repository.IRepository;
using AShop_Models.ViewModels;

namespace AShop.Controllers
{
    [Authorize(WC.AdminRole)]
    public class InquiryController : Controller
    {
        private readonly IInquiryHeaderRepository _inqHeaderRepo;
        private readonly IInquiryDetailsRepository _inqDetailsRepo;
        [BindProperty]
        public InquiryVM InquiryVM { get; set; }
        public InquiryController(IInquiryHeaderRepository inqHeaderRepo, IInquiryDetailsRepository inqDetailsRepo)
        {
            _inqHeaderRepo = inqHeaderRepo;
            _inqDetailsRepo = inqDetailsRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            InquiryVM = new InquiryVM()
            {
                InquiryHeader = _inqHeaderRepo.FirstOrDefault(u => u.Id == id),
                InquiryDetails = _inqDetailsRepo.GetAll(u => u.InquiryHeader.Id == id, includeProperties:"Product")
            };
            return View(InquiryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            InquiryVM.InquiryDetails = _inqDetailsRepo.GetAll(u => u.InquiryHeaderId == InquiryVM.InquiryHeader.Id);
            foreach (var detail in InquiryVM.InquiryDetails)
            {
                ShoppingCart shoppingCart = new ShoppingCart()
                {
                    ProductId = detail.ProductId
                };
                shoppingCartList.Add(shoppingCart);

            }
            HttpContext.Session.Clear();
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            HttpContext.Session.Set(WC.SessionInquiryId, InquiryVM.InquiryHeader.Id);
            return RedirectToAction("Index", "Cart");
        }
        [HttpPost]
        public IActionResult Delete()
        {
            InquiryHeader inquiryHeader = _inqHeaderRepo.FirstOrDefault(u => u.Id == InquiryVM.InquiryHeader.Id);
            IEnumerable<InquiryDetails> inquiryDetails = _inqDetailsRepo.GetAll(u => u.InquiryHeaderId == InquiryVM.InquiryHeader.Id);

            _inqDetailsRepo.RemoveRange(inquiryDetails);
            _inqHeaderRepo.Remove(inquiryHeader);
            _inqHeaderRepo.Save();

            return RedirectToAction(nameof(Index));
        }
        #region API calls
        [HttpGet]
        public IActionResult GetInquiryList()
        {
            return Json(new { data = _inqHeaderRepo.GetAll() });
        }
        #endregion 

    }
}
