using E_Invitation.Helpers;
using E_Invitation.Models;
using E_Invitation.Repository;
using E_Invitation.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Controllers
{
    public class CategoryController : Controller
    {
        public readonly RepositoryCategory _repositoryCategory;
        public CategoryController(RepositoryCategory repositoryCategory)
        {
            _repositoryCategory = repositoryCategory;
        }
        public IActionResult Index(int Id)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                Category category = new Category();
            if (Id != 0)
            {
                category = _repositoryCategory.GetById(Id);
                TempData["Id"] = category.Id;
                ViewBag.AllData = _repositoryCategory.GetAll();
                ViewBag.add = true;
                return View(category);
            }

            ViewBag.add = false;
            TempData["Id"] = 0;
            ViewBag.AllData = _repositoryCategory.GetAll();
            return View();
            }
            else
                return Redirect("/Login/Index");
        }
        public async Task<IActionResult> Save(Category Db)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                try
            {
              
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(TempData["Id"].ToString()))
                    {
                        Db.Id = Convert.ToInt32(TempData["Id"]);
                    }
                    Db.CreatedBy = Logins.Id;
                    int ret = await _repositoryCategory.Save(Db);
                    if (ret == 1)
                    {
                        ViewBag.add = false;
                        ViewBag.message = "" + Db.Title + " is saved successfully";
                        
                    }
                    //else if (ret == 2)
                    //{
                    //    ViewBag.message = "" + Db.Title + " is Update successfully";
                    //}
                    else if (ret == 3)
                    {
                        ViewBag.add = true;
                        ViewBag.message = "Already exists Category";
                    }
                    var AllData = _repositoryCategory.GetAll();

                    ViewBag.AllData = AllData;
                   
                    TempData["Id"] = 0;
                    return View("Index", Db);
                }
                else
                {
                    var AllData = _repositoryCategory.GetAll();
                    ViewBag.add = true;
                    ViewBag.AllData = AllData;
                   
                    TempData["Id"] = 0;
                    return View("Index", Db);
                }
            }
            catch(Exception ex)
            {
                return View("Index");
            }
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult Delete(Category Db)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
               
            try
            {
                Db.CreatedBy = Logins.Id;

                int ret = _repositoryCategory.del(Db);
                if (ret == 1)
                {
                    ViewBag.add = false;
                    ViewBag.Deletemessage = "Record Deleted successfully";
                    ViewBag.AllData = _repositoryCategory.GetAll();

                    return Json(1);
                }
                else
                {

                    return Json(0);
                }
            }
            catch (Exception ex)
            {
                ViewBag.AllData = _repositoryCategory.GetAll();
                return View("Index");
            }
        }
            else
              return Redirect("/Login/Index");
    }
        public ActionResult isSuccess(Boolean Id)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                var AllData = _repositoryCategory.GetAll();
            ViewBag.add = Id;
            ViewBag.AllData = AllData;

            TempData["Id"] = 0;
            return View("Index");
    }
            else
              return Redirect("/Login/Index");
}
        public JsonResult GetAll()
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                return Json(_repositoryCategory.GetAll());
            }
            else
                return Json(-3);
           
        }
    }
}
