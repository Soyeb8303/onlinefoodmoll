using ONLINEFOODMOLL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.UI;
using System.Web.Optimization;



namespace ONLINEFOODMOLL.Controllers
{
    public class PhotoController : Controller
    {
        // GET: Photo
        onlinefoodmollEntities6 db = new onlinefoodmollEntities6();

       
        // GET: photos
        //public ActionResult Index(string searchBy, string search)
        //{

            // if (searchBy == "Name")
            //{
            //    var data = db.Photos.Where(model => model.p_name.StartsWith(search)).ToList();
            //    return View(data);
            //}
            //else

        //    var data = db.Photos.ToList();
        //    {              
        //        return View(data);
        //    }
        //}
        

        public ActionResult IndexData(string searchBy, string search)
        {
            if (searchBy == "Name")
            {
                var data = db.Photos.Where(model => model.p_name.StartsWith(search)).ToList();
                return View(data);
            }
            else
            { 
                var data = db.Photos.ToList();
                return View(data);
            }
            
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Photo s)
        {
            if (ModelState.IsValid == true)
            {
                string fileName = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
                string extension = Path.GetExtension(s.ImageFile.FileName);
                HttpPostedFileBase postedFile = s.ImageFile;
                int length = postedFile.ContentLength;
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        fileName = fileName + extension;
                        s.image_path = "~/images/" + fileName;
                        //s.image_path = "~/images";
                        fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                        s.ImageFile.SaveAs(fileName);

                        db.Photos.Add(s);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {                  
                          TempData["CreateMessage"] = "<script>alert('Data Inserted Successful')</script>";
                            ModelState.Clear();
                            return RedirectToAction("IndexData", "Photo");
                        }
                        else
                        {
                            TempData["CreateMessage"] = "<script>alert('Data Not Inserted')</script>";
                        }
                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Image Size shuld be less than 2 MB')</script>";
                    }
                }
                else
                {
                    TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var StudentRow = db.Photos.Where(model => model.p_id == id).FirstOrDefault();
            Session["Image"] = StudentRow.image_path;
            return View(StudentRow);
        }

        [HttpPost]
        public ActionResult Edit(Photo s)
        {
            if (ModelState.IsValid == true)
            {
                if (s.ImageFile != null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
                    string extension = Path.GetExtension(s.ImageFile.FileName);
                    HttpPostedFileBase postedFile = s.ImageFile;
                    int length = postedFile.ContentLength;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extension;
                            s.image_path = "~/images/" + fileName;
                            //s.image_path = "~/images";
                            fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                            s.ImageFile.SaveAs(fileName);

                            // db.students.Add(s);
                            db.Entry(s).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                string ImagePath = Request.MapPath(Session["Image"].ToString());
                                if (System.IO.File.Exists(ImagePath))
                                {
                                    System.IO.File.Delete(ImagePath);
                                }
                                TempData["UpdateMessage"] = "<script>alert('Data Updated Successful')</script>";
                                ModelState.Clear();
                                return RedirectToAction("IndexData", "Photo");
                            }
                            else
                            {
                                TempData["UpdateMessage"] = "<script>alert('Data Not Updated')</script>";
                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image Size shuld be less than 2 MB')</script>";
                        }
                    }
                    else
                    {
                        TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                    }

                }
                else
                {
                    s.image_path = Session["Image"].ToString();
                    db.Entry(s).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {

                        TempData["UpdateMessage"] = "<script>alert('Data Updated Successful')</script>";
                        ModelState.Clear();
                        return RedirectToAction("IndexData", "Photo");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Not Updated')</script>";
                    }
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var StudentRow = db.Photos.Where(model => model.p_id == id).FirstOrDefault();
                if ((StudentRow != null))
                {
                    db.Entry(StudentRow).State = EntityState.Deleted;
                    int a = db.SaveChanges();

                    if (a > 0)
                    {
                        TempData["DeleteMessage"] = "<script>alert('Data Deleted Successfully')</script>";
                        string ImagePath = Request.MapPath(StudentRow.image_path.ToString());
                        if (System.IO.File.Exists(ImagePath))
                        {
                            System.IO.File.Delete(ImagePath);
                        }
                    }
                    else
                    {
                        TempData["DeleteMessage"] = "<script>alert('Data  not deleted')</script>";
                    }
                }
            }
            return RedirectToAction("IndexData", "Photo");
        }


        public ActionResult Details(int id)
        {
            var StudentRow = db.Photos.Where(model => model.p_id == id).FirstOrDefault();
            Session["Image2"] = StudentRow.image_path.ToString();
            return View(StudentRow);
        }


      
        public ActionResult Reset()
        {
            ModelState.Clear();
            return RedirectToAction("Index", "Admin");
        }
    }
}