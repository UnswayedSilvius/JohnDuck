using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JohnDuck.Data;
using JohnDuck.Models;

namespace JohnDuck.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Search(string searchString)
        //{
        //    var products = from p in _context.Products
        //                   select p;

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        products = products.Where(p => p.Name.Contains(searchString));
        //    }

        //    return View(await products.ToListAsync());
        //}

        // GET: /Products
        public async Task<IActionResult> Product(string searchString)
        {
            var products = from p in _context.Products
                           select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            return View(await products.ToListAsync());
        }
        // GET: /Products/Add
        public IActionResult AddProduct()
        {
            return View();
        }

        // POST: /Products/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Product));
            }
            return View(product);
        }

        // GET: /Products/Edit/5
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: /Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Product));
            }
            return View(product);
        }

        // GET: /Products/Delete/5
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: /Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedProducts(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Product));
        }

        // GET: /Products/Details/5 (for modal)
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();

            return PartialView("_ProductDetailsPartial", product); // Create this partial view
        }
    }
}
