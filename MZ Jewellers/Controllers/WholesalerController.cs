using MZ_Jewellers.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MZ_Jewellers.Controllers
{
    public class WholesalerController : Controller
    {
        readonly JewelleryManagementSystemEntities db = new JewelleryManagementSystemEntities();
        // GET: Wholesaler
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult JewellerLogin(string uname, string pwd)
        {
            var s = db.Jewellers.Where(x=> x.jeweller_name == uname && x.jeweller_password == pwd).FirstOrDefault();

            if(s != null)
            {
                return RedirectToAction("Index");
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

        public ActionResult RFQ()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateRFQ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRFQ(int qty, DateTime Deadlinedate) 
        {
            Quotation_Request qr = new Quotation_Request
            {
                prd_id = 1,
                prd_quantity = qty,
                order_deadline = Deadlinedate.Date
            };
            
            db.Quotation_Request.Add(qr);
            db.SaveChanges();

            return View();
        }

    }
}