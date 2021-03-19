//using GemBox.Spreadsheet;
using Alstar.da;
using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alstar.Areas.Admin.Controllers
{
    public class mCustomerController : Controller
    {
        daLogin l = new daLogin();
        daCustomer c = new daCustomer();
        AlstarDb Db = new AlstarDb();
        static int pGet = 20;
        static int pSkip = 0;
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
            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("LoginForm", "Login");
            }
            List<mCustomer> aCustomer = new List<mCustomer>();
            aCustomer = c.fCustomerList(pGet, pSkip);
            return View(aCustomer);

        }


        public ActionResult fGetPage(int pPageNum, string pAction)
        {
            pSkip = (pPageNum - 1) * pGet;
            return RedirectToAction(pAction);
        }
        public JsonResult ExporttoExcel(mHome pHome)
        {
            mHome vHome = new mHome();
             vHome.aCustomer = c.fCustomerList(1000,0);
            // creating data table and adding dummy data  
            DataTable dt = new DataTable();
            dt.Columns.Add("نام");
            dt.Columns.Add("پیام الکترونیک ");
            dt.Columns.Add("شماره تماس");
            dt.Columns.Add("موضوع");
            dt.Columns.Add("متن پیام ");
            foreach (var item in vHome.aCustomer)
            {
                dt.Rows.Add(new object[] { item.user_name,
                    item.user_email, item.user_phone,
                    item.user_subject, item.user_text });
            }
            //SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            //ExcelFile ef = new ExcelFile();
            //ExcelWorksheet ws = ef.Worksheets.Add("Sheet1");
            //// Insert DataTable into an Excel worksheet.
            //ws.InsertDataTable(dt, new InsertDataTableOptions()
            //{
            //    ColumnHeaders = true,
            //    StartRow = 0
            //});
            var vPath = "";
            string vFileName = "اعضای باشگاه مشتریان";
            vFileName = vFileName + ".xlsx";
            vPath = Path.Combine(Server.MapPath("~/upload"), vFileName);
            //ef.Save(vPath);
            ViewBag.FileName = vFileName;
            ViewBag.Path = "/upload/" + vFileName;
            vHome.path = "/upload/" + vFileName;
            return Json(vHome, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveCustomer(int pId)
        {

            if (fUserControl() == 0)
            {
                ViewBag.Message = "شما وارد سایت نشده اید...";
                return RedirectToAction("Login", "LoginForm");
            }
            if (c.fDeleteCustomer(pId))
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