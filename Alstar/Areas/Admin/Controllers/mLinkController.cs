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
    public class mLinkController : Controller
    {
        daLink l = new daLink();
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
            List<mLink> aLinks = new List<mLink>();
            mLink pLinks = new mLink();
            pLinks.link_status = pId;
            aLinks = l.fLinkList(pGet, pSkip, pLinks);
            ViewBag.pId = pId;
            return View(aLinks);
        }

        [HttpGet]
        public ActionResult LinkGet(int pId=0,int pType=0)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mLink vLink = new mLink();
            vLink.link_status = pType;
            if (pId > 0)
            {
                vLink.link_id = pId;
                vLink = l.fGetLink(vLink);
            }
            else
            {
                vLink = new mLink();
                vLink.link_status = pType;
            }
            return View("_Link",vLink);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult LinkPost(mLink pLink)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            if (!(ModelState.IsValid))
            {
                ViewBag.error = "خطا در  اطلاعات ورودی !";
                return View(pLink);

            }
            else
            {
                if (pLink.img_file != null && pLink.img_file.ContentLength > 0)
                {
                    if (pLink.img_file.ContentLength < 10485760)
                    {
                        Random rnd = new Random();
                        string img = "";
                        
                             img = rnd.Next().ToString() + ".jpg";
                        
                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/link/"));
                        pLink.img_file.SaveAs(Path + img);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pLink.img_file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pLink.link_img = img;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pLink);
                    }
                }
                if (pLink.img_file2 != null && pLink.img_file2.ContentLength > 0)
                {
                    if (pLink.img_file2.ContentLength < 50485760)
                    {
                        Random rnd = new Random();
                        string file = "";

                        file = rnd.Next().ToString() + pLink.img_file2.FileName;

                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/link/"));
                        pLink.img_file2.SaveAs(Path + file);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pLink.img_file2.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pLink.link_file = file;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pLink);
                    }
                }
                if (pLink.link_id > 0)
                {
                    if (l.fUpdateLink(pLink))
                    {
                        return RedirectToAction("index",new { pId=pLink.link_status});
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
                else
                {
                    if (l.InsertLink(pLink))
                    {
                        return RedirectToAction("index", new { pId = pLink.link_status });
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
            }
            return View(pLink);
        }

        public ActionResult RemoveLink(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mLink vLink = new mLink();
            if (l.fDeleteLink(pId))
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
            return RedirectToAction(pAction, new { pId = pType });
        }

    }
}