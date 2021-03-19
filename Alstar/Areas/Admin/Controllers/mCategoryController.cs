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
    public class mCategoryController : Controller
    {
        daCategory c = new daCategory();
        AlstarDb Db = new AlstarDb();
        static int pGet = 20;
        static int pSkip = 0;
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
        public ActionResult Index(int? pId=0)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            List<mCategory> aCategory = new List<mCategory>();
            aCategory = c.fCategoryList(pGet, pSkip, pId);
            ViewBag.title = "فهرست  مجموعه ها";
            ViewBag.pId = pId;
            return View(aCategory);
        }

        [HttpGet]
        public ActionResult CategoryGet(int pId=0,int pType=0)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mCategory vCat = new mCategory();
            if (pId > 0)
            {
                vCat.category_id = pId;
                vCat = c.fGetCategory(vCat);
            }
            else
            {
                vCat = new mCategory();
            }
            vCat.category_type = pType;
            return View("_Category",vCat);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CategoryPost(mCategory pCat)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            if (!(ModelState.IsValid))
            {
                ViewBag.error = "خطا در  اطلاعات ورودی !";
                return View(pCat);
            }
            else
            {
                if (pCat.img_file != null && pCat.img_file.ContentLength > 0)
                {
                    if (pCat.img_file.ContentLength < 10485760)
                    {
                        Random rnd = new Random();
                        string img = rnd.Next().ToString() + ".jpg";
                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/category/"));
                        pCat.img_file.SaveAs(Path + img);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pCat.img_file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pCat.category_img = img;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pCat);
                    }
                }
                if (pCat.category_id > 0)
                {
                    if (c.fUpdateCategory(pCat))
                    {
                        return RedirectToAction("index", new { pId = pCat.category_type });
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
                else
                {
                    if (c.InsertCategory(pCat))
                    {
                        return RedirectToAction("index", new {pId = pCat.category_type });
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
            }
            return View(pCat);
        }

        public ActionResult RemoveCategory(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login", "LoginForm");
            }
            if (c.fDeleteCategory(pId))
            {
                return Json(pId, JsonRequestBehavior.AllowGet);
            }

            else
            {
                ViewBag.Message = "خطا در انجام عملیات حذف...";
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