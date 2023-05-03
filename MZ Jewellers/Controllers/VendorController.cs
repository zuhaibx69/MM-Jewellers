using MZ_Jewellers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace MZ_Jewellers.Controllers
{
    public class VendorController : Controller
    {
        JewelleryManagementSystemEntities1 db = new JewelleryManagementSystemEntities1();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["VendorId"] != null)
            {
                return View();
            }

            else if (Session["JewellerId"] != null)
            {
                return RedirectToAction("Index", "Wholesaler");
            }

            return RedirectToAction("Login", "Accounts");

        }

        [HttpGet]
        public ActionResult QuotationRequests()
        {
            if (Session["VendorId"] != null)
            {
                if (Session["VendorStatus"].ToString().Equals("Non-verified"))
                {
                    return View("VerifyAccount");
                }

                else if (Session["VendorStatus"].ToString().Equals("Verified"))
                {
                    ViewBag.QRlist = db.Quotation_Request.ToList();
                    ViewBag.QRESlist = db.Quotation_Response.ToList();
                    ViewBag.Prodlist = db.Products.ToList();
                    ViewBag.POlist = db.PurchaseOrders.ToList();
                    Quotation_Request s = new Quotation_Request();
                    string a = s.order_deadline.Date.ToString();
                    return View();
                }
            }

            else if (Session["JewellerId"] != null)
            {
                return RedirectToAction("Index", "Wholesaler");
            }

            return RedirectToAction("Login", "Accounts");

        }


        [HttpPost]
        public ActionResult SendResponse(string id, string price_perunit)
        {

            int tid = Convert.ToInt32(id);
            int tprice_perunit = Convert.ToInt32(price_perunit);

            ViewBag.QRlist = db.Quotation_Request.ToList();
            ViewBag.QRESlist = db.Quotation_Response.ToList();

            ViewBag.Prodlist = db.Products.ToList();

            Quotation_Response qr = new Quotation_Response();
            qr.vendor_id = (int)Session["VendorId"];
            qr.price_perunit = tprice_perunit;
            qr.req_id = tid;
            db.Quotation_Response.Add(qr);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult VerifyAccount()
        {
            if (Session["VendorId"] != null)
            {
                if (Session["VendorStatus"].ToString().Equals("Non-verified"))
                {
                    return View();
                }

                else if (Session["VendorStatus"].ToString().Equals("Verified"))
                {
                    return View("VerifiedAccount");
                }

            }

            return RedirectToAction("Login", "Accounts");

        }

        [HttpPost]
        public ActionResult VerifyAccount(string licNo, HttpPostedFileBase licImg)
        {
            if (Session["VendorId"] != null)
            {
                byte[] imageData;

                using (var binaryReader = new BinaryReader(licImg.InputStream))
                {
                    imageData = binaryReader.ReadBytes(licImg.ContentLength);
                }

                Vendor v = db.Vendors.Find(Session["VendorId"]);
                v.vendor_licenseNo = licNo;
                v.vendor_licenseImg = imageData;
                Session["VendorStatus"] = v.vendor_status = "Verified";
                db.Vendors.AddOrUpdate(v);
                db.SaveChanges();
                return RedirectToAction("VerifyAccount");

            }

            return RedirectToAction("Login", "Accounts");

        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Accounts");
        }

    }
}