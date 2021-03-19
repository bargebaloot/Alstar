using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alstar.Areas.Ar.Controllers
{
    public class CategoryController : Controller
    {
        daAbout a = new daAbout();
        daCategory c = new daCategory();
        daCenter ce = new daCenter();
        daProduct p = new daProduct();
        daLink l = new daLink();
        daFaq f = new daFaq();
        daArticle ar = new daArticle();
        daGallery g = new daGallery();
        daContact co = new daContact();
        daFeedback fe = new daFeedback();
        daVideo v = new daVideo();
        daBlog b = new daBlog();
        static int pGet = 12;
        static int pSkip = 0;
        // GET: Category

        public ActionResult About()
        {
            try
            {
                mAbout vAbout = new mAbout();
                mAbout vAbout2 = new mAbout();
                mHome vHome = new mHome();

                vAbout.about_us_type = 1;
                vAbout2.about_us_type = 2;
                vAbout = a.fGetAbout(vAbout);
                vAbout2 = a.fGetAbout(vAbout2);
                vHome.About = vAbout;
                vHome.About2 = vAbout2;
                return View(vHome);
            }
            catch
            {
                return RedirectToAction("_Error", "Home");
            }
        }
        public ActionResult Faq()
        {
            List<mFaq> aFaq = new List<mFaq>();
            mHome vHome = new mHome();
            aFaq = f.fFaqList();
            if (aFaq != null && aFaq.Count > 0)
            {
                vHome.aFaq = aFaq;
            }
            else
            {
                vHome.aFaq = new List<mFaq>();
            }
            return View(vHome);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ContactPost(mContact pContact)
        {
            if (pContact.img_file2 != null && pContact.img_file2.ContentLength > 0)
            {
                if (pContact.img_file2.ContentLength < 50485760)
                {
                    Random rnd = new Random();
                    string file = "";

                    file = rnd.Next().ToString() + pContact.img_file2.FileName;

                    string Path = System.IO.Path.Combine(Server.MapPath("~/images/contact/"));
                    pContact.img_file2.SaveAs(Path + file);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pContact.img_file2.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        pContact.user_file = file;
                    }
                }
                else
                {
                    pContact.pError = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                    goto _exit_line;
                }
            }
            pContact.contact_type = 1;
            var vResult = co.InsertContact(pContact);
            if (vResult == true)
            {
                pContact.pSuccess = "نظر شما با موفقیت ثبت شد.";
            }
            else
            {
                pContact.pError = "ثبت نظر شما با مشکل مواجه شده است.";
            }
        _exit_line:
            return View("Contact", pContact);
        }
        public ActionResult FeedbackBoxPost(mContact pContact)
        {
            if (pContact.img_file2 != null && pContact.img_file2.ContentLength > 0)
            {
                if (pContact.img_file2.ContentLength < 50485760)
                {
                    Random rnd = new Random();
                    string file = "";

                    file = rnd.Next().ToString() + pContact.img_file2.FileName;

                    string Path = System.IO.Path.Combine(Server.MapPath("~/images/contact/"));
                    pContact.img_file2.SaveAs(Path + file);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pContact.img_file2.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        pContact.user_file = file;
                    }
                }
                else
                {
                    pContact.pError = "حد اکثر اندازه فایل را رعایت نکرده اید !";
                    goto _exit_line;
                }
            }
            pContact.contact_type = 2;
            var vResult = co.InsertContact(pContact);
            if (vResult == true)
            {
                pContact.pSuccess = "نظر شما با موفقیت ثبت شد.";
            }
            else
            {
                pContact.pError = "ثبت نظر شما با مشکل مواجه شده است.";
            }
        _exit_line:
            return View("FeedBackBox", pContact);
        }

        public ActionResult Insurance(int pId)
        {
            mHome vHome = new mHome();
            vHome.aCategory = c.fCategoryInsurance(pId);
            return View(vHome);
        }
        public ActionResult Certificate()
        {
            mLink vLink = new mLink();
            List<mLink> aLink = new List<mLink>();
            vLink.link_status = 1;
            mHome vHome = new mHome();
            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            aLink = l.fLinkList(pGet, pSkip, vLink);
            if (aLink == null && aLink.Count == 0)
            {
                aLink = new List<mLink>();
            }
            else
            {
                vHome.aLink = aLink;
            }
            return View(vHome);
        }
        public ActionResult fGetPage(int pPageNum, string pAction)
        {
            pSkip = (pPageNum - 1) * pGet;
            return RedirectToAction(pAction);
        }
        public ActionResult Instructions()
        {
            mHome vHome = new mHome();
            mLink vLink = new mLink();

            mArticle vArticle = new mArticle();
            vArticle.article_type = 1;
            List<mArticle> aArticle = new List<mArticle>();

            vLink.link_status = 1;
            List<mLink> aLink = new List<mLink>();
            aLink = l.fLinkList(pGet, pSkip, vLink);
            aArticle = ar.fArticlesList(pGet, pSkip, vArticle, vHome);

            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            if (aLink != null && aLink.Count > 0)
            {
                vHome.aLink = aLink;
            }
            else
            {
                vHome.aLink = new List<mLink>();
            }
            if (aArticle != null && aArticle.Count > 0)
            {
                vHome.aArticles = aArticle;
            }
            else
            {
                vHome.aArticles = new List<mArticle>();
            }
            return View(vHome);

        }
        public ActionResult Articles()
        {
            mHome vHome = new mHome();
            mLink vLink = new mLink();
            mArticle vArticle = new mArticle();
            vArticle.article_type = 1;
            vLink.link_status = 4;
            List<mArticle> aArticle = new List<mArticle>();
            List<mNews> aNews = new List<mNews>();
            List<mLink> aLinks = new List<mLink>();
            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            aArticle = ar.fArticlesList(pGet, pSkip, vArticle, vHome);
            aNews = b.fNewsList(4, 0);
            aLinks = l.fLinkList(12, 0, vLink);

            if (aArticle != null && aArticle.Count > 0)
            {
                vHome.aArticles = aArticle;
            }
            else
            {
                vHome.aArticles = new List<mArticle>();
            }
            if (aNews != null && aNews.Count > 0)
            {
                vHome.aNews = aNews;
            }
            else
            {
                vHome.aNews = new List<mNews>();
            }
            if (aLinks != null && aLinks.Count > 0)
            {
                vHome.aLink = aLinks;
            }
            else
            {
                vHome.aLink = new List<mLink>();
            }
            return View(vHome);

        }
        public ActionResult Blog()
        {
            mHome vHome = new mHome();
            mArticle vArticle = new mArticle();
            vArticle.article_type = 2;
            List<mArticle> aArticle = new List<mArticle>();
            mCategory vCat = new mCategory();
            vCat.category_type = 1;
            List<mCategory> aCat = new List<mCategory>();

            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            aArticle = ar.fArticlesList(pGet, pSkip, vArticle, vHome);
            aCat = c.fCategoryList(12, 0, vCat.category_type);

            if (aArticle != null && aArticle.Count > 0)
            {
                vHome.aArticles = aArticle;
            }
            else
            {
                vHome.aArticles = new List<mArticle>();
            }
            if (aCat != null && aCat.Count > 0)
            {
                vHome.aCategory = aCat;
            }
            else
            {
                vHome.aCategory = new List<mCategory>();
            }
            return View(vHome);

        }
        public ActionResult Centers(int? pId)
        {
            mHome vHome = new mHome();
            mCategory vCat = new mCategory();
            mCenter pCenter = new mCenter();
            vCat.category_type = 2;
            pCenter.center_cat = pId;
            List<mCategory> aCat = new List<mCategory>();
            List<mCenter> aCenter = new List<mCenter>();
            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            aCenter = ce.fCenterList(pGet, pSkip, pCenter, vHome);
            aCat = c.fCategoryList(20, 0, vCat.category_type);
            if (aCenter != null && aCenter.Count > 0)
            {
                vHome.aCenter = aCenter;
            }
            else
            {
                vHome.aCenter = new List<mCenter>();
            }
            if (aCat != null && aCat.Count > 0)
            {
                vHome.aCategory = aCat;
            }
            else
            {
                vHome.aCategory = new List<mCategory>();
            }
            return View(vHome);

        }
        public ActionResult News()
        {
            mHome vHome = new mHome();
            mNews vNews = new mNews();
            mLink vLink = new mLink();
            vLink.link_status = 2;
            List<mLink> aLinks = new List<mLink>();
            List<mProduct> aProducts = new List<mProduct>();

            List<mNews> aNews = new List<mNews>();
            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }

            aNews = b.fNewsList(pGet, pSkip);
            aLinks = l.fLinkList(12, 0, vLink);

            if (aNews != null && aNews.Count > 0)
            {
                vHome.aNews = aNews;
            }
            else
            {
                vHome.aNews = new List<mNews>();
            }
            if (aLinks != null && aLinks.Count > 0)
            {
                vHome.aLink = aLinks;
            }
            else
            {
                vHome.aLink = new List<mLink>();
            }
            if (aProducts != null && aProducts.Count > 0)
            {
                vHome.aProducts = aProducts;
            }
            else
            {
                vHome.aProducts = new List<mProduct>();
            }
            return View(vHome);
        }
        public ActionResult Gallery(int? pId)
        {
            mHome vHome = new mHome();
            List<mGallery> aGallery = new List<mGallery>();
            mCategory vCat = new mCategory();
            vCat.category_type = 2;
            List<mCategory> aCat = new List<mCategory>();
            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            aGallery = g.fGalleryList(pGet, pSkip, pId);
            aCat = c.fCategoryList(12, 0, vCat.category_type);

            if (aGallery != null && aGallery.Count > 0)
            {
                vHome.aGallery = aGallery;
            }
            else
            {
                vHome.aGallery = new List<mGallery>();
            }
            if (aCat != null && aCat.Count > 0)
            {
                vHome.aCategory = aCat;
            }
            else
            {
                vHome.aCategory = new List<mCategory>();
            }
            return View(vHome);
        }
        public ActionResult customers()
        {
            mLink vLink = new mLink();
            List<mLink> aLink = new List<mLink>();
            vLink.link_status = 5;
            mHome vHome = new mHome();
            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            aLink = l.fLinkList(pGet, pSkip, vLink);
            if (aLink == null && aLink.Count == 0)
            {
                aLink = new List<mLink>();
            }
            else
            {
                vHome.aLink = aLink;
            }
            return View(vHome);
        }
        public ActionResult Contact()
        {
            mContact pContact = new mContact();
            return View(pContact);
        }
        public ActionResult events()
        {
            int pId = 2;
            mHome vHome = new mHome();
            List<mGallery> aGallery = new List<mGallery>();
            mCategory vCat = new mCategory();
            vCat.category_type = 3;
            List<mCategory> aCat = new List<mCategory>();
            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            aGallery = g.fGalleryList(pGet, pSkip, pId);
            aCat = c.fCategoryList(12, 0, vCat.category_type);

            if (aGallery != null && aGallery.Count > 0)
            {
                vHome.aGallery = aGallery;
            }
            else
            {
                vHome.aGallery = new List<mGallery>();
            }
            if (aCat != null && aCat.Count > 0)
            {
                vHome.aCategory = aCat;
            }
            else
            {
                vHome.aCategory = new List<mCategory>();
            }
            return View(vHome);
        }
        public ActionResult feedbacks()
        {
            mHome vHome = new mHome();
            List<mFeedback> aFeedback = new List<mFeedback>();
            aFeedback = fe.fFeedbackList(pGet, pSkip);
            if (aFeedback != null && aFeedback.Count > 0)
            {
                vHome.aFeedback = aFeedback;
            }
            else
            {
                vHome.aFeedback = new List<mFeedback>();
            }
            return View(vHome);
        }
        public ActionResult FeedbackBox()
        {
            mContact pContact = new mContact();
            return View(pContact);
        }
        public ActionResult forms()
        {
            mLink vLink = new mLink();
            List<mLink> aLink = new List<mLink>();
            vLink.link_status = 3;
            mHome vHome = new mHome();
            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            aLink = l.fLinkList(pGet, pSkip, vLink);
            if (aLink == null && aLink.Count == 0)
            {
                aLink = new List<mLink>();
            }
            else
            {
                vHome.aLink = aLink;
            }
            return View(vHome);
        }
        public ActionResult manager()
        {
            try
            {
                mAbout vAbout = new mAbout();
                mAbout vAbout2 = new mAbout();
                mHome vHome = new mHome();

                vAbout.about_us_type = 3;
                vAbout2.about_us_type = 4;
                vAbout = a.fGetAbout(vAbout);
                vAbout2 = a.fGetAbout(vAbout2);
                vHome.About = vAbout;
                vHome.About2 = vAbout2;
                int pId = 3;
                List<mGallery> aGallery = new List<mGallery>();
                aGallery = g.fGalleryList(pGet, pSkip, pId);
                if (aGallery != null)
                {
                    vHome.aGallery = aGallery;
                }
                return View(vHome);
            }
            catch
            {
                return RedirectToAction("_Error", "Home");
            }
        }
        public ActionResult report()
        {
            try
            {
                mAbout vAbout = new mAbout();
                List<mAbout> aAbout = new List<mAbout>();
                vAbout.about_us_type = 6;
                mHome vHome = new mHome();
                vAbout = a.fGetAbout(vAbout);
                if (vAbout == null)
                {
                    vAbout = new mAbout();
                }
                else
                {
                    vHome.About = vAbout;
                }

                return View(vHome);
            }
            catch
            {
                return RedirectToAction("_Error", "Home");
            }
        }
        public ActionResult resume()
        {
            int pId = 1;
            mHome vHome = new mHome();
            List<mGallery> aGallery = new List<mGallery>();
            mCategory vCat = new mCategory();
            vCat.category_type = 2;
            List<mCategory> aCat = new List<mCategory>();
            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            aGallery = g.fGalleryList(pGet, pSkip, pId);
            aCat = c.fCategoryList(12, 0, vCat.category_type);

            if (aGallery != null && aGallery.Count > 0)
            {
                vHome.aGallery = aGallery;
            }
            else
            {
                vHome.aGallery = new List<mGallery>();
            }
            if (aCat != null && aCat.Count > 0)
            {
                vHome.aCategory = aCat;
            }
            else
            {
                vHome.aCategory = new List<mCategory>();
            }
            return View(vHome);
        }
        public ActionResult Videos()
        {

            mHome vHome = new mHome();
            List<mVideo> aVideo = new List<mVideo>();
            mCategory vCat = new mCategory();
            vCat.category_type = 2;
            List<mCategory> aCat = new List<mCategory>();
            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            aVideo = v.fVidoeList(pGet, pSkip);
            aCat = c.fCategoryList(12, 0, vCat.category_type);

            if (aVideo != null && aVideo.Count > 0)
            {
                vHome.aVideo = aVideo;
            }
            else
            {
                vHome.aVideo = new List<mVideo>();
            }
            if (aCat != null && aCat.Count > 0)
            {
                vHome.aCategory = aCat;
            }
            else
            {
                vHome.aCategory = new List<mCategory>();
            }
            return View(vHome);
        }

        public ActionResult team()
        {
            mLink vLink = new mLink();
            List<mLink> aLink = new List<mLink>();
            vLink.link_status = 4;
            mHome vHome = new mHome();
            if (pSkip == 0)
            {
                vHome.page_number = 1;
            }
            else
            {
                vHome.page_number = (pSkip / 12) + 1;
            }
            aLink = l.fLinkList(18, pSkip, vLink);
            if (aLink == null && aLink.Count == 0)
            {
                aLink = new List<mLink>();
            }
            else
            {
                vHome.aLink = aLink;
            }
            return View(vHome);
        }
        public ActionResult representative()
        {
            try
            {
                mAbout vAbout = new mAbout();
                List<mAbout> aAbout = new List<mAbout>();
                vAbout.about_us_type = 5;
                mHome vHome = new mHome();
                mArticle vArticle = new mArticle();
                vAbout = a.fGetAbout(vAbout);

                List<mLink> aLinks = new List<mLink>();
                List<mProduct> aProduct = new List<mProduct>();
                mLink vLink = new mLink();
                vLink.link_status = 2;
                List<mArticle> aArticle = new List<mArticle>();
                aArticle = ar.fArticlesListMostVisited(6, pSkip, vArticle, vHome);
                aLinks = l.fLinkList(12, 0, vLink);

                if (aArticle != null && aArticle.Count > 0)
                {
                    vHome.aArticles = aArticle;
                }
                else
                {
                    vHome.aArticles = new List<mArticle>();
                }
                if (aLinks != null && aLinks.Count > 0)
                {
                    vHome.aLink = aLinks;
                }
                else
                {
                    vHome.aLink = new List<mLink>();
                }


                if (vAbout == null)
                {
                    vAbout = new mAbout();
                }
                else
                {
                    vHome.About = vAbout;
                }
                return View(vHome);
            }
            catch
            {
                return RedirectToAction("_Error", "Home");
            }
        }
        public ActionResult payments()
        {
            try
            {
                mAbout vAbout = new mAbout();
                List<mAbout> aAbout = new List<mAbout>();
                vAbout.about_us_type = 7;
                mHome vHome = new mHome();
                vAbout = a.fGetAbout(vAbout);
                if (vAbout == null)
                {
                    vAbout = new mAbout();
                }
                else
                {
                    vHome.About = vAbout;
                }


                mLink vLink = new mLink();
                vLink.link_status = 2;
                List<mLink> aLinks = new List<mLink>();
                List<mProduct> aProduct = new List<mProduct>();
                List<mNews> aNews = new List<mNews>();
                aNews = b.fNewsList(pGet, pSkip);
                aLinks = l.fLinkList(12, 0, vLink);

                if (aNews != null && aNews.Count > 0)
                {
                    vHome.aNews = aNews;
                }
                else
                {
                    vHome.aNews = new List<mNews>();
                }
                if (aLinks != null && aLinks.Count > 0)
                {
                    vHome.aLink = aLinks;
                }
                else
                {
                    vHome.aLink = new List<mLink>();
                }
                return View(vHome);
            }
            catch
            {
                return RedirectToAction("_Error", "Home");
            }
        }
        public ActionResult register()
        {
            return View();
        }
        public JsonResult RegisterPost(mContact pContact)
        {
            string UserName = "09116476049";
            string Password = "8494";
            var vResult = co.InsertCustomer(pContact);
            if (vResult == true)
            {

                //WebService.Send sms = new WebService.Send();
                //string[] returnValue = sms.SendSimpleSMS(UserName, Password, new string[] {pContact.user_phone}, "30008666476049", "با تشکر از شما، عضویت شما در باشگاه مشتریان بیمه کارآفرین 2571 با موفقیت انجام شد.", true);
                //if (int.Parse(returnValue[0]) > 100)
                //{
                //    for (int i = 0; i < returnValue.Length; i++)
                //    {
                //        Response.Write(returnValue[i]);
                //    }
                //}
                //else
                //{
                //    Response.Write(returnValue[0]);

                //}


                pContact.pSuccess = "ثبت نام با موفقیت انجام شد.";
            }
            else
            {
                pContact.pError = "متاسفانه خطایی رخ داده است.";
            }
            return Json(pContact, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchCenter(mHome pHome)
        {
            mHome vHome = new mHome();
            mCenter pCenter = new mCenter();
            pCenter.center_city = pHome.pSearch;
            List<mCenter> aCenter = new List<mCenter>();
            aCenter = ce.fCenterSearchList(pCenter);
            if (aCenter != null && aCenter.Count > 0)
            {
                vHome.aCenter = aCenter;
            }
            else
            {
                vHome.aCenter = new List<mCenter>();
            }
            return PartialView("_SearchCenter", vHome);

        }
    }
}