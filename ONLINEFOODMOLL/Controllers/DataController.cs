
using ONLINEFOODMOLL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Remoting.Messaging;
using System.ComponentModel.Design;


namespace ALL_IN_ONE_DATABASEFIRSTAPPROACH.Controllers
{
    public class DATAController : Controller
    {
        //private onlinefoodmollEntities db = new onlinefoodmollEntities();

        onlinefoodmollEntities6 db = new onlinefoodmollEntities6();

        List<Cart> li = new List<Cart>();
        // GET: DATA
        public ActionResult Index(string searchBy, string search)
        {
           
             if (searchBy == "Name")
            {
                var data = db.Photos.Where(model => model.p_name.StartsWith(search)).ToList();
                return View(data);
            }
           else {
               // var data = db.Photos.ToList();
               // return View(data);
                return View(db.Photos.ToList());
            }
        }
        public ActionResult Single(int id)
        {
            var data = db.Photos.Where(model => model.p_id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Single(int id, int qty)
        {
            Cart c = new Cart();
            Photo p = db.Photos.Where(model => model.p_id == id).FirstOrDefault();
            c.p_id = p.p_id;          
            c.p_name = p.p_name;
             c.p_price = p.p_price;          
            c.p_qty = Convert.ToInt32(qty);
            c.p_bill = c.p_price * c.p_qty;
            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;
            }
            else
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.p_id == c.p_id)
                    {
                        item.p_qty += c.p_qty;
                        item.p_bill += c.p_bill;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    li2.Add(c);
                }
                TempData["cart"] = li2;
            }
            TempData.Keep();
            return RedirectToAction("Checkout", "Data");
        }

        public ActionResult Checkout()
        {
            if (TempData["cart"] != null)
            {
                int x = 0;
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.p_bill;
                }
                TempData["total"] = x;
                TempData["item_count"] = li2.Count();
            }

            TempData.Keep();
            return View();
        }
        public ActionResult Remove(int? id)
        {
            if (TempData["cart"] == null)

            {
                TempData.Remove("total");
                TempData.Remove("cart");
            }
            else
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                Cart c = li2.Where(x => x.p_id == id).SingleOrDefault();
                li2.Remove(c);
                int s = 0;
                foreach (var item in li2)
                {
                    s += item.p_bill;
                }
                TempData["total"] = s;
            }
            return RedirectToAction("Checkout", "Data");
        }


        public ActionResult Invoice()
        {
            return View();
           
        }
        [HttpPost]
        public ActionResult Invoice(Invoice e)
        {
            if (ModelState.IsValid == true)
            {
                db.Invoices.Add(e);
                db.SaveChanges();
                ViewBag.OrderMessage = "Ordered Successfully";           
                return RedirectToAction("Bill", "Data");
            }

                return View();
        }
        public ActionResult Bill()
        {
            return View();
        }
    }
}