using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using day3_DaoThanhDat.Data;

namespace day3_DaoThanhDat.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            var _categories = SeedData.categories;
            return View(_categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            //lay ra category co id truyen vao tu seed data
            var category = SeedData.categories.FirstOrDefault(c => c.Id == id);
            //neu category khac null thi tra ve trang loi
            if (category == null)
            {
                return NotFound();
            }
            //neu category khac null thi tra ve view va truyen category vao view
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Category category)
        {
            if (ModelState.IsValid)
            {
                int maxId = SeedData.categories.Max(c => c.Id);
                category.Id = maxId + 1;
                //them category vao seed data
                SeedData.categories.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            var category = SeedData.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Category category)
        {
            var existingCategory = SeedData.categories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            var category = SeedData.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = SeedData.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            SeedData.categories.Remove(category);
            return RedirectToAction(nameof(Index));
        
        
        }
    }
}
