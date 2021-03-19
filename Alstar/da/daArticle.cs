using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daArticle
    {

        AlstarDb Db = new AlstarDb();
        public List<mArticle> fArticlesList(int pGet, int pSkip, mArticle pArticle, mHome pHome)
        {
            if(pArticle != null && pArticle.article_type>0)
            {
                if (pArticle.article_visit > 0)
                {
                    var vArticles = (from a in Db.tbl_article
                                     where a.article_type == pArticle.article_type &&
                                     a.article_visit == pArticle.article_visit
                                     select new mArticle
                                     {
                                         article_id = a.article_id,
                                         article_exp = a.article_exp,
                                         article_title = a.article_title,
                                         article_file = a.article_file,
                                         article_img = a.article_img,
                                         article_img2 = a.article_img2,
                                         article_img3 = a.article_img3,
                                         article_img_alt = a.article_img_alt,
                                         article_summery = a.article_summery,
                                         article_date = a.article_date,
                                         article_writer = a.article_writer,
                                         article_code = a.article_code,
                                         article_href = a.article_href,
                                         article_visit = a.article_visit,
                                         article_type = a.article_type
                                     }).OrderByDescending(b => b.article_id).Skip(pSkip).Take(pGet);
                    return vArticles.ToList();
                }
                else
                {
                    var vArticles = (from a in Db.tbl_article
                                     where a.article_type == pArticle.article_type
                                     select new mArticle
                                     {
                                         article_id = a.article_id,
                                         article_exp = a.article_exp,
                                         article_title = a.article_title,
                                         article_file = a.article_file,
                                         article_img = a.article_img,
                                         article_img2 = a.article_img2,
                                         article_img3 = a.article_img3,
                                         article_img_alt = a.article_img_alt,
                                         article_summery = a.article_summery,
                                         article_date = a.article_date,
                                         article_writer = a.article_writer,
                                         article_href = a.article_href,
                                         article_code = a.article_code,
                                         article_visit = a.article_visit,
                                         article_type = a.article_type
                                     }).OrderByDescending(b => b.article_id).Skip(pSkip).Take(pGet);
                    return vArticles.ToList();
                }
           

            }
            else
            {
                var vArticles = (from a in Db.tbl_article
                                 select new mArticle
                                 {
                                     article_id = a.article_id,
                                     article_exp = a.article_exp,
                                     article_title = a.article_title,
                                     article_file = a.article_file,
                                     article_img = a.article_img,
                                     article_img2 = a.article_img2,
                                     article_img3 = a.article_img3,
                                     article_img_alt = a.article_img_alt,
                                     article_summery = a.article_summery,
                                     article_date = a.article_date,
                                     article_writer = a.article_writer,
                                     article_code = a.article_code,
                                     article_href = a.article_href,
                                     article_visit = a.article_visit,
                                     article_type = a.article_type
                                 }).OrderByDescending(b => b.article_id).Skip(pSkip).Take(pGet);
                return vArticles.ToList();

            }

        }
        public List<mArticle> fArticlesListMostVisited(int pGet, int pSkip, mArticle pArticle, mHome pHome)
        {
                    var vArticles = (from a in Db.tbl_article
                                     where a.article_visit > 0 && a.article_type == pArticle.article_type
                                     select new mArticle
                                     {
                                         article_id = a.article_id,
                                         article_exp = a.article_exp,
                                         article_title = a.article_title,
                                         article_file = a.article_file,
                                         article_img = a.article_img,
                                         article_img2 = a.article_img2,
                                         article_img3 = a.article_img3,
                                         article_img_alt = a.article_img_alt,
                                         article_summery = a.article_summery,
                                         article_date = a.article_date,
                                         article_writer = a.article_writer,
                                         article_href = a.article_href,
                                         article_code = a.article_code,
                                         article_visit = a.article_visit,
                                         article_type = a.article_type
                                     }).OrderByDescending(b => b.article_id).Skip(pSkip).Take(pGet);
                    return vArticles.ToList();
                }
       
        public bool InsertArticles(mArticle pArticles)
        {
            try
            {
                tbl_article vArticle = new tbl_article();
                var query = from b in Db.tbl_article
                            orderby b.article_id descending
                            select b;
                vArticle = query.FirstOrDefault();
                tbl_article a = new tbl_article();
                a.article_file = pArticles.article_file;
                a.article_img = pArticles.article_img;
                a.article_img2 = pArticles.article_img2;
                a.article_img3 = pArticles.article_img3;
                a.article_title = pArticles.article_title;
                a.article_summery = pArticles.article_summery;
                a.article_writer = pArticles.article_writer;
                a.article_date = pArticles.article_date;
                a.article_exp = pArticles.article_exp;
                a.article_img_alt = pArticles.article_img_alt;
                a.article_writer = pArticles.article_writer;
                a.article_code = pArticles.article_code;
                a.article_href = pArticles.article_href;
                a.article_visit = 200;
                a.article_id = vArticle.article_id + 1;
                a.article_type = pArticles.article_type;
                Db.tbl_article.Add(a);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public mArticle fGetArticles(mArticle pArticles)
        {
            try
            {

                var vArticles = (from a in Db.tbl_article
                                 where a.article_id.Equals(pArticles.article_id)
                                 select new mArticle
                                 {
                                     article_id = a.article_id,
                                     article_img_alt = a.article_img_alt,
                                     article_summery = a.article_summery,
                                     article_exp = a.article_exp,
                                     article_writer = a.article_writer,
                                     article_file = a.article_file,
                                     article_img = a.article_img,
                                     article_img2 = a.article_img2,
                                     article_img3 = a.article_img3,
                                     article_title = a.article_title,
                                     article_date = a.article_date,
                                     article_visit = a.article_visit,
                                     article_img_height = a.article_img_height,
                                    article_img_width = a.article_img_width, 
                                     article_href = a.article_href,
                                     article_code = a.article_code,
                                     article_type = a.article_type                                   
                                 }).FirstOrDefault();
                return vArticles;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool fUpdateArticles(mArticle pArticles)
        {            
                tbl_article a = new tbl_article();
                a.article_id = pArticles.article_id;
                a.article_exp = pArticles.article_exp;
                a.article_date = pArticles.article_date;
                a.article_file = pArticles.article_file;
                a.article_img = pArticles.article_img;
                a.article_img2 = pArticles.article_img2;
                a.article_img3 = pArticles.article_img3;
                a.article_img_alt = pArticles.article_img_alt;
                a.article_summery = pArticles.article_summery;
                a.article_title = pArticles.article_title;
                a.article_writer = pArticles.article_writer;
                a.article_visit = pArticles.article_visit ;
                a.article_img = pArticles.article_img;
                a.article_type = pArticles.article_type;
            a.article_code = pArticles.article_code;
            a.article_href = pArticles.article_href;
                Db.tbl_article.Attach(a);
                Db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                return Convert.ToBoolean(Db.SaveChanges());
        
        }


        public bool fUpdateNewsVisit(int page)
        {
            try
            {
                var q = (from a in Db.tbl_news
                         where a.news_id.Equals(page)
                         select a).SingleOrDefault();
                q.news_visit = q.news_visit + 1;
                Db.tbl_news.Attach(q);
                Db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                if (Convert.ToBoolean(Db.SaveChanges()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool fUpdateArticleVisit(int page)
        {
            try
            {
                var q = (from a in Db.tbl_article
                         where a.article_id.Equals(page)
                         select a).SingleOrDefault();
                q.article_visit = q.article_visit + 1;
                Db.tbl_article.Attach(q);
                Db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                if (Convert.ToBoolean(Db.SaveChanges()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }




        public bool fDeleteArticles(int pId)
        {
            try
            {
                var vArticles = (from a in Db.tbl_article
                                 where a.article_id.Equals(pId)
                                 select a).OrderBy(a => a.article_id).SingleOrDefault();
                Db.tbl_article.Remove(vArticles);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsDecimal(decimal pNum)
        {
            string pNumStr = pNum.ToString();
            if (pNumStr.ContaIns('.'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
