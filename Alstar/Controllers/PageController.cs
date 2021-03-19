using Alstar.da;
using Alstar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alstar.Controllers
{
    public class PageController : Controller
    {
        // GET: Page
        daBlog b = new daBlog();
        daProduct p = new daProduct();
        daArticle a = new daArticle();
        daLink l = new daLink();
        public ActionResult Instruction(int page)
        {
            mHome vHome = new mHome();
            mLink vLink = new mLink();
            vLink.link_status = 1;
            vLink.link_id = page;
            vHome.Link = l.fGetLink(vLink);
            if (vHome == null)
            {
                vHome.Link = new mLink();
            }
            return View(vHome);
            
        }
        public ActionResult News(int page)
        {
            mHome vHome = new mHome();
            mNews vNews = new mNews();
            vNews.news_id = page;
            a.fUpdateNewsVisit(page);
            vHome.Blog = b.fGetNews(vNews);
            if (vHome == null)
            {
                vHome = new mHome();
            }
            return View(vHome);
            
        }
        public ActionResult Insurance(int page)
        {
            mHome vHome = new mHome();
            mLink vLink = new mLink();
            vLink.link_status = 6;
            vLink.link_cat = page;
            mProduct vProduct = new mProduct();
            List<mArticle> aArticle = new List<mArticle>();
            mArticle vArticle = new mArticle();

            List<mLink> aLinks = new List<mLink>();
            aLinks = l.fLinkList(12, 0, vLink);
            if (aLinks != null && aLinks.Count > 0)
            {
                vHome.aLink = aLinks;
            }
            else
            {
                vHome.aLink = new List<mLink>();
            }
            vArticle.article_type = 1;
            aArticle = a.fArticlesListMostVisited(10, 0, vArticle, vHome);
            if (aArticle != null && aArticle.Count > 0)
            {
                vHome.aArticles = aArticle;
            }
            else
            {
                vHome.aArticles = new List<mArticle>();
            }
            vProduct.product_id = page;
            vHome.Ins = p.fGetInsCat(vProduct);
            if (vHome == null)
            {
                vHome = new mHome();
            }
            return View(vHome);

        }
        public ActionResult Article(int page)
        {
            mHome vHome = new mHome();
            mArticle vArticle = new mArticle();
            vArticle.article_id = page;
            a.fUpdateArticleVisit(page);
            vHome.Article = a.fGetArticles(vArticle);
            if (vHome == null)
            {
                vHome = new mHome();
            }
            mLink vLink = new mLink();
            vArticle.article_type = 1;
            vLink.link_status = 2;
            List<mArticle> aArticle = new List<mArticle>();
            List<mNews> aNews = new List<mNews>();
            List<mLink> aLinks = new List<mLink>();
            aArticle = a.fArticlesList(8, 0, vArticle, vHome);
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
        public ActionResult Blog(int page)
        {
            mHome vHome = new mHome();
            mArticle vArticle = new mArticle();
            vArticle.article_id = page;
            vHome.Article = a.fGetArticles(vArticle);
            if (vHome == null)
            {
                vHome = new mHome();
            }
            return View(vHome);
        }
        public ActionResult Tag(int page)
        {
            mHome vHome = new mHome();
            mLink vLink = new mLink();
            vLink.link_id = page;
            vHome.Link = l.fGetLink(vLink);
            if (vHome.Link == null)
            {
                vHome = new mHome();
                vHome.Link = new mLink();
            }
           return View(vHome);
        }
    }
}