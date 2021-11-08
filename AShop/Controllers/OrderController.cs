using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AShop_Data.Repository.IRepository;
using AShop_Models.ViewModels;
using AShop_Utility;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderHeaderRepository _orderHeaderRepo;
        private readonly IOrderDetailRepository _orderDetailRepo;

        //[BindProperties]
        public OrderVM OrderVM { get; set; }

        public OrderController(IOrderHeaderRepository orderHeaderRepo,
            IOrderDetailRepository orderDetailRepo)
        {
            _orderHeaderRepo = orderHeaderRepo;
            _orderDetailRepo = orderDetailRepo;

        }

        public IActionResult Index(bool? searchIsCompany, string searchName = null, string searchEmail = null, string searchPhone = null, string Status = null)
        {
            OrderListVM orderListVM = new OrderListVM()
            {
                OrderHeaderList = _orderHeaderRepo.GetAll(),
                StatusList = WC.listStatus.ToList().Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };
            if (!string.IsNullOrEmpty(searchName))
            {
                orderListVM.OrderHeaderList = orderListVM.OrderHeaderList.Where(u => u.FullName.ToLower().Contains(searchName.ToLower()));
            };

            if (!string.IsNullOrEmpty(searchEmail))
            {
                orderListVM.OrderHeaderList = orderListVM.OrderHeaderList.Where(u => u.Email.ToLower().Contains(searchEmail.ToLower()));
            };

            if (!string.IsNullOrEmpty(searchPhone))
            {
                orderListVM.OrderHeaderList = orderListVM.OrderHeaderList.Where(u => u.PhoneNumber.ToLower().Contains(searchPhone.ToLower()));
            };

            if (!string.IsNullOrEmpty(Status) && Status != "--Order Status--")
            {
                orderListVM.OrderHeaderList = orderListVM.OrderHeaderList.Where(u => u.OrderStatus.ToLower().Contains(Status.ToLower()));
            };

            if (searchIsCompany.HasValue)
            {
                orderListVM.OrderHeaderList = orderListVM.OrderHeaderList.Where(u => u.IsCompany.Equals(searchIsCompany));
            };

            

            return View(orderListVM);
        }

        public IActionResult Details(int id)
        {
            OrderVM = new OrderVM()
            {
                OrderHeader = _orderHeaderRepo.FirstOrDefault(u => u.Id == id),
                OrderDetail = _orderDetailRepo.GetAll(m => m.OrderHeaderId == id, includeProperties: "Product")
            };
            return View(OrderVM);
        }
    }
}
