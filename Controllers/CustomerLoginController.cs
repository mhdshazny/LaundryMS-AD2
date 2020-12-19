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
    public class CustomerLoginController : Controller
    {
        private DbConnectionClass _context;
        public CustomerLoginController(DbConnectionClass context)
        {
            _context = context;
        }

        public async Task<IActionResult> CustomerInfo(string id)
        {
            if (id == null)
            {

                return RedirectToAction("Index","Presentation");
            }
            return View(await _context.CustomerData.Where(i=>i.CusID==id).FirstOrDefaultAsync());
        }

        public async Task<IActionResult> Login()
        {
            HttpContext.Session.SetString("SessionCusEmail", "");
            HttpContext.Session.SetString("SessionCusID", "");
            //New ID
            var Id = await _context.CustomerData
                            .MaxAsync(i => i.CusID);
            string NewID = "CUS0000001";
            int num;
            if (Id != null)
            {
                num = int.Parse(Id.Substring(3, 7)) + 1;
                NewID = "CUS" + num.ToString().PadLeft(7, '0');
                ViewBag.NewID = NewID.ToString();

                return View();
            }
            ViewBag.NewID = NewID.ToString();

            return View();
        }
        [HttpPost]
        public IActionResult Verify(string CusEmail,string CusPassw)
        {
            if (CusEmail == null && CusPassw == null)
            {
                return View("Login");
            }
            if (ModelState.IsValid)
            {
                var User = _context.CustomerData
                    .Where(i => i.CusEmail == CusEmail && i.CusPassw == CusPassw)
                    .FirstOrDefault();
                if (User != null)
                {
                    HttpContext.Session.SetString("SessionCusEmail", CusEmail);
                    HttpContext.Session.SetString("SessionCusID", User.CusID);
                    return RedirectToAction("Index", "Presentation", "Login Success");
                }
                return RedirectToAction("Login", "CustomerLogin", "Username or Password Incorrect");
            }
            return RedirectToAction("Login", "CustomerLogin", "ModelState Invalid");
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CusID,CusfName,CuslName,CusNIC,CusGender,CusAddress,CusContact,CusEmail,CusPassw,CusStatus")] CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login","CustomerLogin");
            }
            return RedirectToAction("Login", "CustomerLogin",customerModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string CusID, [Bind("CusID,CusfName,CuslName,CusNIC,CusGender,CusAddress,CusContact,CusEmail,CusPassw,CusStatus")] CustomerModel customerModel)
        {
            if (CusID != customerModel.CusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerModelExists(customerModel.CusID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Presentation");
            }
            return RedirectToAction("CustomerInfo", "CustomerLogin",customerModel);

        }
        private bool CustomerModelExists(string CusID)
        {
            return _context.CustomerData.Any(e => e.CusID == CusID);
        }
    }
}
