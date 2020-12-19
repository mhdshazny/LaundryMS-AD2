using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaundryMS_AD2.Models;
using Rotativa.AspNetCore;

namespace LaundryMS_AD2.Controllers
{
    public class OrderController : Controller
    {
        private readonly DbConnectionClass _context;

        public OrderController(DbConnectionClass context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderData.ToListAsync());
        }

        public async Task<IActionResult> OrderReport(string data)
        {
            if (data != null)
            {
                if (data == "Delivered")
                {
                    var OrderDelivered = await _context.OrderData.Where(i => i.OrderStatus == data).ToListAsync();
                    return new ViewAsPdf(OrderDelivered);

                }
                var OrderPending = await _context.OrderData.Where(i => i.OrderStatus != "Delivered").ToListAsync();
                return new ViewAsPdf(OrderPending);
            }
                var OrderData = await _context.OrderData.ToListAsync();

                return new ViewAsPdf(OrderData);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderData
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // GET: Order/Create
        public async Task<IActionResult> Create()
        {
            //PreDefined Values
            //LoggedIn User from Session
            //var User = "CUS0000002";
            //ViewBag.User = "USER000001";
            //ViewBag.Customer = User;

            ////New Reference ID Generation or selecting suitable
            //var WaitingOrdList = await _context.OrderListData
            //                             .Where(m => m.OrdListStatus == "Added To Cart" && m.OrderRefID.Contains(User))
            //                             .FirstOrDefaultAsync();
            //ViewBag.ReferenceID = WaitingOrdList.OrderRefID.ToString();

            ////Tot Amnt For Payment
            //var WaitingOrdList2 = await _context.OrderListData
            //                 .Where(m => m.OrdListStatus == "Added To Cart" && m.OrderRefID.Contains(User))
            //                 .ToListAsync();
            //ViewBag.TotAmnt = WaitingOrdList2.Sum(i => i.OrdPrAmnt).ToString();

            ////Tot Quantity of Products
            //ViewBag.PrQty = WaitingOrdList2.Sum(i => i.OrdPrQty).ToString();
            //
            List<OrderListModel> OrdList = _context.OrderListData.Where(i => i.OrdListStatus == "Added To Cart").Distinct().ToList();
            ViewBag.ReferenceID = new SelectList(OrdList, "OrderRefID", "OrderRefID");

            //New OrderID for Checkout
            ViewBag.NewID = await NewId();

            return View();
        }
        public async Task<string>  NewId()
        {
            string NewID = "ORD0000001";

            try
            {
                string Id = await _context.OrderData
                       .MaxAsync(i => i.OrderID);
                int num;
                if (Id != null)
                {
                    num = int.Parse(Id.Substring(3, 7)) + 1;
                    NewID = "ORD" + num.ToString().PadLeft(7, '0');
                    ViewBag.NewID = NewID.ToString();

                    return NewID;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
                return NewID;
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,OrderRefNo,OrderCusID,OrderApprvdBy,OrderTotQty,OrderTotAmnt,OrderDate,OrderDelivery,OrderDeliveryAddress,OrderDescr,OrderStatus,OrderPaymentStatus")] OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderModel);
                await _context.SaveChangesAsync();
                await UpdateOrderList(orderModel.OrderCusID);
                return RedirectToAction(nameof(Index));
            }
            return View(orderModel);
        }
        public async Task<bool> UpdateOrderList(string OrderCusID)
        {
            bool result = false;
            var OrderList = await _context.OrderListData.Where(i => i.OrderRefID.Contains(OrderCusID) && i.OrdListStatus == "Added To Cart").ToListAsync();
            if (OrderList.Count > 0)
            {
                foreach (var item in OrderList)
                {
                    OrderListModel ordListModel = new OrderListModel();
                    ordListModel.OrderRefID = item.OrderRefID;
                    ordListModel.OrdListID = item.OrdListID;
                    ordListModel.OrdListStatus = "Order Placed";
                    ordListModel.OrdPkg = item.OrdPkg;
                    ordListModel.OrdPkgUP = item.OrdPkgUP;
                    ordListModel.OrdPrAmnt = item.OrdPrAmnt;
                    ordListModel.OrdPrID = item.OrdPrID;
                    ordListModel.OrdPrQty = item.OrdPrQty;

                    _context.Entry(await _context.OrderListData.FirstOrDefaultAsync(x => x.OrdListID == ordListModel.OrdListID)).CurrentValues.SetValues(ordListModel);
                    await _context.SaveChangesAsync();
                }
                return true;

            }
            return result;
        }
        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderData.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }
            return View(orderModel);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderID,OrderRefNo,OrderCusID,OrderApprvdBy,OrderTotQty,OrderTotAmnt,OrderDate,OrderDelivery,OrderDeliveryAddress,OrderDescr,OrderStatus,OrderPaymentStatus")] OrderModel orderModel)
        {
            if (id != orderModel.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModelExists(orderModel.OrderID))
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
            return View(orderModel);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderData
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderModel = await _context.OrderData.FindAsync(id);
            _context.OrderData.Remove(orderModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderModelExists(string id)
        {
            return _context.OrderData.Any(e => e.OrderID == id);
        }
    }
}
