using Alstar.da;
using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alstar.Areas.Controllers
{
    public class mVideoController : Controller
    {
        static int pGet = 15;
        static int pSkip = 0;
        daLogin l = new daLogin();
        daVideo v = new daVideo();
        AlstarDb Db = new AlstarDb();
        // GET: mVideo
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
            List<mVideo> aVideo = new List<mVideo>();
            aVideo = v.fVidoeList(pGet, pSkip);
            ViewBag.title = "فهرست  ویدئو ها";
            return View(aVideo);
        }

        [HttpGet]
        public ActionResult VideoGet(int pId = 0)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mVideo vVideo = new mVideo();
            if (pId > 0)
            {
                vVideo.video_id = pId;
                vVideo = v.fGetVideo(vVideo);
            }
            else
            {
                vVideo = new mVideo();
            }
            return View("_Video", vVideo);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VideoPost(mVideo pVideo)
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
                return View(pVideo);

            }
            else
            {
                if (pVideo.video_id > 0)
                {
                    if (v.fUpdateVideo(pVideo))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
                else
                {
                    if (v.InsertVideo(pVideo))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
            }
            return View(pVideo);
        }

        public ActionResult RemoveVideo(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login", "LoginForm");
            }
            mVideo vVideo = new mVideo();
            if (v.fDeleteVideo(pId))
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