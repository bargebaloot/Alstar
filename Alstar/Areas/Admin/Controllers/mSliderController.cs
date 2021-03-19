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
    public class mSliderController : Controller
    {
        daLogin l = new daLogin();
        daSlider s = new daSlider();
        AlstarDb Db = new AlstarDb();
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
            //if (fUserControl() == 0)
            //{
            //    ViewBag.Message = "شما وارد سایت نشده اید...";
            //    return RedirectToAction("LoginForm", "Login");
            //}
            List<mSlider> aSlider = new List<mSlider>();
            aSlider = s.fSliderList();
            ViewBag.title = "فهرست تصاویر";
            return View(aSlider);

        }

        [HttpGet]
        public ActionResult SliderGet(int pId=0)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mSlider vSlider = new mSlider();
            if (pId > 0)
            {
                vSlider.slider_id = pId;
                vSlider = s.fGetSlider(vSlider);
            }
            else
            {
                vSlider = new mSlider();
            }
            return View("_Slider",vSlider);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SliderPost(mSlider pSlider)
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
                return View(pSlider);

            }
            else
            {
                if (pSlider.img_file != null && pSlider.img_file.ContentLength > 0)
                {
                    if (pSlider.img_file.ContentLength < 10485760)
                    {
                        Random rnd = new Random();
                        string img = rnd.Next().ToString() + ".jpeg";
                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/slider/"));
                        pSlider.img_file.SaveAs(Path + img);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pSlider.img_file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pSlider.slider_img = img;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pSlider);
                    }
                }
                if (pSlider.slider_id > 0)
                {
                    if (s.fUpdateSlider(pSlider))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
                else
                {
                    if (s.InsertSlider(pSlider))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
            }
            return View(pSlider);
        }

        public ActionResult RemoveSlider(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login", "LoginForm");
            }
            mSlider vSlider = new mSlider();
            if (s.fDeleteSlider(pId))
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