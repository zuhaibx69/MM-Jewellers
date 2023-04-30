using MZ_Jewellers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MZ_Jewellers.Controllers
{
    public class AccountsController : Controller
    {
        readonly JewelleryManagementSystemEntities db = new JewelleryManagementSystemEntities();

        // GET: Accounts

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult JewellerLogin(string uname, string pwd)
        {
            var s = db.Jewellers.Where(x => x.jeweller_name == uname && x.jeweller_password == pwd).FirstOrDefault();

            if (s != null)
            {
                return RedirectToAction("Index", "Wholesaler");
            }

            return RedirectToAction("");
        }

        [HttpPost]
        public ActionResult VendorLogin(string email, string pwd)
        {
            var s = db.Vendors.Where(x => x.vendor_email == email && x.vendor_password == pwd).FirstOrDefault();

            if (s != null)
            {
                return RedirectToAction("Index", "Vendor");
            }

            return RedirectToAction("");
        }

    }
}