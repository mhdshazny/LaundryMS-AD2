using LaundryMS_AD2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbConnectionClass _context;

        //public HomeController(DbConnectionClass context)
        //{
        //    _context = context;
        //}
        public HomeController(ILogger<HomeController> logger, DbConnectionClass context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Verify(string email, string password)
        {
            if(email==null && password == null)
            {
                return RedirectToAction("Privacy", "Home", "Null Values");
            }
            if (ModelState.IsValid)
            {
                var User= _context.EmpIdentityData
                    .Where(i => i.Email == email && i.Password == password)
                    .FirstOrDefault();
                if (User != null)
                {
                return RedirectToAction("Privacy", "Home", "Success");
                }
                return RedirectToAction("Privacy", "Home", "Username Password Incorrect");
            }
            return RedirectToAction("Privacy","Home","ModelState Invalid");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
