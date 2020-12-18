using LaundryMS_AD2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Controllers
{
    public class AdminController : Controller
    {
        private DbConnectionClass _context;
        public AdminController(DbConnectionClass context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
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
            var OrdsCount = _context.OrderData.Where(i=>i.OrderStatus=="Delivered").Count().ToString();
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
            var OrdsCount = _context.OrderData.Where(i=>i.OrderStatus!="Delivered").Count().ToString();
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
            var OrdsCount = _context.OrderData.Sum(i=>i.OrderTotAmnt).ToString();
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
            var OrdsCount = _context.OrderData.Where(m=>m.OrderPaymentStatus=="Paid").Sum(i=>i.OrderTotAmnt).ToString();
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
            var OrdsCount = _context.OrderData.Where(m=>m.OrderPaymentStatus!="Paid").Sum(i=>i.OrderTotAmnt).ToString();
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
    }
}
