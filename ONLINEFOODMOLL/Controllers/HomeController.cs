using ONLINEFOODMOLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ClientServices.Providers;
using System.Web.Mvc;

namespace ONLINEFOODMOLL.Controllers
{
    public class HomeController : Controller
    {
        onlinefoodmollEntities2 db = new onlinefoodmollEntities2();
        // GET: Home


        public ActionResult Index()
        {

            return View();
        }
      
        [HttpPost]
        public ActionResult Index(Employee s)
        {
            if (ModelState.IsValid == true)
            {
                var credentials = db.Employees.Where(model => model.name == s.name && model.password == s.password).FirstOrDefault();
                if (credentials == null)
                {
                    ViewBag.ErrorMessage = "Name and Password mot matching";
                }
                else
                {
                    return RedirectToAction("Index", "Data");
                }
            }
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }


     
        [HttpPost]
        public ActionResult Create(Employee s)
        {


            var data = db.Employees.Where(model => model.email == s.email).FirstOrDefault();
            if (data != null)
            {
                ViewBag.ErrorMessage1 = "Email is alredy exist ";
            }
            else
            {
                db.Employees.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
        
    }
}