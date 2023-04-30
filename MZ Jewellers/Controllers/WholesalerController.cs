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

        public ActionResult Orders()
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

        [HttpGet]
        public ActionResult ViewResponse()
        {
            ViewBag.QRlist = db.Quotation_Request.ToList();
            ViewBag.QRESlist = db.Quotation_Response.ToList();
            ViewBag.Prodlist = db.Products.ToList();
            Quotation_Request s = new Quotation_Request();
            string a = s.order_deadline.Date.ToString();

            return View();
        }
    }
}