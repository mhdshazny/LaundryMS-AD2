using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaundryMS_AD2.Models;

namespace LaundryMS_AD2.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbConnectionClass _context;

        public ProductController(DbConnectionClass context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductData.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.ProductData
                .FirstOrDefaultAsync(m => m.PrID == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            //Product Type List
            List<PrTypeModel> PrTypes = _context.PrTypeData.ToList();
            ViewBag.PrType = new SelectList(PrTypes, "PrTypeID", "PrType");

            //New ID
            string NewID = "PROD000001";
            var Id = await _context.ProductData
                            .MaxAsync(i => i.PrID);
            int num;
            if (Id != null)
            {
                num = int.Parse(Id.Substring(4, 6)) + 1;
                NewID = "PROD" + num.ToString().PadLeft(6, '0');
                ViewBag.NewID = NewID.ToString();

                return View();
            }

            ViewBag.NewID = NewID.ToString();

            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrID,PrName,PrType,PrDescr,PrStatus")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Product Type List
            List<PrTypeModel> PrTypes = _context.PrTypeData.ToList();
            ViewBag.PrType = new SelectList(PrTypes, "PrType", "PrType");


            var productModel = await _context.ProductData.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PrID,PrName,PrType,PrDescr,PrStatus")] ProductModel productModel)
        {
            if (id != productModel.PrID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productModel.PrID))
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
            return View(productModel);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.ProductData
                .FirstOrDefaultAsync(m => m.PrID == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var productModel = await _context.ProductData.FindAsync(id);
            _context.ProductData.Remove(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(string id)
        {
            return _context.ProductData.Any(e => e.PrID == id);
        }
    }
}
