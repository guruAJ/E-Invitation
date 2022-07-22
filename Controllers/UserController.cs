using E_Invitation.Data;
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
    public class UserController : Controller
    {

        public readonly ApplicationDbContext _context;

        public readonly RepositoryUser _repositoryUser;
        public UserController(RepositoryUser repositoryUser, ApplicationDbContext context)
        {
            _repositoryUser = repositoryUser;
            _context = context;
        }


        #region AddUser
        public IActionResult Index(int Id)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                if (Id != 0)
                {
                    User user = new User();
                    user = _repositoryUser.GetById(Id);
                    TempData["Id"] = Id;
                    TempData.Keep("Id");
                    GetAll();
                    return View(user);
                }
                else
                {
                    TempData["Id"] = "";
                    TempData.Keep("Id");
                    GetAll();
                    return View();
                }

            }
            else
            {

                return Redirect("/Login/Index");
            }
        }

        public async Task<IActionResult> Save(User Db)
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
                            Db.GroupId = Logins.Id;
                            Db.CreatedOn = Db.CreatedOn;
                            Db.CreatedBy = Logins.Id;
                        }
                        else
                        {

                            Db.GroupId = Logins.Id;
                            Db.CreatedOn = DateTime.Now;
                            Db.CreatedBy = Logins.Id;
                        }
                        Db.Password = Utility.Security.GetHashString(Db.Password);
                        Db.TypeId = 2;
                        int ret = await _repositoryUser.Save(Db);
                        if (ret == 1)
                        {

                            ViewBag.message = "" + Db.UserName + " is saved successfully";

                        }
                        //else if (ret == 2)
                        //{
                        //    ViewBag.message = "" + Db.Title + " is Update successfully";
                        //}
                        else if (ret == 2)
                        {
                            ViewBag.message = "Already exists User";
                        }

                        GetAll();

                        TempData["Id"] = "";
                        TempData.Keep("Id");
                        return View("Index", Db);
                    }
                    else
                    {


                        TempData["Id"] = "";
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
        public void GetAll()
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {

                var AllData = _repositoryUser.GetAll(Logins.Id);


                ViewBag.AllData = AllData;
            }

        }

        public IActionResult GetAllOcassionBy(int OcassionFilterId)
        {

            var AllData = _repositoryUser.GetAllOcassionBy(OcassionFilterId);
            return Json(AllData);
        }
        #endregion

        public IActionResult MakeAdmin(int UserId)
        {
            var AllData = _repositoryUser.MakeAdmin(UserId);
            return Json(AllData);
        }

        public IActionResult RemoveAdmin(int UserId)
        {
            var AllData = _repositoryUser.RemoveAdmin(UserId);
            return Json(AllData);
        }
        #region ChangePassword
        public IActionResult ChangePassword()
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                return View();
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult ChangePasswordSave(User Db)
        {
            if (Db.Password != null || Db.ConfirmPassword != null)
            {
                User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
                if (Logins.IsNotNull())
                {
                    Db.Id = Logins.Id;
                    if (Db.Password == Db.ConfirmPassword)
                    {
                        if (Db.Password.Length >= 6)
                        {
                            Db.Password = Utility.Security.GetHashString(Db.Password);
                            int ret = _repositoryUser.ChnagePassword(Db);

                            if (ret == 1)
                            {

                                ViewBag.message = "Password Changed successfully";

                            }
                            //else if (ret == 2)
                            //{
                            //    ViewBag.message = "" + Db.Title + " is Update successfully";
                            //}
                            else if (ret == 2)
                            {
                                ViewBag.message = "Password Not Change";
                            }
                        }
                        else
                        {
                            // ViewBag.message = "The must be at least 6 and at max {100} characters long";
                        }
                    }
                    else
                    {
                        ViewBag.message = "Confirm Password is not match";
                    }
                    return View("ChangePassword", Db);
                }
                else
                    return Redirect("/Login/Index");


            }
            else
            {
                ViewBag.message = "Password or Confirm Password not empty";
                return View("ChangePassword", Db);
            }
        }
        #endregion



        public IActionResult ResetPassword()
        {
            List<User> cl = new List<User>();
            cl = (from c in _context.Users select c).ToList();
            cl.Insert(0, new User { Id = 0, UserName = "--Select User Name--" });
            ViewBag.cl = cl.ToList();




            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                return View();
            }
            else
                return Redirect("/Login/Index");
        }





        public IActionResult ResetPasswordSave(User Db)
        {
          
            List<User> cl = new List<User>();
            cl = (from c in _context.Users select c).ToList();
            cl.Insert(0, new User { Id = 0, UserName = "--Select User Name--" });
            ViewBag.cl = cl.ToList();

            if (Db.Password != null || Db.ConfirmPassword != null)
            {
                User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
                if (Logins.IsNotNull())
                {
                    Db.Id = Db.Id;
                    if (Db.Password == Db.ConfirmPassword)
                    {
                        if (Db.Password.Length >= 6)
                        {
                            Db.Password = Utility.Security.GetHashString(Db.Password);
                            int ret = _repositoryUser.ResetPassword(Db);

                            if (ret == 1)
                            {

                                ViewBag.message = "Password Changed successfully";

                            }
                            //else if (ret == 2)
                            //{
                            //    ViewBag.message = "" + Db.Title + " is Update successfully";
                            //}
                            else if (ret == 2)
                            {
                                ViewBag.message = "Password Not Change";
                            }
                        }
                        else
                        {
                            // ViewBag.message = "The must be at least 6 and at max {100} characters long";
                        }
                    }
                    else
                    {
                        ViewBag.message = "Confirm Password is not match";
                    }
                    if (ModelState.ContainsKey("{key}"))
                        ModelState["{key}"].Errors.Clear();
                    return View("ResetPassword", Db);
                }
                else
                    return Redirect("/Login/Index");


            }
            else
            {
                ViewBag.message = "Password or Confirm Password not empty";
                return View("ResetPassword", Db);
            }
        }


    }
}
