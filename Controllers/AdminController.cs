using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
