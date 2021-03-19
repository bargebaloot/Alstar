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
    public class mAboutController : Controller
    {
        daAbout a = new daAbout();
        AlstarDb Db = new AlstarDb();
        static int pGet = 15;
        static int pSkip = 0;

        // GET: mLinks
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

        public ActionResult Index(int pId)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            List<mAbout> aAbout = new List<mAbout>();
            mAbout pAbout = new mAbout();
            pAbout.about_us_id = pId;
            aAbout = a.fAboutList(pGet, pSkip, pAbout);
            return View(aAbout);
        }

        [HttpGet]
        public ActionResult AboutGet(int pId = 0)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mAbout vAbout = new mAbout();
            if (pId > 0)
            {
                vAbout.about_us_id = pId;
                vAbout = a.fGetAboutPanel(vAbout);
            }
            else
            {
                vAbout = new mAbout();
            }
            return View("_About", vAbout);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AboutPost(mAbout pAbout)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            if (!(ModelState.IsValid))
            {
                ViewBag.error = "خطا در  اطلاعات ورودی !";
                return View(pAbout);

            }
            else
            {
                if (pAbout.img_file != null && pAbout.img_file.ContentLength > 0)
                {
                    if (pAbout.img_file.ContentLength < 10485760)
                    {
                        Random rnd = new Random();
                        string img = rnd.Next().ToString() + ".jpg";
                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/about/"));
                        pAbout.img_file.SaveAs(Path + img);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pAbout.img_file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pAbout.about_us_img = img;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pAbout);
                    }
                }
                if (pAbout.about_us_id > 0)
                {
                    if (a.fUpdateAbout(pAbout))
                    {
                        return RedirectToAction("index", new { pId = pAbout.about_us_id });
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
                else
                {
                    if (a.InsertAbout(pAbout))
                    {
                        return RedirectToAction("index", new { pId = pAbout.about_us_id });
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
            }
            return View(pAbout);
        }

        public ActionResult RemoveAbout(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mLink vLink = new mLink();
            if (a.fDeleteAbout(pId))
            {
                return Json(pId, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(pId, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult fGetPage(int pPageNum, string pAction)
        {
            pSkip = (pPageNum - 1) * pGet;
            return RedirectToAction(pAction);
        }
    }
}