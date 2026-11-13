using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using day3_DaoThanhDat.Data;

namespace day3_DaoThanhDat.Controllers
{
    public class CategoryController : Controller
    {
        //logger 
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }
        // GET: Category
        public ActionResult Index(string? searchString, int page = 1)
        {
            var _categories = SeedData.categories.AsEnumerable();

            // 1. Lọc theo từ khóa tìm kiếm (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                string keyword = searchString.Trim().ToLower();
                _categories = _categories.Where(c =>
                    (c.Name != null && c.Name.ToLower().Contains(keyword)) ||
                    (c.Description != null && c.Description.ToLower().Contains(keyword))
                );
            }

            // 2. Phân trang đơn giản: Trang 1 lấy 1, Trang 2 lấy 2
            List<Models.Category> pagedData;
            if (page == 1)
            {
                pagedData = _categories.Take(1).ToList();
            }
            else
            {
                page = 2;
                pagedData = _categories.Skip(1).Take(2).ToList();
            }

            ViewBag.SearchString = searchString;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = _categories.Count() > 1 ? 2 : 1;

            return View(pagedData);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            _logger.LogInformation("Details action called with id: {Id}", id);
            _logger.LogError("This is a sample error log for id: {Id}", id);
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
