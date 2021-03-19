using Alstar.da;
using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alstar.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        daLogin l = new daLogin();
        daAdmin a = new daAdmin();
        AlstarDb Db = new AlstarDb();
        // GET: Admin
        public int fUserControl()
        {
            if ((Session["user_email"] != null) && (Session["user_pass"] != null))
            {
                ViewBag.username = Session["user_email"];
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public ActionResult Index()
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login/LoginForm");
            }
            List<mUser> aAdminusers = new List<mUser>();
            aAdminusers = a.fGetAdminusers();
            ViewBag.title = "فهرست کاربران";
            return View(aAdminusers);

        }
        public ActionResult welcome()
        {
            if ((Session != null) && (Session["user_pass"] != null) && Session["user_email"] != null)
            {
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login/LoginForm");
            }
            mUser vAdminUser = new mUser();
            ViewBag.title = "درج کاربر جدید";
            return View(vAdminUser);
        }

        [HttpPost]
        public ActionResult Create(mUser pUsers)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login/LoginForm");
            }
            ViewBag.title = "درج کاربر جدید";
            if (!(ModelState.IsValid))
            {
                ViewBag.error = "خطا در  اطلاعات ورودی !";
                return View(pUsers);

            }
            else
            {
                if (a.InsertUsers(pUsers))
                {
                    return RedirectToAction("index");
                }
                ViewBag.error = "خطا در انجام عملیات درج ! ";
            }
            return View(pUsers);
        }

        public ActionResult checkAdminuserExists(string pEmail)
        {
            mUser vAdminUser = new mUser();
            vAdminUser.user_email = pEmail;
            if (a.checkAdminuserExists(pEmail))
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.DenyGet);

            }
        }
        [HttpGet]
        public ActionResult Edit(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login/LoginForm");
            }
            ViewBag.title = "ویرایش ";
            mUser vUsers = new mUser();
            vUsers.user_id = pId;
            vUsers = a.fGetUsers(vUsers);
            return View(vUsers);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(mUser pUsers)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login/LoginForm");
            }
            ViewBag.title = "ویرایش";
            if (!(ModelState.IsValid))
            {
                ViewBag.error = "خطا در  اطلاعات ورودی !";
                return View(pUsers);

            }
            else
            {
                if (a.fUpdateAdminuser(pUsers))
                {
                    return RedirectToAction("index");
                }
                ViewBag.error = "خطا در انجام عملیات ویرایش ! ";
            }
            return View(pUsers);
        }

        public ActionResult RemoveUsers(long pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login/LoginForm");
            }
            mUser vUsers = new mUser();
            if (a.fDeleteAdminuser(pId))
            {
                return Json(pId, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(pId, JsonRequestBehavior.AllowGet);
            }
        }

    }
}