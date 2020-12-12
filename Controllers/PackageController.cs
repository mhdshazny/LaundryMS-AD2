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
    public class PackageController : Controller
    {
        private readonly DbConnectionClass _context;

        public PackageController(DbConnectionClass context)
        {
            _context = context;
        }

        // GET: Package
        public async Task<IActionResult> Index()
        {
            return View(await _context.PackageData.ToListAsync());
        }

        // GET: Package/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packageModel = await _context.PackageData
                .FirstOrDefaultAsync(m => m.PkgID == id);
            if (packageModel == null)
            {
                return NotFound();
            }

            return View(packageModel);
        }

        // GET: Package/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Package/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkgID,PkgName,PkgDescr,PkgPrice,PkgStatus")] PackageModel packageModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(packageModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(packageModel);
        }

        // GET: Package/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packageModel = await _context.PackageData.FindAsync(id);
            if (packageModel == null)
            {
                return NotFound();
            }
            return View(packageModel);
        }

        // POST: Package/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PkgID,PkgName,PkgDescr,PkgPrice,PkgStatus")] PackageModel packageModel)
        {
            if (id != packageModel.PkgID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packageModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageModelExists(packageModel.PkgID))
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
            return View(packageModel);
        }

        // GET: Package/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packageModel = await _context.PackageData
                .FirstOrDefaultAsync(m => m.PkgID == id);
            if (packageModel == null)
            {
                return NotFound();
            }

            return View(packageModel);
        }

        // POST: Package/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var packageModel = await _context.PackageData.FindAsync(id);
            _context.PackageData.Remove(packageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageModelExists(string id)
        {
            return _context.PackageData.Any(e => e.PkgID == id);
        }
    }
}
