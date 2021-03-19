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
    public class mCenterController : Controller
    {
        static int pGet = 15;
        static int pSkip = 0;
        daLogin l = new daLogin();
        daCenter a = new daCenter();
        AlstarDb Db = new AlstarDb();
        // GET: Articles
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
            mHome vHome = new mHome();
            mCenter pCenter = new mCenter();
            List<mCenter> aCenter = new List<mCenter>();
            aCenter = a.fCenterList(pGet, pSkip, pCenter, vHome);
            return View(aCenter);
        }

        [HttpGet]
        public ActionResult CenterGet(int pId = 0)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mCenter vCenter = new mCenter();
            if (pId > 0)
            {
                vCenter.center_id = pId;
                vCenter = a.fGetCenter(vCenter);
            }
            else
            {
                vCenter = new mCenter();
            }
            return View("_Center", vCenter);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CenterPost(mCenter pCenters)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            if (!(ModelState.IsValid))
            {
                ViewBag.error = "خطا در  اطلاعات ورودی !";
                return View(pCenters);

            }
            else
            {
                if ((pCenters.img_file != null) && (pCenters.img_file.ContentLength > 0))
                {
                    if (pCenters.img_file.ContentLength < 10485760)
                    {
                        Random rnd = new Random();
                        string img = rnd.Next().ToString() + ".jpg";
                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/center/"));
                        pCenters.img_file.SaveAs(Path + img);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pCenters.img_file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pCenters.center_img = img;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pCenters);
                    }
                }
                if (pCenters.center_id > 0)
                {
                    if (a.fUpdateCenter(pCenters))
                    {
                        return RedirectToAction("index", new { pId = pCenters.center_cat });
                    }
                }
                else
                {
                    if (a.InsertCenter(pCenters))
                    {
                        return RedirectToAction("index", new { pId = pCenters.center_cat });
                    }
                }
                ViewBag.error = "خطا در انجام عملیات  ! ";
            }
            return View(pCenters);
        }

        public ActionResult RemoveCenter(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login", "LoginForm");
            }
            mCenter vCenter = new mCenter();
            if (a.fDeleteCenter(pId))
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