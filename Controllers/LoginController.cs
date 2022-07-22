using E_Invitation.DTO;
using E_Invitation.Helpers;
using E_Invitation.Models;
using E_Invitation.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Controllers
{
  
    public class LoginController : Controller
    {
        public readonly RepositoryUser _repositoryUser;
        public LoginController(RepositoryUser repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Validate(Login Db)
        {
            try {
                if (ModelState.IsValid)
                {
                    User user = new User();
                    user.UserName = Db.UserName;
                    user.Password = Db.Password;
                    user = _repositoryUser.Validated(user);
                    Db.Id = user.Id;
                    Db.TypeId = user.TypeId;
                    Db.GroupId = user.GroupId;
                    if (Db.Id > 0)
                    {
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "User", Db);

                      
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "UserName Or Password Invalid");
                        return View("index", Db);
                    }
                

                }
                else
                {
                    return View("Index", Db);
                }
            }
            catch(Exception ex)
            {
                //  ModelState.AddModelError(string.Empty, ex.Message);
                ModelState.AddModelError(string.Empty, "UserName Or Password Invalid");
                return View("Index", Db);
            }
            }
        public IActionResult Logout()
        {

            foreach (var cookie in Request.Cookies.Keys)

            {
                if (cookie == ".AspNetCore.Session")
                    Response.Cookies.Delete(cookie);
            }

            return View("Index");

        }

    }
}
