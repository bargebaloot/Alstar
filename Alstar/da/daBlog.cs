using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daBlog
    {
        AlstarDb Db = new AlstarDb();
        public List<mNews> fNewsList(int pGet, int pSkip)
        {
            var vNews = (from n in Db.tbl_news
                         select new mNews
                         {
                             news_id = n.news_id,
                             news_summery = n.news_summery,
                             news_image = n.news_image,
                             news_title = n.news_title,
                             news_exp = n.news_exp,
                             news_date = n.news_date,
                             news_writer = n.news_writer,
                             news_category = n.news_category,
                             news_index = n.news_index,
                             news_code = n.news_code,
                             news_href = n.news_href,
                             news_img_alt = n.news_img_alt,
                             news_visit = n.news_visit
                         }).OrderByDescending(b => b.news_id).Skip(pSkip).Take(pGet);
            return vNews.ToList();
        }
        public bool InsertNews(mNews pNews)
        {
            try
            {
                tbl_news vNews = new tbl_news();
                var query = from b in Db.tbl_news
                            orderby b.news_id descending
                            select b;
                vNews = query.FirstOrDefault();
                tbl_news n = new tbl_news();
                n.news_summery = pNews.news_summery;
                n.news_image = pNews.news_image;
                n.news_title = pNews.news_title;
                n.news_exp = pNews.news_exp;
                n.news_date = pNews.news_date;
                n.news_writer = pNews.news_writer;
               n.news_index = pNews.news_index;
                n.news_img_alt = pNews.news_img_alt;
                n.news_href = pNews.news_href;
                n.news_code = pNews.news_code;
                n.news_visit = 200;
                n.news_id = vNews.news_id + 1;
                n.news_img_alt = pNews.news_img_alt;
                Db.tbl_news.Add(n);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public mNews fGetNews(mNews pNews)
        {
            try
            {

                var vNews = (from n in Db.tbl_news
                             where n.news_id.Equals(pNews.news_id)
                             select new mNews
                             {
                                 news_id = n.news_id,
                                 news_summery = n.news_summery,
                                 news_image = n.news_image,
                                 news_title = n.news_title,
                                 news_exp = n.news_exp,
                                 news_date = n.news_date,
                                 news_writer = n.news_writer,
                                 news_index = n.news_index,
                                 news_visit = n.news_visit,
                                 news_category = n.news_category,
                                 news_code = n.news_code,
                                 news_href = n.news_href,
                                 news_img_alt = n.news_img_alt
                             }).FirstOrDefault();
                return vNews;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool fUpdateNews(mNews pNews)
        {
            tbl_news n = new tbl_news();
            n.news_id = pNews.news_id;
            n.news_summery = pNews.news_summery;
            n.news_image = pNews.news_image;
            n.news_title = pNews.news_title;
            n.news_exp = pNews.news_exp;
            n.news_date = pNews.news_date;
            n.news_writer = pNews.news_writer;
           n.news_category = pNews.news_category;
            n.news_index = pNews.news_index;
            n.news_visit = pNews.news_visit;
            n.news_img_alt = pNews.news_img_alt;
            n.news_href = pNews.news_href;
            n.news_code = pNews.news_code;
            Db.tbl_news.Attach(n);
            Db.Entry(n).State = System.Data.Entity.EntityState.Modified;
            return Convert.ToBoolean(Db.SaveChanges());

        }
        public bool fDeleteNews(int pId)
        {
            try
            {
                var vNews = (from a in Db.tbl_news
                             where a.news_id.Equals(pId)
                             select a).OrderBy(a => a.news_id).SingleOrDefault();
                Db.tbl_news.Remove(vNews);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}