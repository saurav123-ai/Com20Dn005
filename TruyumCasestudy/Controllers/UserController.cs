using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Truyumcasestudy.Models;

namespace Truyumcasestudy.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        truyum db = new truyum();

        // GET: User
        public ActionResult UserRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserRegister(FormCollection frm)
        {
            user user = new user();
            if (frm.Get("password").Equals(frm.Get("RePass")))
            {
                user.user_id = int.Parse(frm.Get("user_id"));
                user.username = frm.Get("username");
                user.Password = frm.Get("password");
                db.users.Add(user);
                db.SaveChanges();
                TempData.Add("username", user.username);
                ViewBag.message = "Welcome to Truyum " + user.username;
                return RedirectToAction("Loginuser", "menus");

            }
            return View();
        }

        public ActionResult Login()
        {
            //user myUser = new user();
            //if (TempData["username"] != null)
            //    myUser.username = TempData["username"].ToString();
            return View();//myUser);
        }
        [HttpPost]
        public ActionResult Login(user user)
        {
            if (ModelState.IsValid)
            {

               int myUser = db.users.Where(u => u.username == user.username && u.Password == user.Password && u.IsAdmin == true).Count();
                if (myUser== 1)
                {

                    return RedirectToAction("Index", "menus");
                }
                //if(myUser!=null)
                //{
                //    Session["userid"] = myUser.user_id.ToString();
                //    return RedirectToAction("Index")
                //}


                else
                {
                    ViewBag.message = "Invalid Username or password";
                    return Content(ViewBag.message);
                }
            }
            return View();
        }
        public ActionResult Loginuser()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Loginuser(Models.user model)
        {
            using(var context=new truyum())
            {
                bool isValid = context.users.Any(x => x.username == model.username &&  x.Password == model.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.username, false);
                    return RedirectToAction("menu","menus");
                }
                ModelState.AddModelError("", "Invalid username and Password");
                return View();
            }
        }

    }
}