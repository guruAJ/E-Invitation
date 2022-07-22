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
    public class VacancyPlanController : Controller
    {
        public readonly RepositoryOcassion _repositoryOcassion;
        public readonly RepositoryVacancyPlan _repositoryVacancyPlan;
        public readonly RepositoryUser _repositoryUser;
        public readonly RepositoryOcassionMapping _repositoryOcassionMapping;
        public VacancyPlanController(RepositoryOcassion repositoryOcassion, RepositoryVacancyPlan repositoryVacancyPlan, RepositoryUser repositoryUser, RepositoryOcassionMapping repositoryOcassionMapping)
        {
            _repositoryOcassion = repositoryOcassion;
            _repositoryVacancyPlan = repositoryVacancyPlan;
            _repositoryUser = repositoryUser;
            _repositoryOcassionMapping = repositoryOcassionMapping;
        }
        public IActionResult Index()
        {
            ViewBag.AllOcassion = _repositoryOcassion.GetAllActive();
            return View();
        }
        public IActionResult GetAll(int OcassionFilterId)
        {
            //if (OcassionFilterId > 0)
            //{
            //    TempData["OcassionId"] = _repositoryVacancy.GetAll(OcassionFilterId);
            //    ViewBag.EnclosureCount = _repositoryVacancy.EnclosureCountData(OcassionFilterId);
            //}
            //GetAllData();
            //TempData["Id"] = 0;
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            { 
                var data = _repositoryVacancyPlan.GetAllVacancy(OcassionFilterId);
            return Json(data);
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult GetNotMappAll(int OcassionFilterId)
        {
            //if (OcassionFilterId > 0)
            //{
            //    TempData["OcassionId"] = _repositoryVacancy.GetAll(OcassionFilterId);
            //    ViewBag.EnclosureCount = _repositoryVacancy.EnclosureCountData(OcassionFilterId);
            //}
            //GetAllData();
            //TempData["Id"] = 0;
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                var data = _repositoryUser.GetNotMappAll(OcassionFilterId);
                return Json(data);
            }
            else
                return Redirect("/Login/Index");
        }

        public IActionResult GetAllByUserId(int OcassionFilterId)
        {
            //if (OcassionFilterId > 0)
            //{
            //    TempData["OcassionId"] = _repositoryVacancy.GetAll(OcassionFilterId);
            //    ViewBag.EnclosureCount = _repositoryVacancy.EnclosureCountData(OcassionFilterId);
            //}
            //GetAllData();
            //TempData["Id"] = 0;
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
               
            var data = _repositoryVacancyPlan.GetAllVacancyUserBy(OcassionFilterId, Logins.Id);
           
            return Json(data);
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult GetAllAvailableByUserId(int OcassionFilterId)
        {
            //if (OcassionFilterId > 0)
            //{
            //    TempData["OcassionId"] = _repositoryVacancy.GetAll(OcassionFilterId);
            //    ViewBag.EnclosureCount = _repositoryVacancy.EnclosureCountData(OcassionFilterId);
            //}
            //GetAllData();
            //TempData["Id"] = 0;
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                
            var data = _repositoryVacancyPlan.GetAllVacancyWithPlanByUserId(OcassionFilterId, Logins.Id);
            return Json(data);
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult GetAllAvailable(int OcassionFilterId)
        {
            //if (OcassionFilterId > 0)
            //{
            //    TempData["OcassionId"] = _repositoryVacancy.GetAll(OcassionFilterId);
            //    ViewBag.EnclosureCount = _repositoryVacancy.EnclosureCountData(OcassionFilterId);
            //}
            //GetAllData();
            //TempData["Id"] = 0;
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                var data = _repositoryVacancyPlan.GetAllVacancyWithPlan(OcassionFilterId);
            return Json(data);
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult SavePlan(VacancyPlan db)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                return Json(_repositoryVacancyPlan.SaveAsync(db));
            }
            else
                return Redirect("/Login/Index");
        }

        public IActionResult ForUser()
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                ViewBag.AllOcassion = _repositoryOcassion.GetAllActive();
            return View();
            }
            else
                return Redirect("/Login/Index");
        }

        public IActionResult SaveMappAll(string[] Arr,int OcassionId)
        {
            List<OcassionMapping> lst = new List<OcassionMapping>();
            for (int i=0;i<Arr.Length;i++)
            {
                OcassionMapping Db = new OcassionMapping();
                Db.UserId = Convert.ToInt32(Arr[i].ToString());
                Db.OcassionId = OcassionId;
                Db.IsActive = 1;
                lst.Add(Db);
                _repositoryOcassionMapping.Save(Db);
            }
            

            return Json(1);
        }
        public async Task<IActionResult> DeleteMappingAsync(int UserId, int OcassionId)
        {
            
           await  _repositoryOcassionMapping.delAsync(UserId, OcassionId);


            VacancyPlan db = new VacancyPlan();
            db.UserId = UserId;
            db.OcassionId = OcassionId;
            _repositoryVacancyPlan.del(db);
            
            return Json(1);
        }
      
    }
}
