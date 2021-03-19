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
    public class mGalleryController : Controller
    {
            daLogin l = new daLogin();
            daGallery g = new daGallery();
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
            public ActionResult Index(int pId)
            {
                if (fUserControl() == 0)
                {
                    ViewBag.Message = "شما وارد سایت نشده اید...";
                    return RedirectToAction("LoginForm", "Login");

                }
             List<mGallery> aGallery = new List<mGallery>();
                aGallery = g.fGalleryList(pGet, pSkip,pId);
                ViewBag.title = "فهرست تصاویر";
            ViewBag.pId = pId;
                return View(aGallery);
            }

            [HttpGet]
            public ActionResult GalleryGet(int pId = 0,int pType=0)
            {

                if (fUserControl() == 0)
                {
                    ViewBag.Message = "شما وارد سایت نشده اید...";
                    return RedirectToAction("LoginForm", "Login");
                }
                mGallery vGallery = new mGallery();
                if (pId > 0)
                {
                vGallery.gallery_id = pId;
                vGallery = g.fGetGallery(vGallery);
                }
                else
                {
                vGallery = new mGallery();
                }
            vGallery.gallery_cat = pType;
                return View("_Gallery", vGallery);
            }

            [HttpPost, ValidateInput(false)]
            public ActionResult GalleryPost(mGallery pGallery)
            {
                if (fUserControl() == 0)
                {
                    ViewBag.Message = "شما وارد سایت نشده اید...";
                    return RedirectToAction("LoginForm", "Login");
                }
                if (!(ModelState.IsValid))
                {
                    ViewBag.error = "خطا در  اطلاعات ورودی !";
                    return View(pGallery);
                }
                else
                {
                    if (pGallery.img_file != null && pGallery.img_file.ContentLength > 0)
                    {
                        if (pGallery.img_file.ContentLength < 10485760)
                        {
                            Random rnd = new Random();
                            string img = rnd.Next().ToString() + ".jpg";
                            string Path = System.IO.Path.Combine(Server.MapPath("~/images/gallery/"));
                        pGallery.img_file.SaveAs(Path + img);
                            using (MemoryStream ms = new MemoryStream())
                            {
                            pGallery.img_file.InputStream.CopyTo(ms);
                                byte[] array = ms.GetBuffer();
                            pGallery.gallery_image = img;
                            }
                        }
                        else
                        {
                            ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                            return View(pGallery);
                        }
                    }
                    if (pGallery.gallery_id > 0)
                    {
                        if (g.fUpdateGallery(pGallery))
                       { 
                            return RedirectToAction("index",new {pId = pGallery.gallery_cat });
                        }
                        ViewBag.error = "خطا در انجام عملیات  ! ";

                    }
                    else
                    {
                        if (g.InsertGallery(pGallery))
                        {
                            return RedirectToAction("index", new { pId = pGallery.gallery_cat });
                        }
                        ViewBag.error = "خطا در انجام عملیات  ! ";

                    }
                }
                return View(pGallery);
            }

            public ActionResult RemoveGallery(int pId)
            {

                if (fUserControl() == 0)
                {
                    ViewBag.Message = "شما وارد سایت نشده اید...";
                    return RedirectToAction("Login", "LoginForm");
                }
                mNews vNews = new mNews();
                if (g.fDeleteGallery(pId))
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