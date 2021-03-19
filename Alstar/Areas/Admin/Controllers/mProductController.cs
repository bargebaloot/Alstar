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
    public class mInsuranceController : Controller
    {
        daLogin l = new daLogin();
        daProduct p = new daProduct();
        AlstarDb Db = new AlstarDb();
        static int pGet = 15;
        static int pSkip = 0;
        // GET: mInsurances

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
            List<mProduct> aInsurance = new List<mProduct>();
            aInsurance = p.fInsList(pGet, pSkip,0);
            ViewBag.title = "فهرست محصولات";
            return View(aInsurance);
        }

        [HttpGet]
        public ActionResult InsuranceGet(int pId=0)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            mProduct vInsurance = new mProduct();
            if (pId > 0)
            {
                vInsurance.product_id = pId;
                vInsurance = p.fGetIns(vInsurance);
            }
            else
            {
                vInsurance = new mProduct();
            }
            return View("_Insurance",vInsurance);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InsurancePost(mProduct pInsurance)
        {
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            if (!(ModelState.IsValid))
            {
                ViewBag.error = "خطا در  اطلاعات ورودی !";
                return View(pInsurance);

            }
            else
            {

                if (pInsurance.img_file1 != null && pInsurance.img_file1.ContentLength > 0)
                {
                    if (pInsurance.img_file1.ContentLength < 10485760)
                    {
                        Random rnd = new Random();
                        string img = rnd.Next().ToString() + ".jpg";
                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/Product/"));
                        pInsurance.img_file1.SaveAs(Path + img);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pInsurance.img_file1.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pInsurance.product_img1 = img;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pInsurance);
                    }
                }
                if (pInsurance.img_file2 != null && pInsurance.img_file2.ContentLength > 0)
                {
                    if (pInsurance.img_file2.ContentLength < 10485760)
                    {
                        Random rnd = new Random();
                        string img = rnd.Next().ToString() + ".jpg";
                        string Path = System.IO.Path.Combine(Server.MapPath("~/images/Insurance/"));
                        pInsurance.img_file2.SaveAs(Path + img);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pInsurance.img_file2.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            pInsurance.product_img1 = img;
                        }
                    }
                    else
                    {
                        ViewBag.error = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                        return View(pInsurance);
                    }
                }
                if (pInsurance.product_id > 0)
                {
                    if (p.fUpdateIns(pInsurance))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";
                }
                else
                {
                    if (p.InsertIns(pInsurance))
                    {
                        return RedirectToAction("index");
                    }
                    ViewBag.error = "خطا در انجام عملیات  ! ";         
                }
            }
            return View(pInsurance);
        }

        public ActionResult RemoveInsurance(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login", "LoginForm");
            }
            if (p.fDeleteIns(pId))
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