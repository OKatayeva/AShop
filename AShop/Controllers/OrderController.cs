using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AShop_Data.Repository.IRepository;
using AShop_Models.ViewModels;
using AShop_Utility;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderHeaderRepository _orderHeaderRepo;
        private readonly IOrderDetailRepository _orderDetailRepo;

        public OrderController(IOrderHeaderRepository orderHeaderRepo,
            IOrderDetailRepository orderDetailRepo)
        {
            _orderHeaderRepo = orderHeaderRepo;
            _orderDetailRepo = orderDetailRepo;

        }

        public IActionResult Index()
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
            
            return View(orderListVM);
        }
    }
}
