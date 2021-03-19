using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alstar.Areas.Ar.Controllers
{
    public class HomeController : Controller
    {
        daHome h = new daHome();
        daCategory c = new daCategory();
        AlstarDb Db = new AlstarDb();

        public ActionResult Index()
        {
            mHome vHome = new mHome();
            mCategory vCat = new mCategory();
            vCat.category_type = 3;
            List<mCategory> aCat = new List<mCategory>();
            aCat = c.fCategoryList(12, 0, vCat.category_type);
            vHome = h.fGetHomeContent();
            if (vHome == null)
            {
                vHome = new mHome();
            }
            vHome.aCategoryGallery = aCat;
            return View(vHome);
        }
        public ActionResult Search(string pSearch)
        {
            mHome vHome = new mHome();
            vHome = h.fGetSearch(pSearch);
            if (vHome == null)
            {
                vHome = new mHome();
            }
            return View(vHome);
        }
        public ActionResult SearchCat(mHome pHome)

        {
            mHome vHome = new mHome();
            vHome = h.fGetSearch(pHome.pSearch);
            if (vHome == null)
            {
                vHome = new mHome();
            }
            return PartialView("_Search", vHome);
        }


    }
}