using E_Invitation.DTO;
using E_Invitation.Helpers;
using E_Invitation.Models;
using E_Invitation.Repository;
using E_Invitation.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Controllers
{
    public class GestListController : Controller
    {
        public readonly RepositoryOcassion _repositoryOcassion;
        public readonly RepositoryVacancyPlan _repositoryVacancyPlan;
        public readonly RepositoryGestList _repositoryGestList;
        public readonly RepositoryOcassionStatus _repositoryOcassionStatus;
        private readonly IWebHostEnvironment _env;

        public IConfiguration Configuration { get; }
        public GestListController(IWebHostEnvironment env,RepositoryOcassion repositoryOcassion, RepositoryVacancyPlan repositoryVacancyPlan, RepositoryGestList repositoryGestList, IConfiguration configuration, RepositoryOcassionStatus repositoryOcassionStatus)
        {
            _repositoryOcassion = repositoryOcassion;
            _repositoryVacancyPlan = repositoryVacancyPlan;
            _repositoryGestList = repositoryGestList;
            Configuration = configuration;
            _repositoryOcassionStatus = repositoryOcassionStatus;
            _env = env;
        }
        public async Task<IActionResult> Index(AddGestList db)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
             TempData["OcassionId"] = db.OcassionId;
            TempData["EnclosureId"] = db.EnclosureId;
            TempData["RankId"] = db.RankId;


            return View();
            }
            else
                return Redirect("/Login/Index");
        }
        public async Task<IActionResult> GetGestByUserId(int Id)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
            DTOGestList dTOGestList = new DTOGestList();
           
            int OcassionId = Convert.ToInt32(TempData["OcassionId"]);
            int EnclosureId = Convert.ToInt32(TempData["EnclosureId"]);
            int RankId = Convert.ToInt32(TempData["RankId"]);

            TempData.Keep("OcassionId");
            TempData.Keep("EnclosureId");
            TempData.Keep("RankId");
           

            VacancyPlanEntry vacancy = new VacancyPlanEntry();
            vacancy  = await _repositoryVacancyPlan.GetAllVacancyRankWise(OcassionId, Logins.Id, EnclosureId, RankId);

            List<AddGestList> addGestList = new List<AddGestList>();
            AddGestList Db = new AddGestList();
            Db.UserId = Logins.Id;
            Db.OcassionId = OcassionId;
            Db.EnclosureId = EnclosureId;
            Db.RankId = RankId;

            addGestList = _repositoryGestList.GetAll(Db);


            dTOGestList.addGestLists = addGestList;
            dTOGestList.vacancies = vacancy.vacancieslist;

            dTOGestList.ocassionStatuses = _repositoryOcassionStatus.GetById(OcassionId,Logins.Id);


            return Json(dTOGestList);
            }
            else
                return Redirect("/Login/Index");
        }


        public async Task<IActionResult> GetGestByOcassionId(int OcassionId)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                DTOGestList dTOGestList = new DTOGestList();



                VacancyPlanEntry vacancy = new VacancyPlanEntry();
                vacancy = await _repositoryVacancyPlan.GetAllVacancyOcassion(OcassionId);

                List<AddGestList> addGestList = new List<AddGestList>();
                AddGestList Db = new AddGestList();
                Db.UserId = Logins.Id;
                Db.OcassionId = OcassionId;
               
                addGestList = _repositoryGestList.GetAllOcassionby(Db);


                dTOGestList.addGestLists = addGestList;
                dTOGestList.vacancies = vacancy.vacancieslist;

                dTOGestList.ocassionStatuses = _repositoryOcassionStatus.GetById(OcassionId, Logins.Id);


                return Json(dTOGestList);
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult DeleteData(int Id)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                return Json(_repositoryGestList.del(Id));
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult GetbyArmyno(string ArmyNo)
        {
            try
            {
                User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
                if (Logins.IsNotNull())
                {
                    return Json(_repositoryGestList.GetbyArmyno(ArmyNo));
                }
                else
                    return Redirect("/Login/Index");
            }
            catch (Exception ex)
            {
                return Json(0);    
            }
            
        }
        public async Task<IActionResult> SaveDataAsync(AddGestList Db)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
               
            int ret = 0;

            try
            {
                int OcassionId = Convert.ToInt32(TempData["OcassionId"]);
                int EnclosureId = Convert.ToInt32(TempData["EnclosureId"]);
                int RankId = Convert.ToInt32(TempData["RankId"]);

                
                Db.OcassionId = OcassionId;
                Db.EnclosureId = EnclosureId;
                Db.RankId = RankId;
                if (Logins != null && Logins.Id > 0)
                {
                    Db.UserId = Logins.Id;
                    if (Db.OcassionId !=0 && Db.EnclosureId !=0 && Db.RankId!=0)
                    ret = await _repositoryGestList.Save(Db);
                    return Json(ret);
                }
                else
                {
                    return Json(4);
                }
            }
            catch(Exception ex)
            {
                return Json(4);
            }
            }
            else
                return Redirect("/Login/Index");
        }

        public async Task<IActionResult> UpdatePassDataById(AddGestList Db)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {

                int ret = 0;

                try
                {
                   
                    if (Logins != null && Logins.Id > 0)
                    {
                        
                       
                            ret =  _repositoryGestList.UpdatePass(Db);
                        return Json(ret);
                    }
                    else
                    {
                        return Json(4);
                    }
                }
                catch (Exception ex)
                {
                    return Json(4);
                }
            }
            else
                return Redirect("/Login/Index");
        }
        public async Task<IActionResult> Compare(List<IFormFile> files)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                long size = files.Sum(f => f.Length);
            try
            {
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        string[] ss = formFile.ContentType.Split("/");
                        string ImageName = Guid.NewGuid() + "." + ss[1].ToString();
                           // var path = Path.Combine(_env.ContentRootPath, "App_Data/Files");
                            var filePath = Path.Combine(_env.ContentRootPath, "wwwroot/images/Profile/"+ImageName);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        return Json(ImageName);
                    }
                }

            }catch(Exception ex)
            {
                _ = ex;
            }
            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }
            else
              return Redirect("/Login/Index");
    }
        //[HttpPost]
        //public ActionResult Compare(string Name)
        //{
        //    try
        //    {
        //        string _filePath = "";

        //        foreach (string file in Request.Files)
        //        {
        //            var fileContent = Request.Files[file];
        //            if (fileContent != null && fileContent.ContentLength > 0)
        //            {
        //                _filePath = Path.Combine(Server.MapPath("~/UploadedFiles/Excel/"), fileContent.FileName);
        //                fileContent.SaveAs(_filePath);
        //                return Json(1);//file not found

        //            }
        //            else
        //            {
        //                return Json(0);//file not found
        //            }
        //        }
        //        return Json(1);//file not found
        //    }
        //    catch (Exception ex)
        //    {
        //        _ = ex;
        //        return Json(-1);//Error.
        //    }
        //}

        public IActionResult PassGenerateAll(int OcassionId)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                DTOGestList dTOGestList = new DTOGestList();
           


            List<DTOGestPass> list = new List<DTOGestPass>();



            list = _repositoryGestList.GetAllPass(Logins.Id, OcassionId);






            return View(list);
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult colorCodeAll(int OcassionId)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                DTOGestList dTOGestList = new DTOGestList();



                List<DTOGestPass> list = new List<DTOGestPass>();



                list = _repositoryGestList.GetAllPass(Logins.Id, OcassionId);






                return View(list);
            }
            else
                return Redirect("/Login/Index");
        }
        public async Task<IActionResult> GetGestPass(int OcassionId)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                DTOGestList dTOGestList = new DTOGestList();
            
          
            List<DTOGestPass> list = new List<DTOGestPass>();
           


            list = _repositoryGestList.GetAllPass(Logins.Id,OcassionId);


            
           

            return Json(list);
        }
            else
              return Redirect("/Login/Index");
    }
        public IActionResult GetTotalGest(int UserId, int OcassionId, int Encl, int rankId)
        {
           return Json(_repositoryGestList.GetTotalGest(UserId, OcassionId, Encl, rankId));
        }
    }
}
