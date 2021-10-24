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
    [Authorize(Roles = WC.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly IApplicationTypeRepository _appTypeRepo;

        public ApplicationTypeController(IApplicationTypeRepository appTypeRepo)
        {
            _appTypeRepo = appTypeRepo;
        }

        // GET: ApplicationType
        public IActionResult Index()
        {
            //return View(await _context.Category.ToListAsync());
            IEnumerable<ApplicationType> objList = _appTypeRepo.GetAll();
            return View(objList);
        }
        // GET: ApplicationType/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: ApplicationType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _appTypeRepo.Add(obj);
                _appTypeRepo.Save();
                TempData[WC.Success] = "Action completed!";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error!";
            return View(obj);

        }

        // GET: ApplicationType/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _appTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        // POST: ApplicationType/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _appTypeRepo.Update(obj);
                _appTypeRepo.Save();
                TempData[WC.Success] = "Action completed!";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error!";
            return View(obj);

        }

        // GET: ApplicationType/Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _appTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: ApplicationType/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _appTypeRepo.Find(id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }
            _appTypeRepo.Remove(obj);
            _appTypeRepo.Save();
            TempData[WC.Success] = "Action completed!";
            return RedirectToAction("Index");
        }
    }
}
