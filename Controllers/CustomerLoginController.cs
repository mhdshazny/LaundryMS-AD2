using LaundryMS_AD2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Controllers
{
    public class CustomerLoginController : Controller
    {
        private DbConnectionClass _context;
        public CustomerLoginController(DbConnectionClass context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Verify(string CusEmail,string CusPassw)
        {
            if (CusEmail == null && CusPassw == null)
            {
                return View("Login");
            }
            if (ModelState.IsValid)
            {
                var User = _context.CustomerData
                    .Where(i => i.CusEmail == CusEmail && i.CusPassw == CusPassw)
                    .FirstOrDefault();
                if (User != null)
                {
                    HttpContext.Session.SetString("SessionCusEmail", CusEmail);
                    HttpContext.Session.SetString("SessionCusID", User.CusID);
                    return RedirectToAction("Index", "Presentation", "Login Success");
                }
                return RedirectToAction("Login", "CustomerLogin", "Username or Password Incorrect");
            }
            return RedirectToAction("Login", "CustomerLogin", "ModelState Invalid");
        }
    }
}
