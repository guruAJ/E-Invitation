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
    public class RankController : Controller
    {
        public readonly RepositoryRank _repositoryRank;
        public RankController(RepositoryRank repositoryRank)
        {
            _repositoryRank = repositoryRank;
        }
        public IActionResult Index(int Id)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                if (HttpContext.Session.IsNotNull())
            {
                Rank rank = new Rank();
                if (Id != 0)
                {
                    rank = _repositoryRank.GetById(Id);
                    TempData["Id"] = rank.Id;

                    ViewBag.AllData = _repositoryRank.GetAll();
                    TempData.Keep("Id");
                    return View(rank);
                }

                TempData["Id"] = 0;
                TempData.Keep("Id");
                ViewBag.AllData = _repositoryRank.GetAll();
                return View();
            }
            else
                return Redirect("/Login/Index");
            }
            else
                return Redirect("/Login/Index");
        }
        public async Task<IActionResult> Save(Rank Db)
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
                    Db.CreatedOn = DateTime.Now;
                    Db.CreatedBy = Logins.Id;
                    Db.IsActive = 1;
                    int ret = await _repositoryRank.Save(Db);
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
                        ViewBag.message = "Already exists Rank";

                    }
                    var AllData = _repositoryRank.GetAll();

                    ViewBag.AllData = AllData;

                    TempData["Id"] = 0;
                    TempData.Keep("Id");
                    return View("Index", Db);
                }
                else
                {
                    var AllData = _repositoryRank.GetAll();

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
        public IActionResult Delete(Rank Db)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
               
            try
            {
                Db.CreatedBy = Logins.Id;

                int ret = _repositoryRank.del(Db);
                if (ret == 1)
                {

                    ViewBag.Deletemessage = "Record Deleted successfully";
                    ViewBag.AllData = _repositoryRank.GetAll();

                    return Json(1);
                }
                else
                {
                   
                    return Json(0);
                }

                
              
            }
            catch (Exception ex)
            {
                ViewBag.AllData = _repositoryRank.GetAll();
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
                return Json(_repositoryRank.GetAll());
            }
            else
                return Json(-3);
    }
    }
}
