using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreMVCLAB5.Models;

namespace NetCoreMVCLAB5.Controllers;

public class LXHProductController : Controller
{
    private readonly IWebHostEnvironment _environment;

    public LXHProductController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public IActionResult Index()
    {
        var products = MockData.Products.Select(p =>
        {
            p.Category = MockData.Categories.FirstOrDefault(c => c.Id == p.CategoryId);
            return p;
        }).ToList();
        return View(products);
    }

    public IActionResult Details(int id)
    {
        var product = MockData.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        product.Category = MockData.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
        return View(product);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(MockData.Categories, "Id", "Name");
        return View(new Product());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (!MockData.Categories.Any(c => c.Id == product.CategoryId))
        {
            ModelState.AddModelError("CategoryId", "Danh mục đã chọn không hợp lệ");
        }

        if (ModelState.IsValid)
        {
            if (product.ImageFile != null && product.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "products");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(product.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                product.Image = "/products/" + uniqueFileName;
            }

            product.Id = MockData.Products.Count > 0 ? MockData.Products.Max(p => p.Id) + 1 : 1;
            MockData.Products.Add(product);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Categories = new SelectList(MockData.Categories, "Id", "Name", product.CategoryId);
        return View(product);
    }

    public IActionResult Edit(int id)
    {
        var product = MockData.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        ViewBag.Categories = new SelectList(MockData.Categories, "Id", "Name", product.CategoryId);
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (!MockData.Categories.Any(c => c.Id == product.CategoryId))
        {
            ModelState.AddModelError("CategoryId", "Danh mục đã chọn không hợp lệ");
        }

        var existingProduct = MockData.Products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        if (product.ImageFile == null && string.IsNullOrEmpty(product.Image))
        {
            product.Image = existingProduct.Image;
        }

        if (ModelState.IsValid)
        {
            if (product.ImageFile != null && product.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "products");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(product.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                existingProduct.Image = "/products/" + uniqueFileName;
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.SalePrice = product.SalePrice;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Categories = new SelectList(MockData.Categories, "Id", "Name", product.CategoryId);
        return View(product);
    }

    public IActionResult Delete(int id)
    {
        var product = MockData.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        product.Category = MockData.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = MockData.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            MockData.Products.Remove(product);
        }
        return RedirectToAction(nameof(Index));
    }
}
