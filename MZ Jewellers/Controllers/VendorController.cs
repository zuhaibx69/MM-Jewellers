using MZ_Jewellers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MZ_Jewellers.Controllers
{
    public class VendorController : Controller
    {
        JewelleryManagementSystemEntities db = new JewelleryManagementSystemEntities();

        // GET: Vendor

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.QRlist = db.Quotation_Request.ToList();
            ViewBag.Prodlist = db.Products.ToList();
            Quotation_Request s = new Quotation_Request();
            string a = s.order_deadline.Date.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Index(int price_perunit)
        {
            ViewBag.QRlist = db.Quotation_Request.ToList();
            ViewBag.Prodlist = db.Products.ToList();

            Quotation_Response qr = new Quotation_Response();
            qr.vendor_id = 1;
            qr.price_perunit = price_perunit;
            qr.req_id = 1;

            return View();
        }

    }
}