using day3_DaoThanhDat.Data;
using day3_DaoThanhDat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace day3_DaoThanhDat.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            var data = from p in Data.SeedData.products
                       join c in Data.SeedData.categories on p.CategoryId equals c.Id
                       select new Models.ProductViewModel
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Description = p.Description,
                           Price = p.Price,
                           CategoryId = c.Id,
                           CategoryName = c.Name
                       };
            return View(data);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var data=SeedData.products.FirstOrDefault(p => p.Id == id);
            var category = SeedData.categories.FirstOrDefault(c => c.Id == data.CategoryId);
            var productViewModel = new Models.ProductViewModel
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                CategoryId = category.Id,
                CategoryName = category.Name
            };
            return View(productViewModel);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(SeedData.categories, "Id", "Name");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Product product)
        {
            if (ModelState.IsValid)
            {
                int maxId = SeedData.products.Any() ? SeedData.products.Max(p => p.Id) : 0;
                product.Id = maxId + 1;
                SeedData.products.Add(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new SelectList(SeedData.categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = SeedData.products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            ViewBag.CategoryId = new SelectList(SeedData.categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            var existingProduct = SeedData.products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null) return NotFound();

            if (ModelState.IsValid)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new SelectList(SeedData.categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = SeedData.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var category = SeedData.categories.FirstOrDefault(c => c.Id == product.CategoryId);
            ViewBag.CategoryName = category?.Name;
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = SeedData.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            SeedData.products.Remove(product);
            return RedirectToAction(nameof(Index));
        
        }
    }
}
