﻿using MZ_Jewellers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MZ_Jewellers.Controllers
{
    public class VendorController : Controller
    {
        JewelleryManagementSystemEntities1 db = new JewelleryManagementSystemEntities1();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["JewellerId"] != null || Session["VendorId"] != null)
            {
                if (Session["VendorId"] != null)
                {
                    ViewBag.QRlist = db.Quotation_Request.ToList();
                    ViewBag.QRESlist = db.Quotation_Response.ToList();
                    ViewBag.Prodlist = db.Products.ToList();
                    Quotation_Request s = new Quotation_Request();
                    string a = s.order_deadline.Date.ToString();
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Wholesaler");
                }
            }
            return RedirectToAction("Login", "Accounts");

        }


        public ActionResult LogOut()
        {
            Session.Abandon();
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

    }
}