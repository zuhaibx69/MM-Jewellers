using MZ_Jewellers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

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
            ViewBag.Jewellerlist = db.Jewellers.ToList();

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
            if (Session["JewellerId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Accounts");

        }

        [HttpPost]
        public ActionResult CreateRFQ(int qty, int unit, DateTime Deadlinedate)
        {
            Quotation_Request qr = new Quotation_Request
            {
                prd_id = unit,
                jeweller_id = Session["JewellerId"].ToString(),
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
            ViewBag.Vendor = db.Vendors.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddVendor(string vname, string vemail, string vnum,string vcnic,string vaddr,int vtax,string vtitle,string vbank,string vacnum)
        {
            bool isExisting = false;
            string message = "";

            if (db.Vendors.Any(v => v.vendor_email == vemail))
            {
                isExisting = true;
                message = "Email";
            }
            else if (db.Vendors.Any(v => v.vendor_contact == vnum))
            {
                isExisting = true;
                message = "Contact";
            }
            else if (db.Vendors.Any(v => v.vendor_cnic == vcnic))
            {
                isExisting = true;
                message = "CNIC";
            }
            else if (db.Vendors.Any(v => v.vendor_acc_no == vacnum))
            {
                isExisting = true;
                message = "Account Number";
            }

            if (isExisting)
            {
                TempData["AlertMessage"] = message + " already exists in the database. Please change it.";
                return RedirectToAction("AddVendor");
            }
            else
            {
                // Data is new, add the vendor to the database
                Vendor v = new Vendor
                {
                    vendor_name = vname,
                    vendor_email = vemail,
                    vendor_contact = vnum,
                    vendor_cnic = vcnic,
                    vendor_address = vaddr,
                    vendor_tax = vtax,
                    vendor_acc_title = vtitle,
                    vendor_bank_name = vbank,
                    vendor_acc_no = vacnum,
                    vendor_password = vemail.Split('@')[0],
                    vendor_status = "Non-verified"
                };
                TempData["AlertMessage"] = "Vendor Registered Successfully..";
                db.Vendors.Add(v);
                db.SaveChanges();
                return RedirectToAction("AddVendor");
            }
            
        }

        [HttpGet]
        public ActionResult VerifyVendor()
        {
            ViewBag.Vendor = db.Vendors.ToList();
            return View();
        }

        public ActionResult VerifyVendorPost(int id)
        {
            Vendor v = db.Vendors.Find(id);
            Session["VendorStatus"] = v.vendor_status = "Verified";
            db.Vendors.AddOrUpdate(v);
            db.SaveChanges();
            return RedirectToAction("VerifyVendor");
        }

        [HttpGet]
        public ActionResult Vendors()
        {
            ViewBag.Vendor = db.Vendors.ToList();
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
        public ActionResult PendingOrder(string vendor_name, int order_id, string payment_type, int netprice, int qty, string unit)
        {
            Payment p = new Payment
            {
                order_id = order_id,
                payment_type = payment_type,
                netprice = netprice
            };
            db.Payments.Add(p);
            db.SaveChanges();

            Inventory i = new Inventory
            {
                order_id = order_id,
                prd_name = "Gold",
                prd_description = "Raw Material",
                prd_quantity = qty,
                prd_unit = unit,
                total_amount = netprice + (netprice / 4)
            };

            db.Inventories.Add(i);
            db.SaveChanges();

            GRN g = new GRN
            {
                order_id = order_id,
                vendor_name = vendor_name,
                prd_name = "Gold",
                prd_quantity = qty,
                prd_unit = unit,
                total_amount = netprice + (netprice / 4),
                receiving_date = DateTime.Now.Date
            };

            db.GRNs.Add(g);
            db.SaveChanges();

            return RedirectToAction("PendingOrder");
        }

        [HttpGet]
        public ActionResult Inventory()
        {
            ViewBag.Inventory = db.Inventories.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult GRN()
        {
            ViewBag.GRN = db.GRNs.ToList();
            return View();
        }

        public ActionResult GetVendorImage(int vendorId)
        {
            
                var vendor = db.Vendors.FirstOrDefault(v => v.vendor_id == vendorId);
                if (vendor != null)
                {
                    return File(vendor.vendor_licenseImg, "image/jpeg");
                }
                else
                {
                    return HttpNotFound();
                }
            }
        


    }
}
