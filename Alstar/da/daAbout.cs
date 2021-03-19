using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daAbout
    {
        AlstarDb Db = new AlstarDb();
        public List<mAbout> fAboutList(int pGet, int pSkip, mAbout pAbout)
        {
            IQueryable<mAbout> vAbout;
                vAbout = (from a in Db.tbl_about
                          where a.about_us_id == pAbout.about_us_id
                          select new mAbout
                          {
                              about_us_id = a.about_us_id,
                              about_exp_en_one = a.about_exp_en_one,
                              about_us_type = a.about_us_type,
                              about_exp_en_two = a.about_exp_en_two,
                              about_summery = a.about_summery,
                              about_us_exp_one = a.about_us_exp_one,
                              about_us_exp_two = a.about_us_exp_two,
                              about_us_img = a.about_us_img,
                              about_us_title = a.about_us_title,
                              about_us_img_alt = a.about_us_img_alt
                          }).OrderByDescending(b => b.about_us_type).Skip(pSkip).Take(pGet);
            
            return vAbout.ToList();
        }
        public bool InsertAbout(mAbout pAbout)
        {
            try
            {
                tbl_about vAbout = new tbl_about();
                var query = from b in Db.tbl_about
                            orderby b.about_us_id descending
                            select b;
                vAbout = query.FirstOrDefault();
                tbl_about l = new tbl_about();
                l.about_exp_en_one = pAbout.about_exp_en_one;
                l.about_exp_en_two = pAbout.about_exp_en_two;
                l.about_summery = pAbout.about_summery;
                l.about_us_exp_one = pAbout.about_us_exp_one;
                l.about_us_exp_two = pAbout.about_us_exp_two;
                l.about_us_img = pAbout.about_us_img;
                l.about_us_img_alt = pAbout.about_us_img_alt;
                l.about_us_img_height = pAbout.about_us_img_height;
                l.about_us_img_width = pAbout.about_us_img_width;
                l.about_us_title = pAbout.about_us_title;
                l.about_us_type = pAbout.about_us_type;
                l.about_us_id = vAbout.about_us_id + 1;
                Db.tbl_about.Add(l);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public mAbout fGetAbout(mAbout pAbout)
        {
            try
            {
                var vAbout = (from a in Db.tbl_about
                             where a.about_us_type == (pAbout.about_us_type)
                             select new mAbout
                             {
                                 about_us_id = a.about_us_id,
                                 about_exp_en_one = a.about_exp_en_one,
                                 about_us_type = a.about_us_type,
                                 about_exp_en_two = a.about_exp_en_two,
                                 about_summery = a.about_summery,
                                 about_us_exp_one = a.about_us_exp_one,
                                 about_us_exp_two = a.about_us_exp_two,
                                 about_us_img = a.about_us_img,
                                 about_us_title = a.about_us_title,
                                 about_us_img_alt = a.about_us_img_alt
                             }).FirstOrDefault();
                return vAbout;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public mAbout fGetAboutPanel(mAbout pAbout)
        {
            try
            {
                var vAbout = (from a in Db.tbl_about
                              where a.about_us_id == (pAbout.about_us_id)
                              select new mAbout
                              {
                                  about_us_id = a.about_us_id,
                                  about_exp_en_one = a.about_exp_en_one,
                                  about_us_type = a.about_us_type,
                                  about_exp_en_two = a.about_exp_en_two,
                                  about_summery = a.about_summery,
                                  about_us_exp_one = a.about_us_exp_one,
                                  about_us_exp_two = a.about_us_exp_two,
                                  about_us_img = a.about_us_img,
                                  about_us_title = a.about_us_title,
                                  about_us_img_alt = a.about_us_img_alt
                              }).FirstOrDefault();
                return vAbout;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool fUpdateAbout(mAbout pAbout)
        {
            try
            {
                tbl_about l = new tbl_about();
                l.about_us_id = pAbout.about_us_id;
                l.about_exp_en_one = pAbout.about_exp_en_one;
                l.about_exp_en_two = pAbout.about_exp_en_two;
                l.about_summery = pAbout.about_summery;
                l.about_us_exp_one = pAbout.about_us_exp_one;
                l.about_us_exp_two = pAbout.about_us_exp_two;
                l.about_us_img = pAbout.about_us_img;
                l.about_us_img_alt = pAbout.about_us_img_alt;
                l.about_us_img_height = pAbout.about_us_img_height;
                l.about_us_img_width = pAbout.about_us_img_width;
                l.about_us_title = pAbout.about_us_title;
                l.about_us_type = pAbout.about_us_type;
                Db.tbl_about.Attach(l);
                Db.Entry(l).State = System.Data.Entity.EntityState.Modified;
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool fDeleteAbout(int pId)
        {
            try
            {
                var vAbout = (from a in Db.tbl_about
                             where a.about_us_id.Equals(pId)
                             select a).OrderBy(a => a.about_us_id).SingleOrDefault();
                Db.tbl_about.Remove(vAbout);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}