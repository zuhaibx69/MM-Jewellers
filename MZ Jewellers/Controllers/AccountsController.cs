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
        readonly JewelleryManagementSystemEntities1 db = new JewelleryManagementSystemEntities1();

        // GET: Accounts

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["JewellerId"] != null || Session["VendorId"] != null)
            {
                if(Session["JewellerId"] != null)
                {
                    return RedirectToAction("Index", "Wholesaler");
                }
                else
                {
                    return RedirectToAction("Index", "Vendor");
                }
                
            }
            else { 
                return View();
            }
        }

        [HttpPost]
        public ActionResult JewellerLogin(string uname, string pwd)
        {
            var s = db.Jewellers.Where(x => x.jeweller_name == uname && x.jeweller_password == pwd).FirstOrDefault();
            
            if (s != null)
            {
                Session["JewellerId"] = s.jeweller_id;
                Session["JewellerName"] = s.jeweller_name;
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

                Session["VendorId"] = s.vendor_id;
                Session["VendorName"] = s.vendor_name;
                Session["VendorStatus"] = s.vendor_status;
                return RedirectToAction("Index", "Vendor");
            }

            return RedirectToAction("");
        }

    }
}