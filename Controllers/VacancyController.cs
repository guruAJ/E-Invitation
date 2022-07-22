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

    public class OVacancyController : Controller
    {
        public readonly RepositoryVacancy _repositoryVacancy;
        public readonly RepositoryOcassion _repositoryOcassion;
        public readonly RepositoryCategory _repositoryCategory;
        public readonly RepositoryEnclosure _repositoryEnclosure;
        public readonly RepositoryRank _repositoryRank;


        string OccasionName = null;
        public IActionResult Index(int Id)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                Vacancy vacancy = new Vacancy();
            if (Id != 0)
            {
                vacancy = _repositoryVacancy.GetById(Id);
                TempData["Id"] = vacancy.Id;
                TempData.Keep("Id");

                TempData["drop"] = vacancy.OcassionId;
                TempData.Keep("drop");


                TempData["OcassionId"] = _repositoryVacancy.GetAll(vacancy.OcassionId);
                ViewBag.EnclosureCount = _repositoryVacancy.EnclosureCountData(vacancy.OcassionId);

               
                GetAllData();
                return View(vacancy);
            }

            TempData["drop"] = 0;
            TempData["OcassionId"] = null;
            TempData.Keep("OcassionId");
            TempData["Id"] = 0;
            TempData.Keep("Id");

              

           
            GetAllData();
            return View();
            }
            else
                return Redirect("/Login/Index");
        }
        public OVacancyController(RepositoryVacancy repositoryVacancy,
            RepositoryOcassion repositoryOcassion, RepositoryCategory repositoryCategory, RepositoryEnclosure repositoryEnclosure, RepositoryRank repositoryRank)
        {
            _repositoryVacancy = repositoryVacancy;
            _repositoryOcassion = repositoryOcassion;
            _repositoryCategory = repositoryCategory;
            _repositoryEnclosure = repositoryEnclosure;
            _repositoryRank = repositoryRank;
        }
        //public JsonResult GetOcassion()
        //{
        //    User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
        //    try
        //    {


        //        ViewBag.AllOcassion = _repositoryOcassion.GetAllActive();
        //        ViewBag.Category = _repositoryCategory.GetAll();
        //        ViewBag.Enclosure = _repositoryEnclosure.GetAll();
        //        ViewBag.Rank = _repositoryRank.GetAll();
        //        return Json(ViewBag.AllOcassion);

        //    }
        //    catch (Exception ex)
        //    {

        //        return Json(0);
        //    }
        //}
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Vacancy Db)
        {
            try
            {
                User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
                if (Logins.IsNotNull())
                {
                    //if (Db.OcassionId > 0)
                    //{
                   

                  
                    if (ModelState.IsValid)
                        {
                            if (!string.IsNullOrEmpty(TempData["Id"].ToString()))
                            {
                                Db.Id = Convert.ToInt32(TempData["Id"]);

                            Vacancy temp = new Vacancy();
                            if (Db.Id == 0)
                                temp = await _repositoryVacancy.GetCatDesc(Db);

                            if (temp != null && temp.RankDesc != null && temp.RankDesc != Db.RankDesc)
                                Db.RankDesc = temp.RankDesc + "," + Db.RankDesc;
                        }
                            else
                            {
                                Db.CreatedBy = Logins.Id;
                                Db.CreatedOn = DateTime.Now;
                            }

                            int ret = await _repositoryVacancy.Save(Db);
                            if (ret == 1)
                            {

                                ViewBag.message = "Data is saved successfully";

                            }
                            //else if (ret == 2)
                            //{
                            //    ViewBag.message = "" + Db.Title + " is Update successfully";
                            //}
                            else if (ret == 3)
                            {
                                ViewBag.message = "Already exists Category";
                            }
                            TempData["OcassionId"] = _repositoryVacancy.GetAll(Db.OcassionId);
                            ViewBag.EnclosureCount = _repositoryVacancy.EnclosureCountData(Db.OcassionId);
                            GetAllData();
                            TempData["Id"] = 0;
                            TempData.Keep("Id");
                            return View("Index", Db);
                        }
                        else
                        {

                            GetAllData();
                            TempData["Id"] = 0;
                            return View("Index", Db);
                        }
                    //}
                    //else
                    //{
                        
                    //    ModelState.AddModelError("", "Please Select Event Name");
                    //    TempData["OcassionId"] = _repositoryVacancy.GetAll(Db.OcassionId);
                    //    ViewBag.EnclosureCount = _repositoryVacancy.EnclosureCountData(Db.OcassionId);
                    //    GetAllData();
                    //    TempData["Id"] = 0;
                    //    TempData.Keep("Id");
                    //    return View("Index", Db);
                    //}
                }
                else
                    return Redirect("/Login/Index");
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }
        public void GetAllData()
        {


            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {

                ViewBag.AllOcassion = _repositoryOcassion.GetAllActive();
                //try
                //{

                //        ViewBag.AllData = _repositoryVacancy.GetAll(Convert.ToInt32(TempData["OcassionId"]));
                //}
                //catch(Exception ex)
                //{
                //    ViewBag.AllData = null;
                //}
                ViewBag.Category = _repositoryCategory.GetAll();
                ViewBag.Enclosure = _repositoryEnclosure.GetAll();
                ViewBag.Rank = _repositoryRank.GetAll();
            }
        }
        public IActionResult GetAll(int OcassionFilterId)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
               
                if (OcassionFilterId > 0)
            {
                TempData["OcassionId"] = _repositoryVacancy.GetAll(OcassionFilterId);
                ViewBag.EnclosureCount = _repositoryVacancy.EnclosureCountData(OcassionFilterId);
            }
            GetAllData();

             

            TempData["drop"] = OcassionFilterId;
            TempData.Keep("drop");

            TempData["Id"] = 0;
            TempData.Keep("Id");
            return View("Index");
            }
            else
                return Redirect("/Login/Index");
        }
        public async Task<IActionResult> DeleteDataAsync(int Id)
        {
            return Json(await _repositoryVacancy.DeleteData(Id));

        }
    }
}
