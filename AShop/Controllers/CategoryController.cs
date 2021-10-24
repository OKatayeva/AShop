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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _catRepo;

        public CategoryController(ICategoryRepository catRepo)
        {
            _catRepo = catRepo;
        }

        // GET: Category
        public IActionResult Index()
        {
            //return View(await _context.Category.ToListAsync());
            IEnumerable<Category> objList = _catRepo.GetAll();
            return View(objList);
        }
        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _catRepo.Add(obj);
                _catRepo.Save();
                TempData[WC.Success] = "Action completed!";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error!";
                return View(obj);
            
        }

        // GET: Category/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
                return NotFound();
            var obj = _catRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        // POST: Category/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _catRepo.Update(obj);
                _catRepo.Save();
                TempData[WC.Success] = "Action completed!";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error!";
            return View(obj);

        }

        // GET: Category/Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _catRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: Category/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _catRepo.Find(id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }
            _catRepo.Remove(obj);
            _catRepo.Save();
            TempData[WC.Success] = "Action completed!";
            return RedirectToAction("Index");
           

        }

    }
}
