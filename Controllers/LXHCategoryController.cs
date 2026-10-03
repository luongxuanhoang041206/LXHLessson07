using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NetCoreMVCLAB5.Models;

namespace NetCoreMVCLAB5.Controllers;

public class LXHCategoryController : Controller
{
    public IActionResult Index()
    {
        return View(MockData.Categories);
    }

    public IActionResult Create()
    {
        return View(new Category());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            category.Id = MockData.Categories.Count > 0 ? MockData.Categories.Max(c => c.Id) + 1 : 1;
            MockData.Categories.Add(category);
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }
}
