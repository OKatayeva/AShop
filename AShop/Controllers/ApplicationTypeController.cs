using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AShop.Data;
using AShop_Models;
using Microsoft.AspNetCore.Authorization;
using AShop_Utility;

namespace AShop.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly AshopDB _context;

        public ApplicationTypeController(AshopDB context)
        {
            _context = context;
        }

        // GET: ApplicationType
        public IActionResult Index()
        {
            //return View(await _context.Category.ToListAsync());
            IEnumerable<ApplicationType> objList = _context.ApplicationType;
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
                _context.ApplicationType.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(obj);

        }

        // GET: ApplicationType/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _context.ApplicationType.Find(id);
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
                _context.ApplicationType.Update(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(obj);

        }

        // GET: ApplicationType/Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _context.ApplicationType.Find(id);
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
            var obj = _context.ApplicationType.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _context.ApplicationType.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
