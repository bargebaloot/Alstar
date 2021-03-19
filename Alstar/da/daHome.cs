using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daHome
    {
        AlstarDb Db = new AlstarDb();
        public mHome fGetHomeContent()
        {

            mHome pHome = new mHome();
            var vSlider = (from s in Db.tbl_slider
                           select new mSlider
                           {
                               slider_id = s.slider_id,
                               slider_img = s.slider_img,
                               slider_title = s.slider_title,
                               slider_link = s.slider_link,
                               slider_img_alt = s.slider_img_alt,
                               slider_exp = s.slider_exp,
                           }).OrderBy(b => b.slider_id);
            pHome.aSlider = vSlider.ToList();

            var vCategory = (from c in Db.tbl_category
                             select new mCategory
                             {
                                 category_id = c.category_id,
                                 category_href = c.category_href,
                                 category_img = c.category_img,
                                 category_title = c.category_title,
                                 category_parent = c.category_parent,
                                 category_href_en = c.category_href_en,
                                 category_type = c.category_type,
                                 category_title_en = c.category_title_en
                             }).OrderBy(b => b.category_id);
            pHome.aCategory = vCategory.ToList();

            var vAboutUs = (from a in Db.tbl_about
                            where a.about_us_type == 1
                            select new mAbout
                            {
                                about_us_id = a.about_us_id,
                                about_us_img = a.about_us_img,
                                about_us_title = a.about_us_title,
                                about_us_exp_one = a.about_us_exp_one,
                                about_summery = a.about_summery,
                                about_us_exp_two = a.about_us_exp_two,
                                about_us_img_alt = a.about_us_img_alt,
                                about_us_type = a.about_us_type
                            }).FirstOrDefault();
            pHome.About = vAboutUs;

            var aAbout = (from a in Db.tbl_about
                            where a.about_us_type == 2
                            select new mAbout
                            {
                                about_us_id = a.about_us_id,
                                about_us_img = a.about_us_img,
                                about_us_title = a.about_us_title,
                                about_us_exp_one = a.about_us_exp_one,
                                about_summery = a.about_summery,
                                about_us_exp_two = a.about_us_exp_two,
                                about_us_img_alt = a.about_us_img_alt,
                                about_us_type = a.about_us_type
                            }).ToList();
            pHome.aAboutUs = aAbout;

            var aGallery = (from a in Db.tbl_gallery
                            where a.gallery_cat == 2
                            select new mGallery
                            {
                                gallery_id = a.gallery_id,
                                gallery_alt = a.gallery_alt,
                                gallery_cat = a.gallery_cat,
                                gallery_date = a.gallery_date,
                                gallery_exp = a.gallery_exp,
                                gallery_image = a.gallery_image,
                                gallery_summery = a.gallery_summery,
                                gallery_title = a.gallery_title,
                                gallery_is_fa = a.gallery_is_fa,
                                gallery_visit = a.gallery_visit
                            }).ToList();
            pHome.aGallery = aGallery;

            var vNews = (from n in Db.tbl_news
                         select new mNews
                         {
                             news_id = n.news_id,
                             news_title = n.news_title,
                             news_image = n.news_image,
                             news_summery = n.news_summery,
                             news_date = n.news_date,
                             news_writer = n.news_writer,
                             news_category = n.news_category,
                             news_exp = n.news_exp,
                            news_img_alt = n.news_img_alt,
                            news_visit = n.news_visit
                         }).Take(3).OrderByDescending(b => b.news_id);

            pHome.aNews = vNews.ToList();

            var vLink = (from n in Db.tbl_link
                         where n.link_status == 1
                         select new mLink
                         {
                             link_id = n.link_id,
                             link_exp = n.link_exp,
                             link_img = n.link_img,
                             link_status = n.link_status,
                             link_title = n.link_title,
                             link_exp_en = n.link_exp_en,
                             link_title_en = n.link_title_en
                         }).Take(12).OrderByDescending(b => b.link_id);

            pHome.aLink = vLink.ToList();

            var vLink7 = (from n in Db.tbl_link
                         where n.link_status == 7
                         select new mLink
                         {
                             link_id = n.link_id,
                             link_exp = n.link_exp,
                             link_img = n.link_img,
                             link_status = n.link_status,
                             link_title = n.link_title,
                             link_exp_en = n.link_exp_en,
                             link_url = n.link_url,
                             link_title_en = n.link_title_en
                         }).Take(6).OrderByDescending(b => b.link_id);

            pHome.aLink7 = vLink7.ToList();

            var vLink5 = (from  l in Db.tbl_link
                         where l.link_status == 5
                         select new mLink
                         {
                             link_id = l.link_id,
                             link_exp = l.link_exp,
                             link_img = l.link_img,
                             link_status = l.link_status,
                             link_title = l.link_title,
                             link_exp_en = l.link_exp_en,
                             link_title_en = l.link_title_en
                         }).Take(3).OrderByDescending(b => b.link_id);

            pHome.aLink5 = vLink5.ToList();

            var vArticles = (from a in Db.tbl_article
                             where a.article_type == 1
                             select new mArticle
                             {
                                 article_id = a.article_id,
                                 article_exp = a.article_exp,
                                 article_title = a.article_title,
                                 article_img = a.article_img,
                                 article_img2 = a.article_img2,
                                 article_img3 = a.article_img3,
                                 article_img_alt = a.article_img_alt,
                                 article_summery = a.article_summery,
                                 article_date = a.article_date,
                                 article_writer = a.article_writer,
                                 article_visit = a.article_visit,
                                 article_type = a.article_type
                             }).Take(3).OrderByDescending(b => b.article_id);
            pHome.aArticles = vArticles.ToList();


            var vFeedback = (from a in Db.tbl_customer_feedback
                             select new mFeedback
                             {
                                 cust_feedback_id = a.cust_feedback_id,
                                 cust_feedback_title = a.cust_feedback_title,
                                 cust_feedback_img = a.cust_feedback_img,
                                 cust_feedback_exp = a.cust_feedback_exp,
                                 cust_company_title = a.cust_company_title,
                                 cust_address = a.cust_address,
                                 cust_feedback_project_img = a.cust_feedback_project_img,
                                 cust_feedback_img_height = a.cust_feedback_img_height,
                                 cust_feedback_img_width = a.cust_feedback_img_width
                             }).Take(6).OrderByDescending(b => b.cust_feedback_id);
            pHome.aFeedback = vFeedback.ToList();

            var vIns = (from p in Db.tbl_product
                             select new mProduct
                             {
                                 product_id = p.product_id,
                                 product_img1 = p.product_img1,
                                 product_img2 = p.product_img2,
                                 product_img_alt = p.product_img_alt,
                                 product_price = p.product_price,
                                 product_summery = p.product_summery,
                                 product_title1 = p.product_title1,
                                 product_title2 = p.product_title2,
                                 product_cat = p.product_cat,
                                 product_parent = p.product_parent,
                                 product_exist = p.product_exist,
                                 product_exp = p.product_exp,
                                 product_visit = p.product_visit,
                             }).Take(4).OrderByDescending(b => b.product_id); ;
            pHome.aIns = vIns.ToList();

            return pHome;

        }

        public mHome fGetLinks()
        {
            try
            {
                mHome pHome = new mHome();

                var vLink = (from s in Db.tbl_link
                             where s.link_status.Equals(1)
                             select new mLink
                             {
                                 link_id = s.link_id,
                                 link_exp = s.link_exp,
                                 link_url = s.link_url,
                                 link_title = s.link_title,
                                 link_img = s.link_img,
                                 link_title_en = s.link_title_en,
                                 link_exp_en = s.link_exp_en
                             }).OrderBy(b => b.link_id);
                pHome.aLink = vLink.ToList();

                return pHome;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public mHome fGetSearch(string pSearch)
        {
            try
            {
                mHome pHome = new mHome();
                var vNews = (from n in Db.tbl_news
                             where n.news_title.ContaIns(pSearch) || n.news_summery.ContaIns(pSearch)
                             || n.news_exp.ContaIns(pSearch) 
                             select new mNews
                             {
                                 news_id = n.news_id,
                                 news_title = n.news_title,
                                 news_summery = n.news_summery,
                                 news_image = n.news_image,
                                 news_exp = n.news_exp,
                                 news_category = n.news_category,
                                 news_img_alt = n.news_img_alt,
                                 news_visit = n.news_visit,
                                 news_writer = n.news_writer,
                                 news_date = n.news_date
                             }).OrderByDescending(b => b.news_id);
                pHome.aNews = vNews.ToList();


                var vArticle = (from n in Db.tbl_article
                             where n.article_title.ContaIns(pSearch) 
                             || n.article_summery.ContaIns(pSearch) 
                             || n.article_exp.ContaIns(pSearch) 
                             select new mArticle
                             {
                                 article_id = n.article_id,
                                  article_exp = n.article_exp,
                                  article_summery = n.article_summery,
                                 article_title = n.article_title,
                                 article_date = n.article_date,
                                 article_img = n.article_img,
                                 article_img_alt = n.article_img_alt,
                                 article_type = n.article_type,
                                 article_writer = n.article_writer,
                                 article_visit = n.article_visit
                             }).OrderByDescending(b => b.article_id);
                pHome.aArticles = vArticle.ToList();

                var vLink = (from n in Db.tbl_link
                             where n.link_title.ContaIns(pSearch) || n.link_title_en.ContaIns(pSearch)
                             select new mLink
                             {
                                 link_title = n.link_title,
                                 link_exp = n.link_exp,
                                 link_exp_en = n.link_exp_en,
                                 link_id = n.link_id,
                                 link_img = n.link_img,
                                 link_status = n.link_status,
                                 link_title_en = n.link_title_en,
                                 link_url = n.link_url
                             }).OrderByDescending(b => b.link_id);
                pHome.aLink = vLink.ToList();
                return pHome;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<mArticle> fArticlesList(mHome pHome)
        {
                    var vArticles = (from a in Db.tbl_article
                                     where a.article_title.ContaIns(pHome.search_title)
                                     select new mArticle
                                     {
                                         article_id = a.article_id,
                                         article_title = a.article_title
                                     }).OrderByDescending(b => b.article_id);
                    return vArticles.ToList();
        }

    }
}