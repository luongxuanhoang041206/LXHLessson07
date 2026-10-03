using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using NetCoreMVCLAB5.Models;

namespace NetCoreMVCLAB5.Controllers;

public class LXHAccountController : Controller
{
    private static readonly List<Account> accounts = new List<Account>();

    public IActionResult Index()
    {
        return View(accounts);
    }

    public IActionResult Details(int id)
    {
        var account = accounts.FirstOrDefault(a => a.Id == id);
        if (account == null)
        {
            return NotFound();
        }
        return View(account);
    }

    public IActionResult Create()
    {
        Account model = new Account();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Account account)
    {
        if (ModelState.IsValid)
        {
            if (account.Id == 0)
            {
                account.Id = accounts.Count > 0 ? accounts.Max(a => a.Id) + 1 : 1;
            }
            accounts.Add(account);
            return RedirectToAction(nameof(Index));
        }
        return View(account);
    }

    [AcceptVerbs("GET", "POST")]
    public IActionResult VerifyPhone(string phone)
    {
        Regex _isPhone = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
        if (!_isPhone.IsMatch(phone))
        {
            return Json($"Số điện thoại {phone} không đúng định dạng, VD: 0986421127 hoặc 098.421.1127");
        }
        return Json(true);
    }
}
