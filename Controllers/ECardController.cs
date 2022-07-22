using E_Invitation.Helpers;
using E_Invitation.Models;
using E_Invitation.Repository;
using E_Invitation.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Controllers
{
    public class ECardController : Controller
    {
        public readonly RepositoryOcassion _repositoryOcassion;
        private readonly IWebHostEnvironment _env;
        public readonly RepositoryECard _repositoryECard;
        public ECardController(RepositoryOcassion repositoryOcassion,IWebHostEnvironment env, RepositoryECard repositoryECard)
        {
            _repositoryOcassion = repositoryOcassion;
            _env = env;
            _repositoryECard = repositoryECard;
        }
        public IActionResult Index()
        {
            ViewBag.GetAllOcassionData = _repositoryOcassion.GetAllActive();
            return View();
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
                            var filePath = Path.Combine(_env.ContentRootPath, "wwwroot/images/ECard/" + ImageName);

                            using (var stream = System.IO.File.Create(filePath))
                            {
                                await formFile.CopyToAsync(stream);
                            }
                            return Json(ImageName);
                        }
                    }

                }
                catch (Exception ex)
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

        [HttpPost]

        public async Task<IActionResult> Save(ECard Db)
        {

            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {

                int ret = await _repositoryECard.Save(Db);

                return Json(ret);
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult GetAll(int OcassionId)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {

                return Json(_repositoryECard.GetAll(OcassionId));
            }
            else
                return Redirect("/Login/Index");

        }

    }
}
