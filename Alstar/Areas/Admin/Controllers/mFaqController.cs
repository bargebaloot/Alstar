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
    public class mFaqController : Controller
    {
        daFaq c = new daFaq();
        AlstarDb Db = new AlstarDb();
        // GET: mCategory

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
                return RedirectToAction("LoginForm", "Login");
            }
            List<mFaq> aFaq = new List<mFaq>();
            aFaq = c.fFaqList();
            return View(aFaq);
        }

        [HttpGet]
        public ActionResult FaqGet(int pId = 0)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mFaq vFaq = new mFaq();
            if (pId > 0)
            {
                vFaq.faq_id = pId;
                vFaq = c.fGetFaq(vFaq);
            }
            else
            {
                vFaq = new mFaq();
            }
            return View("_Faq", vFaq);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FaqPost(mFaq pFaq)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            if (!(ModelState.IsValid))
            {
                ViewBag.error = "خطا در  اطلاعات ورودی !";
                return View(pFaq);
            }
            else
            {
                if (pFaq.faq_id > 0)
                {
                    if (c.fUpdateFaq(pFaq))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
                else
                {
                    if (c.InsertFaq(pFaq))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
            }
            return View(pFaq);
        }

        public ActionResult RemoveFaq(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login", "LoginForm");
            }
            if (c.fDeleteFaq(pId))
            {
                return Json(pId, JsonRequestBehavior.AllowGet);
            }

            else
            {
                ViewBag.Message = "خطا در انجام عملیات حذف...";
                return Json(pId, JsonRequestBehavior.AllowGet);
            }
        }
    }
}