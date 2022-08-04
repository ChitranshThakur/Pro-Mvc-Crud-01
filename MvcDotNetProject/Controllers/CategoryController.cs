using Microsoft.AspNetCore.Mvc;
using MvcDotNetProject.Models;

namespace MvcDotNetProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //get
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Create(Category category)
        {   
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("displayorder", "both the fields Can not be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            { 
              return NotFound();
            }
            var categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("displayorder", "both the fields Can not be same");
            }
            if (ModelState.IsValid)
            {
                _db.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("displayorder", "both the fields Can not be same");
            }
            if (ModelState.IsValid)
            {
                _db.Remove(category);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(category);
        }


    }
}
