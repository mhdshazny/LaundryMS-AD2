using LaundryMS_AD2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Controllers
{
    public class PresentationController : Controller
    {
        private DbConnectionClass _context;
        public PresentationController(DbConnectionClass context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductData.ToListAsync());
        }

        public async Task<IActionResult> Checkout()
        {
            //LoggedIn User from Session
            var User = HttpContext.Session.GetString("SessionCusID");
            ViewBag.User = "USER000001";
            ViewBag.Customer = User;

            CustomerModel CustomerData = await _context.CustomerData
                                .Where(c => c.CusID == User)
                                .FirstOrDefaultAsync();

            ViewBag.CusID = CustomerData.CusID.ToString();
            ViewBag.CusName = CustomerData.CusfName.ToString();
            ViewBag.CusEmail = CustomerData.CusEmail.ToString();
            ViewBag.CusPhone = CustomerData.CusContact.ToString();
            ViewBag.CusAddress = CustomerData.CusAddress.ToString();

            //OrderList
            var WaitingOrdList = await _context.OrderListData
                             .Where(m => m.OrdListStatus == "Added To Cart" && m.OrderRefID.Contains(User))
                             .ToListAsync();

            return View(WaitingOrdList);
        }

        public IActionResult AddToCart()
        {
            OrderListModel ordlist = new OrderListModel();
            return PartialView("_AddToCartPartial", ordlist);
        }

        public async Task<IActionResult> AddOneToCart(string OrderRefID, string OrderQty, OrderListDataLibraryModel ord)
        {
            ////Change Qty
            //var ordList = await _context.OrderListData
            //    .Where(i => i.OrderRefID == OrderRefID)
            //    .FirstOrDefaultAsync();


            //ord.OrderRefID = OrderRefID;
            //ord.OrdListID = ordList.OrdListID;
            //ord.OrdListStatus = ordList.OrdListStatus;
            //ord.OrdPkg = ordList.OrdPkg;
            //ord.OrdPkgUP = ordList.OrdPkgUP;
            //ord.OrdPrQty = ordList.OrdPrQty + 1;
            //ord.OrdPrAmnt = ordList.OrdPkgUP * ord.OrdPrQty;
            //ord.OrdPrID = ordList.OrdPrID;

            //_context.Update(ord);
            //var result = _context.SaveChanges();

            //OrderList
            var User = HttpContext.Session.GetString("SessionCusID");

            var WaitingOrdList = await _context.OrderListData
                             .Where(m => m.OrdListStatus == "Added To Cart" && m.OrderRefID.Contains(User))
                             .ToListAsync();
            return View("Cart", WaitingOrdList);
        }

        public async Task<IActionResult> Cart()
        {
            //LoggedIn User from Session
            var User = HttpContext.Session.GetString("SessionCusID");
            ViewBag.User = "USER000001";
            ViewBag.Customer = User;

            //OrderList
            var WaitingOrdList = await _context.OrderListData
                             .Where(m => m.OrdListStatus == "Added To Cart" && m.OrderRefID.Contains(User))
                             .ToListAsync();

            ViewBag.WaitingOrdList = WaitingOrdList;
            return View(WaitingOrdList);
        }

        public async Task<IActionResult> CusOrders()
        {
            var orderstat = await _context.OrderData.Where(i => i.OrderStatus != "Added To Cart").ToListAsync();
            return View(orderstat);
        }
    }
}
