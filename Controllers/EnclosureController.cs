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
    public class EnclosureController : Controller
    {
        public readonly RepositoryEnclosure _repositoryEnclosure;
        public EnclosureController(RepositoryEnclosure repositoryEnclosure)
        {
            _repositoryEnclosure = repositoryEnclosure;
        }
        public IActionResult Index(int Id)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                Enclosure enclosure = new Enclosure();
            if (Id != 0)
            {
                enclosure = _repositoryEnclosure.GetById(Id);
                TempData["Id"] = enclosure.Id;
                ViewBag.AllData = _repositoryEnclosure.GetAll();
                TempData.Keep("Id");
                return View(enclosure);
            }

            TempData["Id"] = 0;
            TempData.Keep("Id");
            ViewBag.AllData = _repositoryEnclosure.GetAll();
            return View();
            }
            else
                return Redirect("/Login/Index");
        }
        public async Task<IActionResult> Save(Enclosure Db)
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
                        Db.CreatedBy = Db.CreatedBy;
                        Db.UpdatedOn = DateTime.Now;
                        Db.CreatedOn = Db.CreatedOn;
                    }
                    else
                    {
                      
                        Db.CreatedOn = DateTime.Now;
                        Db.CreatedBy = Logins.Id;

                    }
                   
                    int ret = await _repositoryEnclosure.Save(Db);
                    if (ret == 1)
                    {

                        ViewBag.message = "" + Db.Title + " is saved successfully";

                    }
                    //else if (ret == 2)
                    //{
                    //    ViewBag.message = "" + Db.Title + " is Update successfully";
                    //}
                    else if (ret == 3)
                    {
                        ViewBag.message = "Already exists Category";
                    }
                    var AllData = _repositoryEnclosure.GetAll();

                    ViewBag.AllData = AllData;

                    TempData["Id"] = 0;
                    TempData.Keep("Id");
                    return View("Index", Db);
                }
                else
                {
                    var AllData = _repositoryEnclosure.GetAll();

                    ViewBag.AllData = AllData;

                    TempData["Id"] = 0;
                    TempData.Keep("Id");
                    return View("Index", Db);
                }
            }
            catch (Exception ex)
            {
                return View("Index");
            }
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult Delete(Enclosure Db)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
              
            try
            {
                Db.CreatedBy = Logins.Id;

                int ret = _repositoryEnclosure.del(Db);
                if (ret == 1)
                {

                    ViewBag.Deletemessage = "Record Deleted successfully";
                    ViewBag.AllData = _repositoryEnclosure.GetAll();

                    return Json(1);
                }
                else
                {

                    return Json(0);
                }
            }
            catch (Exception ex)
            {
                ViewBag.AllData = _repositoryEnclosure.GetAll();
                return View("Index");
            }
        }
            else
              return Redirect("/Login/Index");
    }

        public JsonResult GetAll()
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                return Json(_repositoryEnclosure.GetAll());

            }

            else
                return Json(-3);
    }
    }
}
