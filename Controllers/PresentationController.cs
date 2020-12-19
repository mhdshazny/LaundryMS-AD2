using LaundryMS_AD2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            List<PackageModel> PkgList = await _context.PackageData.ToListAsync();
            ViewBag.PkgList = new SelectList(PkgList, "PrID", "PrName"); 

            return View(await _context.ProductData.ToListAsync());
        }

        public async Task<IActionResult> Checkout()
        {
            //LoggedIn User from Session
            var User = HttpContext.Session.GetString("SessionCusID");
            ViewBag.Customer = User;

            CustomerModel CustomerData = await _context.CustomerData
                                .Where(c => c.CusID == User)
                                .FirstOrDefaultAsync();
            if (User != "")
            {
                ViewBag.CusID = CustomerData.CusID.ToString();
                ViewBag.CusName = CustomerData.CusfName.ToString();
                ViewBag.CusEmail = CustomerData.CusEmail.ToString();
                ViewBag.CusPhone = CustomerData.CusContact.ToString();
                ViewBag.CusAddress = CustomerData.CusAddress.ToString();

                //OrderList
                var WaitingOrdList = await _context.OrderListData
                                 .Where(m => m.OrdListStatus == "Added To Cart" && m.OrderRefID.Contains(User))
                                 .ToListAsync();
                if (WaitingOrdList.Count != 0)
                {
                var OneItem = WaitingOrdList.Where(i => i.OrderRefID.Contains(User)).FirstOrDefault();
                ViewBag.OrdRefID = OneItem.OrderRefID;

                }
                ViewBag.OrdNewID = NewOrdId().ToString();

                return View(WaitingOrdList);
            }
            return View();
        }
        public string NewOrdId()
        {
            string NewID = "ORD0000001";

            try
            {
                string Id = _context.OrderData
                       .Max(i => i.OrderID);
                int num;
                if (Id != null)
                {
                    num = int.Parse(Id.Substring(3, 7)) + 1;
                    NewID = "ORD" + num.ToString().PadLeft(7, '0');
                    return NewID;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return NewID;
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(string PrID, string OrdPkg,int PrQty)
        {
            //sss
            var OrderListTemp = await _context.OrderListData
                .Where(i => i.OrdPrID == PrID && i.OrdListStatus == "Added To Cart" && i.OrdPkg==OrdPkg)
                .FirstOrDefaultAsync();
            if (OrderListTemp != null)
            {
                OrderListModel ordlistOld = new OrderListModel();
                ordlistOld.OrderRefID = OrderListTemp.OrderRefID;
                ordlistOld.OrdPrID = OrderListTemp.OrdPrID;
                ordlistOld.OrdListStatus = OrderListTemp.OrdListStatus;
                ordlistOld.OrdPkg = OrderListTemp.OrdPkg;
                ordlistOld.OrdPkgUP = OrderListTemp.OrdPkgUP;

                ordlistOld.OrdPrAmnt = (OrderListTemp.OrdPrQty+PrQty)*OrderListTemp.OrdPkgUP;
                ordlistOld.OrdPrQty = OrderListTemp.OrdPrQty + PrQty;
                ordlistOld.OrdListID = OrderListTemp.OrdListID;

                _context.Entry(await _context.OrderListData.FirstOrDefaultAsync(x => x.OrdListID == ordlistOld.OrdListID)).CurrentValues.SetValues(ordlistOld);
                await _context.SaveChangesAsync();
                //_context.Attach(ordlistOld);
                //await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }

            var PkgUP = await _context.PackageData
                .Where(i => i.PkgID == OrdPkg)
                .FirstOrDefaultAsync();
            var TotAmnt = PkgUP.PkgPrice * PrQty;
            OrderListModel ordlist = new OrderListModel();
            ordlist.OrdPrID = PrID;
            ordlist.OrdPkgUP = PkgUP.PkgPrice;
            ordlist.OrdListStatus = "Added To Cart";
            ordlist.OrdPkg = OrdPkg;
            ordlist.OrdPrAmnt = TotAmnt;
            ordlist.OrdPrQty = PrQty;
            ordlist.OrderRefID = NewRefId().ToString();

             _context.Add(ordlist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public string NewRefId()
        {
            var User = HttpContext.Session.GetString("SessionCusID");
            string FinalID;
            //New Reference ID Generation or selecting suitable

            var WaitingOrdList = _context.OrderListData
                                         .Where(m => m.OrdListStatus == "Added To Cart" && m.OrderRefID.Contains(User))
                                         .FirstOrDefault();
            if (WaitingOrdList == null)
            {
                var NewWaitingOrdList = _context.OrderListData
                             .Where(m => m.OrdListStatus == "Added To Cart" && m.OrderRefID.Contains(User))
                             .FirstOrDefault();

                if (NewWaitingOrdList != null)
                {
                    var NewRefID = NewWaitingOrdList.OrderRefID.Max().ToString();

                    int num = int.Parse(NewRefID.Substring(13, 3)) + 1;
                    var NewRefId = User.ToString() + "ORD" + num.ToString().PadLeft(3, '0');
                    FinalID = NewRefId.ToString();
                    return FinalID;

                }
                else
                {
                    FinalID = User.ToString() + "ORD001";

                    return FinalID;
                }

            }
            FinalID = WaitingOrdList.OrderRefID.ToString();
            return FinalID;
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
            var User = HttpContext.Session.GetString("SessionCusID");

            var orderstat = await _context.OrderData.Where(i => i.OrderStatus != "Added To Cart"&&i.OrderRefNo.Contains(User)).ToListAsync();
            return View(orderstat);
        }

        public IActionResult ProductView(string id)
        {
            var Product = _context.ProductData
                .Where(i => i.PrID == id).FirstOrDefault();
            ViewBag.PrID = id;
            ViewBag.PrName = Product.PrName;
            ViewBag.PrStat = Product.PrStatus;
            ViewBag.PrDescr = Product.PrDescr;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(int listId, int OrdPrQty)
        {
            var id = listId;
            var OrderListTemp = await _context.OrderListData
                                       .Where(i => i.OrdListID == id && i.OrdListStatus == "Added To Cart")
                                       .FirstOrDefaultAsync();
            if (OrderListTemp != null)
            {
                OrderListModel ordlistOld = new OrderListModel();
                ordlistOld.OrderRefID = OrderListTemp.OrderRefID;
                ordlistOld.OrdPrID = OrderListTemp.OrdPrID;
                ordlistOld.OrdListStatus = OrderListTemp.OrdListStatus;
                ordlistOld.OrdPkg = OrderListTemp.OrdPkg;
                ordlistOld.OrdPkgUP = OrderListTemp.OrdPkgUP;

                ordlistOld.OrdPrAmnt = OrdPrQty * OrderListTemp.OrdPkgUP;
                ordlistOld.OrdPrQty = OrdPrQty;
                ordlistOld.OrdListID = OrderListTemp.OrdListID;

                _context.Entry(await _context.OrderListData.FirstOrDefaultAsync(x => x.OrdListID == ordlistOld.OrdListID)).CurrentValues.SetValues(ordlistOld);
                await _context.SaveChangesAsync();
                //_context.Attach(ordlistOld);
                //await _context.SaveChangesAsync();
                return RedirectToAction("Cart");

            }
            return RedirectToAction("Cart");
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string OrderID, [Bind("OrderID,OrderRefNo,OrderCusID,OrderApprvdBy,OrderTotQty,OrderTotAmnt,OrderDate,OrderDelivery,OrderDeliveryAddress,OrderDescr,OrderStatus,OrderPaymentStatus")] OrderModel orderModel)
        {
            string id = OrderID;
            if (id != orderModel.OrderID)
            {
                return NotFound();
            }


                try
                {
                orderModel.OrderStatus = "Order Placed";
                    _context.Add(orderModel);
                    await UpdateOrderList(orderModel.OrderCusID);
                    await _context.SaveChangesAsync();
                    
                    return RedirectToAction(nameof(Index));
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
                //return RedirectToAction(nameof(Index));
        }

        public async Task<bool> UpdateOrderList(string OrderCusID)
        {
            bool result = false;
            var OrderList = await _context.OrderListData.Where(i => i.OrderRefID.Contains(OrderCusID) && i.OrdListStatus == "Added To Cart").ToListAsync();
            if (OrderList.Count > 0)
            {
                foreach(var item in OrderList)
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

        private bool OrderModelExists(string id)
        {
            return _context.OrderData.Any(e => e.OrderID == id);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var orderListModel = await _context.OrderListData.FindAsync(id);
            _context.OrderListData.Remove(orderListModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Cart","Presentation");
        }
    }
}
