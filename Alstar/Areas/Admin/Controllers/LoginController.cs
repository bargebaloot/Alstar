using Alstar.da;
using Alstar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alstar.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        daLogin l = new daLogin();
        // GET: Login
        public ActionResult LoginForm()
        {
            ViewBag.error = "";
            return View("Index");
        }
        public ActionResult Close()
        {
            if (Session["user_email"] != null)
            {
                Session["user_email"] = null;
                ViewBag.Message = ".شما با موفقیت از سایت خارج شدید";
                return View("Index");
            }
            else
            {
                return RedirectToAction("LoginForm");
            }
        }
        public ActionResult CheckLogin(mUser pUsers)
        {
            ViewBag.title = "فرم ورود";
            if (pUsers.user_email == "" || pUsers.user_pass == "")
            {
                ViewBag.error = "تکمیل فیلد ها الزامی است !";
                return View("Index", pUsers);
            }
            else
            {
                if (l.CheckLogin(pUsers.user_email, pUsers.user_pass))
                {
                    Session["user_email"] = pUsers.user_email;
                    Session["user_pass"] = pUsers.user_pass;
                    return RedirectToAction("welcome", "Admin");
                }
                else
                {
                    ViewBag.Message = "نام کاربری یا کلمه عبور اشتباه است.";
                    return View("LoginForm");
                }
            }
        }
    }
}