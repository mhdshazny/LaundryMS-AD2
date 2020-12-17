using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaundryMS_AD2.Models;
using Microsoft.AspNetCore.Http;

namespace LaundryMS_AD2.Controllers
{
    public class OrderListController : Controller
    {
        private readonly DbConnectionClass _context;

        public OrderListController(DbConnectionClass context)
        {
            _context = context;
        }

        // GET: OrderList
        public async Task<IActionResult> Index()
        {
         

            return View(await _context.OrderListData.ToListAsync());
        }

        // GET: OrderList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderListModel = await _context.OrderListData
                .FirstOrDefaultAsync(m => m.OrdListID == id);
            if (orderListModel == null)
            {
                return NotFound();
            }

            return View(orderListModel);
        }

        // GET: OrderList/Create
        public async Task<IActionResult> Create()
        {
            var User = HttpContext.Session.GetString("SessionCusID");
            if (User == null)
            {
                RedirectToAction("Login", "CustomerLogin");
            }

            //Products List
            List<ProductModel> Products = _context.ProductData.ToList();
            ViewBag.OrdProducts = new SelectList(Products, "PrID", "PrName");
            //Package List
            List<PackageModel> Packages = _context.PackageData.ToList();
            ViewBag.OrdPkg = new SelectList(Packages, "PkgID", "PkgName");
            ViewBag.PkgList = new SelectList(Packages, "PkgID", "PkgPrice", "PkgPrice");

            //New Reference ID Generation or selecting suitable

            var WaitingOrdList = await _context.OrderListData
                                         .Where(m => m.OrdListStatus == "Added To Cart" && m.OrderRefID.Contains(User))
                                         .FirstOrDefaultAsync();


            if (WaitingOrdList == null)
            {
                var NewWaitingOrdList = await _context.OrderListData
                             .Where(m => m.OrdListStatus == "Added To Cart" && m.OrderRefID.Contains(User))
                             .FirstOrDefaultAsync();
                int num;
                if (NewWaitingOrdList != null)
                {
                    var NewRefID = NewWaitingOrdList.OrderRefID.Max().ToString();

                    num = int.Parse(NewRefID.Substring(13, 3)) + 1;
                    var NewRefId = User.ToString() + "ORD" + num.ToString().PadLeft(3, '0');
                    ViewBag.ReferenceID = NewRefId.ToString();
                    return View();

                }
                else
                {
                    ViewBag.ReferenceID = User.ToString() + "ORD001";
         
                    return View();
                }

            }
            ViewBag.ReferenceID = WaitingOrdList.OrderRefID.ToString();

            return View();

        }

        // POST: OrderList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdListID,OrderRefID,OrdPrID,OrdPrQty,OrdPrAmnt,OrdPkg,OrdPkgUP,OrdListStatus")] OrderListModel orderListModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderListModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderListModel);
        }

        // GET: OrderList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderListModel = await _context.OrderListData.FindAsync(id);
            if (orderListModel == null)
            {
                return NotFound();
            }
            return View(orderListModel);
        }

        // POST: OrderList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdListID,OrderRefID,OrdPrID,OrdPrQty,OrdPrAmnt,OrdPkg,OrdPkgUP,OrdListStatus")] OrderListModel orderListModel)
        {
            if (id != orderListModel.OrdListID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderListModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderListModelExists(orderListModel.OrdListID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderListModel);
        }

        // GET: OrderList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderListModel = await _context.OrderListData
                .FirstOrDefaultAsync(m => m.OrdListID == id);
            if (orderListModel == null)
            {
                return NotFound();
            }

            return View(orderListModel);
        }

        // POST: OrderList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderListModel = await _context.OrderListData.FindAsync(id);
            _context.OrderListData.Remove(orderListModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderListModelExists(int id)
        {
            return _context.OrderListData.Any(e => e.OrdListID == id);
        }
    }
}
