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
    public class PrTypeController : Controller
    {
        private readonly DbConnectionClass _context;

        public PrTypeController(DbConnectionClass context)
        {
            _context = context;
        }

        // GET: PrType
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrTypeData.ToListAsync());
        }

        // GET: PrType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prTypeModel = await _context.PrTypeData
                .FirstOrDefaultAsync(m => m.PrTypeID == id);
            if (prTypeModel == null)
            {
                return NotFound();
            }

            return View(prTypeModel);
        }

        // GET: PrType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrTypeID,PrType,PrTypeDescr,PrTypeStatus")] PrTypeModel prTypeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prTypeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prTypeModel);
        }

        // GET: PrType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prTypeModel = await _context.PrTypeData.FindAsync(id);
            if (prTypeModel == null)
            {
                return NotFound();
            }
            return View(prTypeModel);
        }

        // POST: PrType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrTypeID,PrType,PrTypeDescr,PrTypeStatus")] PrTypeModel prTypeModel)
        {
            if (id != prTypeModel.PrTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prTypeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrTypeModelExists(prTypeModel.PrTypeID))
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
            return View(prTypeModel);
        }

        // GET: PrType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prTypeModel = await _context.PrTypeData
                .FirstOrDefaultAsync(m => m.PrTypeID == id);
            if (prTypeModel == null)
            {
                return NotFound();
            }

            return View(prTypeModel);
        }

        // POST: PrType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prTypeModel = await _context.PrTypeData.FindAsync(id);
            _context.PrTypeData.Remove(prTypeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrTypeModelExists(int id)
        {
            return _context.PrTypeData.Any(e => e.PrTypeID == id);
        }
    }
}
