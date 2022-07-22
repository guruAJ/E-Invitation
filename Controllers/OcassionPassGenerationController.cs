using E_Invitation.Helpers;
using E_Invitation.Models;
using E_Invitation.Repository;
using E_Invitation.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_Invitation.Controllers
{
    public class PassGenerationController : Controller
    {
        public readonly RepositoryOcassion _repositoryOcassion;
        public readonly RepositoryECard _repositoryECard;
        public PassGenerationController(RepositoryOcassion repositoryOcassion, RepositoryECard repositoryECard)
        {
            _repositoryOcassion = repositoryOcassion;
            _repositoryECard= repositoryECard;
        }
        public IActionResult Index(int Id)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                Ocassion ocassion = new Ocassion();
                if (Id != 0)
                {
                    ocassion = _repositoryOcassion.GetById(Id);

                    TempData["Id"] = ocassion.Id;
                    TempData.Keep("Id");
                    ViewBag.AllData = _repositoryOcassion.GetAll();
                    return View(ocassion);
                }

                TempData["Id"] = 0;
                TempData.Keep("Id");
                ViewBag.AllData = _repositoryOcassion.GetAll();
                return View();
            }
            else
                return Redirect("/Login/Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Ocassion Db)
        {

            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {


                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(TempData["Id"].ToString()))
                    {
                        Db.Id = Convert.ToInt32(TempData["Id"]);
                        Db.CreatedBy = Db.CreatedBy;
                        Db.UpdatedOn = DateTime.Now;
                        Db.CreatedOn = DateTime.Now;
                    }
                    else
                    {

                        Db.CreatedOn = DateTime.Now;
                        Db.CreatedBy = Logins.Id;

                    }
                    Db.CreatedBy = Logins.Id;
                    int ret = await _repositoryOcassion.Save(Db);
                    if (ret == 1)
                    {
                        ViewBag.message = "" + Db.OcassionName + " is saved successfully";
                    }
                    else if (ret == 2)
                    {
                        ViewBag.message = "" + Db.OcassionName + " is Update successfully";
                    }
                    else if (ret == 3)
                    {
                        ViewBag.message = "Allready Exits Event Name And Date";

                    }
                    var AllData = _repositoryOcassion.GetAll();

                    ViewBag.AllData = AllData;
                    TempData["Id"] = 0;
                    TempData.Keep("Id");
                    return View("Index", Db);
                }
                else
                {
                    var AllData = _repositoryOcassion.GetAll();
                    TempData["Id"] = 0;
                    ViewBag.AllData = AllData;
                    return View("Index", Db);
                }
            }
            else
                return Redirect("/Login/Index");
        }


        public IActionResult SaveStatus(int OcassionId, int Status)
        {
            ECard eCard = new ECard();
            eCard.OcassionId = OcassionId;
          bool st=  _repositoryECard.CheckUserExist(eCard);
            if (st)
                return Json(_repositoryOcassion.SaveStatus(OcassionId, Status));
            else
                return Json(0);

        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
