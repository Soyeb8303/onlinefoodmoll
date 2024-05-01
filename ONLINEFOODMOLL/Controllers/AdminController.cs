using ONLINEFOODMOLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace ONLINEFOODMOLL.Controllers
{
    public class AdminController : Controller
    {
        onlinefoodmollEntities6 db = new onlinefoodmollEntities6();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    
        [HttpPost]
        public ActionResult Index(Admin a)
        {      

            if (ModelState.IsValid == true)
            {
                var credentials = db.Admins.Where(model => model.name == a.name && model.password == a.password).FirstOrDefault();
                if (credentials == null)
                {
                    ViewBag.ErrorMessage = "Login Failed";                   
                }
                else
                {                
                    return RedirectToAction("IndexData", "Photo");
                }              
            }
            return View();
        }
     
    
    }

}
