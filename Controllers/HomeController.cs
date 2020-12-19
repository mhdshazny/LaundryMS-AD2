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
            ViewBag.TotOrds = TotOrds().ToString();
            ViewBag.TotOrdDel = TotOrdDel().ToString();
            ViewBag.TotOrdPen = TotOrdPen().ToString();
            ViewBag.TotIncome = TotIncome().ToString();
            ViewBag.TotSales = TotSales().ToString();
            ViewBag.TotCredits = TotCredits().ToString();
            ViewBag.TotCust = TotCust().ToString();
            return View();
        }

        public int TotOrds()
        {
            int TotOrds = 0;
            var OrdsCount = _context.OrderData.Count().ToString();
            if (OrdsCount != null)
            {
                TotOrds = int.Parse(OrdsCount);
                return TotOrds;
            }

            return TotOrds;
        }
        public int TotOrdDel()
        {
            int TotOrdDel = 0;
            var OrdsCount = _context.OrderData.Where(i => i.OrderStatus == "Delivered").Count().ToString();
            if (OrdsCount != null)
            {
                TotOrdDel = int.Parse(OrdsCount);
                return TotOrdDel;
            }

            return TotOrdDel;
        }
        public int TotOrdPen()
        {
            int TotOrdPen = 0;
            var OrdsCount = _context.OrderData.Where(i => i.OrderStatus != "Delivered").Count().ToString();
            if (OrdsCount != null)
            {
                TotOrdPen = int.Parse(OrdsCount);
                return TotOrdPen;
            }

            return TotOrdPen;
        }
        public decimal TotIncome()
        {
            decimal TotIncome = 0;
            var OrdsCount = _context.OrderData.Sum(i => i.OrderTotAmnt).ToString();
            if (OrdsCount != null)
            {
                TotIncome = decimal.Parse(OrdsCount);
                return TotIncome;
            }

            return TotIncome;
        }
        public decimal TotSales()
        {
            decimal TotSales = 0;
            var OrdsCount = _context.OrderData.Where(m => m.OrderPaymentStatus == "Paid").Sum(i => i.OrderTotAmnt).ToString();
            if (OrdsCount != null)
            {
                TotSales = decimal.Parse(OrdsCount);
                return TotSales;
            }

            return TotSales;
        }
        public decimal TotCredits()
        {
            decimal TotCredits = 0;
            var OrdsCount = _context.OrderData.Where(m => m.OrderPaymentStatus != "Paid").Sum(i => i.OrderTotAmnt).ToString();
            if (OrdsCount != null)
            {
                TotCredits = decimal.Parse(OrdsCount);
                return TotCredits;
            }

            return TotCredits;
        }
        public int TotCust()
        {
            int TotCust = 0;
            var OrdsCount = _context.CustomerData.Count().ToString();
            if (OrdsCount != null)
            {
                TotCust = int.Parse(OrdsCount);
                return TotCust;
            }

            return TotCust;
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
