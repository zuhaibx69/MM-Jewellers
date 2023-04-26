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
        JewelleryManagementSystemEntities db = new JewelleryManagementSystemEntities();
        // GET: Wholesaler
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            dynamic model = new ExpandoObject();
            Jeweller j = new Jeweller();
            Vendor v = new Vendor();
            model.Jeweller = j;
            model.Vendor = v;

            return View();
        }

        [HttpPost]
        public ActionResult JewellerLogin(Jeweller jewelers)
        {

            var s = db.Jewellers.Where(x=> x.jeweller_name == jewelers.jeweller_name && x.jeweller_password == jewelers.jeweller_password).FirstOrDefault();

            if(s != null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("");
        }

        [HttpPost]
        public ActionResult VendorLogin(Jeweller jewelers)
        {

            var s = db.Jewellers.Where(x => x.jeweller_name == jewelers.jeweller_name && x.jeweller_password == jewelers.jeweller_password).FirstOrDefault();

            if (s != null)
            {
                return RedirectToAction("Index");
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
            Quotation_Request qr = new Quotation_Request();
            qr.prd_id = 1;
            qr.prd_quantity = qty;
            qr.order_deadline = Deadlinedate.Date;
            var s = db.Quotation_Request.Add(qr);
            db.SaveChanges();

            return View();
        }

    }
}