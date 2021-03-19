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
    public class mBlogController : Controller
    {
        daLogin l = new daLogin();
        daBlog n = new daBlog();
        AlstarDb Db = new AlstarDb();
        static int pGet = 15;
        static int pSkip = 0;
        // GET: mNews
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
            List<mNews> aNews = new List<mNews>();
            aNews = n.fNewsList(pGet, pSkip);
            ViewBag.title = "فهرست خبرها";
            return View(aNews);
        }

        [HttpGet]
        public ActionResult BlogGet(int pId=0)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mNews vNews = new mNews();
            if (pId > 0)
            {
                vNews.news_id = pId;
                vNews = n.fGetNews(vNews);
            }
            else
            {
                vNews = new mNews();
            }
            return View("_Blog",vNews);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BlogPost(mNews pNews)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            if (!(ModelState.IsValid))
            {
                ViewBag.error = "خطا در  اطلاعات ورودی !";
                return View(pNews);
            }
            else
            {
                if (pNews.img_file != null && pNews.img_file.ContentLength > 0)
                {
                    if (pNews.img_file.ContentLength < 10485760)
                    {
                        Random rnd = new Random();
                        string img = rnd.Next().ToString() + ".jpg";
                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/blog/"));
                        pNews.img_file.SaveAs(Path + img);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pNews.img_file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pNews.news_image = img;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pNews);
                    }
                }
                if (pNews.news_id > 0)
                {
                    if (n.fUpdateNews(pNews))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";

                }
                else
                {
                    if (n.InsertNews(pNews))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";

                }
            }
            return View(pNews);
        }

        public ActionResult RemoveNews(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login", "LoginForm");
            }
            mNews vNews = new mNews();
            if (n.fDeleteNews(pId))
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