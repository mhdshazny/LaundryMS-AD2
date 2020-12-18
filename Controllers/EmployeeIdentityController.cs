using LaundryMS_AD2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Controllers
{
    public class EmployeeIdentityController : Controller
    {
        private readonly DbConnectionClass _context;
        public EmployeeIdentityController(DbConnectionClass context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginOut()
        {
            HttpContext.Session.SetString("SessionEmpEmail", "");
            HttpContext.Session.SetString("SessionEmpID", "");
            HttpContext.Session.SetString("SessionEmpRole", "");
            return RedirectToAction("Login","EmployeeIdentity","SessionCleared");
        }

        [HttpPost]
        public IActionResult Verify(string email, string password)
        {
            if (email == null && password == null)
            {
                return RedirectToAction("Login", "EmployeeIdentity", "Null Values");
            }
            if (ModelState.IsValid)
            {
                var User = _context.EmpIdentityData
                    .Where(i => i.Email == email && i.Password == password)
                    .FirstOrDefault();
                if (User != null)
                {
                    HttpContext.Session.SetString("SessionEmpEmail", email);
                    HttpContext.Session.SetString("SessionEmpID", User.UserID);
                    HttpContext.Session.SetString("SessionEmpRole", User.Role);
                    return RedirectToAction("Index", "Home", "Success");
                }
                return RedirectToAction("Login", "EmployeeIdentity", "Username Password Incorrect");
            }
            return RedirectToAction("Login", "EmployeeIdentity", "ModelState Invalid");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
