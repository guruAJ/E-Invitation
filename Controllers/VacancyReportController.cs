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
    public class VacancyReportController : Controller
    {
        public readonly RepositoryOcassion _repositoryOcassion;
        public readonly RepositoryVacancyReport _repositoryVacancyReport;
        public readonly RepositoryUser _repositoryUser;
        public readonly RepositoryOcassionStatus _repositoryOcassionStatus;
        public readonly RepositoryVacancyPlan _repositoryVacancyPlan;
        public VacancyReportController(RepositoryOcassion repositoryOcassion, RepositoryVacancyReport repositoryVacancyReport, RepositoryUser repositoryUser, RepositoryOcassionStatus repositoryOcassionStatus, RepositoryVacancyPlan repositoryVacancyPlan)
        {
            _repositoryOcassion = repositoryOcassion;
            _repositoryVacancyReport = repositoryVacancyReport;
            _repositoryUser = repositoryUser;
            _repositoryOcassionStatus = repositoryOcassionStatus;
            _repositoryVacancyPlan = repositoryVacancyPlan;
        }

        public IActionResult Index()
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                ViewBag.AllOcassion = _repositoryOcassion.GetAllActive().Where(c => c.Id != 0);
                //ViewBag.AllUser = _repositoryUser.GetAllOcassionBy(1);

                

                return View();
            }
            else

                return Redirect("/Login/Index");
        }
        public IActionResult CloseEvent()
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                ViewBag.AllOcassion = _repositoryOcassion.GetAllDeActive().Where(c => c.Id != 0);
                //ViewBag.AllUser = _repositoryUser.GetAllOcassionBy(1);



                return View();
            }
            else

                return Redirect("/Login/Index");
        }
        public IActionResult GetAll(int OcassionFilterId,int UserId)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {

                var data = _repositoryVacancyReport.GetAll(OcassionFilterId, UserId);
            return Json(data);
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult GetStatus(int OcassionFilterId, int UserId)
        {
            return Json(_repositoryOcassionStatus.GetById(OcassionFilterId, UserId));
        }

        public async Task<IActionResult> SaveStatusAsync(int OcassionFilterId, int UserId,int status)
        {
            OcassionStatus db = new OcassionStatus();
            db.OcassionId = OcassionFilterId;
            db.UserId = UserId;
            int ret = await _repositoryOcassionStatus.Save(db, status);
            if(ret==1)
            {
               //await _repositoryVacancyPlan.MirronSave(OcassionFilterId, UserId);
                _repositoryVacancyReport.VacancyRecall(db.OcassionId, db.UserId);
            }


            return Json(ret);
        }
        public IActionResult GetAllStatus(int OcassionFilterId)
        {

            var AllData = _repositoryUser.GetAllOcassionBy(OcassionFilterId);

            var AllData1 = _repositoryOcassionStatus.GetAll(OcassionFilterId);

            if(AllData.Count() == AllData1.Count())
            {
                return Json(1);
            }
            else
            {
                return Json(2);
            }
            
        }
    }
}
