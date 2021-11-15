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
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepo;

        public BrandController(IBrandRepository brandRepo)
        {
            _brandRepo = brandRepo;
        }

        // GET: Brand
        public IActionResult Index()
        {
            
            IEnumerable<Brand> objList = _brandRepo.GetAll();
            return View(objList);
        }
        // GET: Brand/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand obj)
        {
            if (ModelState.IsValid)
            {
                _brandRepo.Add(obj);
                _brandRepo.Save();
                TempData[WC.Success] = "Action completed!";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error!";
            return View(obj);

        }

        // GET: Brand/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _brandRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        // POST: Brand/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Brand obj)
        {
            if (ModelState.IsValid)
            {
                _brandRepo.Update(obj);
                _brandRepo.Save();
                TempData[WC.Success] = "Action completed!";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error!";
            return View(obj);

        }

        // GET: Brand/Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _brandRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: Brand/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _brandRepo.Find(id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }
            _brandRepo.Remove(obj);
            _brandRepo.Save();
            TempData[WC.Success] = "Action completed!";
            return RedirectToAction("Index");
        }
    }
}
