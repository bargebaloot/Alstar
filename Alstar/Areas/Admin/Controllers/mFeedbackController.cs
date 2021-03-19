using Alstar.da;
using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alstar.Areas.Admin.Controllers
{
    public class mFeedbackController : Controller
    {
        daLogin l = new daLogin();
        daFeedback f = new daFeedback();
        AlstarDb Db = new AlstarDb();
        static int pGet = 15;
        static int pSkip = 0;
        // GET: mSlide
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
            List<mFeedback> aFeedback = new List<mFeedback>();
            aFeedback = f.fFeedbackList(pGet,pSkip);
            return View(aFeedback);

        }

        [HttpGet]
        public ActionResult FeedbackGet(int pId = 0)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mFeedback vFeedback = new mFeedback();
            if (pId > 0)
            {
                vFeedback.cust_feedback_id = pId;
                vFeedback = f.fGetFeedback(vFeedback);
            }
            else
            {
                vFeedback = new mFeedback();
            }
            return View("_Feedback", vFeedback);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FeedbackPost(mFeedback pFeedback)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            ViewBag.title = "ویرایش فایل";
            if (!(ModelState.IsValid))
            {
                ViewBag.error = "خطا در  اطلاعات ورودی !";
                return View(pFeedback);

            }
            else
            {
                if (pFeedback.img_file != null && pFeedback.img_file.ContentLength > 0)
                {
                    if (pFeedback.img_file.ContentLength < 10485760)
                    {
                        Random rnd = new Random();
                        string img = rnd.Next().ToString() + ".jpeg";
                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/feedback/"));
                        pFeedback.img_file.SaveAs(Path + img);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pFeedback.img_file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pFeedback.cust_feedback_img = img;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pFeedback);
                    }
                }
                if (pFeedback.cust_feedback_id > 0)
                {
                    if (f.fUpdateFeedback(pFeedback))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
                else
                {
                    if (f.InsertFeedback(pFeedback))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
            }
            return View(pFeedback);
        }

        public ActionResult RemoveFeedback(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login", "LoginForm");
            }
            mFeedback vFeedback = new mFeedback();
            if (f.fDeleteFeedback(pId))
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