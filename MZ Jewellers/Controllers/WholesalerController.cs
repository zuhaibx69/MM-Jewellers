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
            if (Session["JewellerId"] != null || Session["VendorId"] != null)
            {
                if (Session["JewellerId"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Vendor");
                }
            }
            return RedirectToAction("Login", "Accounts");
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Accounts");
        }

        [HttpGet]
        public ActionResult Orders()
        {
            ViewBag.Orderlist = db.PurchaseOrders.ToList();
            ViewBag.Prodlist = db.Products.ToList();
            ViewBag.Vendorlist = db.Vendors.ToList();
            ViewBag.Payment = db.Payments.ToList();
            ViewBag.Reqlist = db.Quotation_Request.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Orders(int i)
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
            ViewBag.Vendor = db.Vendors.ToList();
            ViewBag.PurchaseOrder = db.PurchaseOrders.ToList();

            Quotation_Request s = new Quotation_Request();
            string a = s.order_deadline.Date.ToString();

            return View();
        }

        [HttpGet]
        public ActionResult AddVendor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddVendor(string vname, string vemail, string vnum)
        {
            Vendor v = new Vendor
            {
                vendor_name = vname,
                vendor_email = vemail,
                vendor_contact = vnum,
                vendor_password = vemail.Split('@')[0],
                vendor_status = "Non-verified"
            };

            db.Vendors.Add(v);
            db.SaveChanges();

            return View();
        }


        [HttpPost]
        public ActionResult ViewResponse(int order_id, int vendor_id, int total_amount, string payment_type, int netprice)
        {
            PurchaseOrder q = new PurchaseOrder
            {
                order_id = order_id,
                vendor_id = vendor_id,
                total_amount = total_amount
            };
            db.PurchaseOrders.Add(q);

            db.SaveChanges();

            Payment p = new Payment
            {
                order_id = order_id,
                payment_type = payment_type,
                netprice = netprice
            };
            db.Payments.Add(p);

            db.SaveChanges();


            return RedirectToAction("ViewResponse");
        }

        [HttpGet]
        public ActionResult PendingOrder()
        {

            ViewBag.Orderlist = db.PurchaseOrders.ToList();
            ViewBag.Prodlist = db.Products.ToList();
            ViewBag.Vendorlist = db.Vendors.ToList();
            ViewBag.Payment = db.Payments.ToList();
            ViewBag.Reqlist = db.Quotation_Request.ToList();

            return View();

        }

        [HttpPost]
        public ActionResult PendingOrder(int order_id, string payment_type, int netprice)
        {
            Payment p = new Payment
            {
                order_id = order_id,
                payment_type = payment_type,
                netprice = netprice
            };
            db.Payments.Add(p);
            db.SaveChanges();


            return RedirectToAction("PendingOrder");
        }


    }
}
