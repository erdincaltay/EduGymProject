using EduGymProject.Models;
using EduGymProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EduGymProject.Controllers
{
    [AllowAnonymous]
    public class UsersController : Controller
    {
        edugymEntities1 db = new edugymEntities1();
        // GET: Users
        public ActionResult Login()
        {
            LoginModel loginmodel = new LoginModel();
            return View(loginmodel);
        }

        [HttpPost]
        
        public ActionResult Login(LoginModel objLoginModel)
        {
            if (ModelState.IsValid)
            {
                var logUser = db.User.FirstOrDefault(x => x.UserName == objLoginModel.UserName.ToLower() && x.Password == objLoginModel.Password);
                if (logUser != null)
                {
                    FormsAuthentication.SetAuthCookie(logUser.UserName, false);
                    Session["UserName"] = objLoginModel.UserName.ToLower();
                    Session["UserId"] = logUser.UserId;
                    ViewBag.userid = Session["UserId"];

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Error", "Geçersiz kullanıcı adı veya şifre!");
                    return View();
                }

            }
            return View();
        }

        public ActionResult Register()
        {
                UserModel usermodel = new UserModel();
                return View(usermodel);
        }

        [HttpPost]
        public ActionResult Register(UserModel usermodel)

        {
            if (ModelState.IsValid)
            {
                if (!db.User.Any(m => m.UserName == usermodel.UserName.ToLower() || m.Email == usermodel.Email.ToLower()))
                {
                    User usermodelclass = new Models.EntityFramework.User();

                    usermodelclass.UserName = usermodel.UserName.ToLower();
                    usermodelclass.Email = usermodel.Email.ToLower();
                    usermodelclass.Password = usermodel.Password;
                    usermodelclass.CreatedOn = DateTime.Now;
                    usermodelclass.RolId = 1;

                    db.User.Add(usermodelclass);
                    db.SaveChanges();
                    usermodel = new UserModel();

                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    ModelState.AddModelError("Error", "Email veya kullanıcı adı alınmış");
                    return View();
                }

            }
            return View();


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}