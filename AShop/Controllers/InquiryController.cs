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

namespace AShop.Controllers
{

    public class InquiryController : Controller
    {
        private readonly IInquiryHeaderRepository _inqHeaderRepo;
        private readonly IInquiryDetailsRepository _inqDetailRepo;

        public InquiryController(IInquiryHeaderRepository inqHeaderRepo, IInquiryDetailsRepository inqDetailsRepo)
        {
            _inqHeaderRepo = inqHeaderRepo;
            _inqDetailRepo = inqDetailsRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
