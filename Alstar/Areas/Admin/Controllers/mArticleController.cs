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
    public class mArticleController : Controller
    {

        static int pGet = 15;
        static int pSkip = 0;
        daLogin l = new daLogin();
        daArticle a = new daArticle();
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
        public ActionResult Index(int pId)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");

            }
            mHome vHome = new mHome();
            mArticle pArticle = new mArticle();
            pArticle.article_type = pId;
            List<mArticle> aArticles = new List<mArticle>();
            aArticles = a.fArticlesList(pGet, pSkip, pArticle, vHome);
            ViewBag.pId = pId;
            return View(aArticles);
        }

        [HttpGet]
        public ActionResult ArticleGet(int pId = 0,int pType=0)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mArticle vArticles = new mArticle();
            if (pId > 0)
            {
                vArticles.article_id = pId;
                vArticles = a.fGetArticles(vArticles);
            }
            else
            {
                vArticles = new mArticle();
            }
            vArticles.article_type = pType;
            return View("_Article", vArticles);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ArticlePost(mArticle pArticles)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            if (!(ModelState.IsValid))
            {
                ViewBag.error = "خطا در  اطلاعات ورودی !";
                return View(pArticles);

            }
            else
            {
                if ((pArticles.img_file != null) && (pArticles.img_file.ContentLength > 0))
                {
                    if (pArticles.img_file.ContentLength < 10485760)
                    {
                        Random rnd = new Random();
                        string img = rnd.Next().ToString() + ".jpg";
                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/articles/"));
                        pArticles.img_file.SaveAs(Path + img);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pArticles.img_file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pArticles.article_img = img;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pArticles);
                    }
                }
                if ((pArticles.img_file2 != null) && (pArticles.img_file2.ContentLength > 0))
                {
                    if (pArticles.img_file2.ContentLength < 50485760)
                    {
                        Random rnd = new Random();
                        string img = rnd.Next().ToString() + ".zip";
                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/articles/"));
                        pArticles.img_file2.SaveAs(Path + img);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pArticles.img_file2.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pArticles.article_file = img;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pArticles);
                    }
                }
                if (pArticles.article_id > 0)
                {
                    if (a.fUpdateArticles(pArticles))
                    {
                        return RedirectToAction("index",new {pId = pArticles.article_type });
                    }
                }
                else
                {
                    if (a.InsertArticles(pArticles))
                    {
                        return RedirectToAction("index", new { pId = pArticles.article_type });
                    }
                }
                ViewBag.error = "خطا در انجام عملیات  ! ";
            }
            return View(pArticles);
        }

        public ActionResult RemoveArticles(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login", "LoginForm");
            }
            mArticle vArticles = new mArticle();
            if (a.fDeleteArticles(pId))
            {
                return Json(pId, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(pId, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult fGetPage(int pPageNum, string pAction, int pType)
        {
            pSkip = (pPageNum - 1) * pGet;
            return RedirectToAction(pAction, new {pId = pType });
        }


    }
}