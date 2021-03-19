using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daGallery
    {
        AlstarDb Db = new AlstarDb();
        public List<mGallery> fGalleryList(int pGet , int pSkip, int? pId)
        {
            if (pId > 0)
            {
                var vGallery = (from a in Db.tbl_gallery
                                where a.gallery_cat == pId
                                select new mGallery
                                {
                                    gallery_id = a.gallery_id,
                                    gallery_alt = a.gallery_alt,
                                    gallery_image = a.gallery_image,
                                    gallery_title = a.gallery_title,
                                    gallery_title_en = a.gallery_title_en,
                                    gallery_date = a.gallery_date,
                                    gallery_cat = a.gallery_cat,
                                    gallery_is_fa = a.gallery_is_fa,
                                    //gallery_exp = a.gallery_exp,
                                    //gallery_summery_en = a.gallery_summery_en,
                                    //gallery_summery = a.gallery_summery,
                                    gallery_visit = a.gallery_visit
                                }).OrderByDescending(b => b.gallery_id).Skip(pSkip).Take(pGet);
                return vGallery.ToList();
            }
            else
            {
                var vGallery = (from a in Db.tbl_gallery
                                select new mGallery
                                {
                                    gallery_id = a.gallery_id,
                                    gallery_alt = a.gallery_alt,
                                    gallery_image = a.gallery_image,
                                    gallery_title = a.gallery_title,
                                    gallery_title_en = a.gallery_title_en,
                                    gallery_date = a.gallery_date,
                                    gallery_cat = a.gallery_cat,
                                    gallery_is_fa = a.gallery_is_fa,
                                    //gallery_exp = a.gallery_exp,
                                    //gallery_summery_en = a.gallery_summery_en,
                                    //gallery_summery = a.gallery_summery,
                                    gallery_visit = a.gallery_visit
                                }).OrderByDescending(b => b.gallery_id).Skip(pSkip).Take(pGet);
                return vGallery.ToList();
            }
              
            }

        

        public bool InsertGallery(mGallery pGallery)
        {
            try
            {
                tbl_gallery vGallery = new tbl_gallery();
                var query = from b in Db.tbl_gallery
                            orderby b.gallery_id descending
                            select b;
                vGallery = query.FirstOrDefault();
                tbl_gallery g = new tbl_gallery();
                g.gallery_title = pGallery.gallery_title;
                g.gallery_title_en = pGallery.gallery_title_en;
                g.gallery_image = pGallery.gallery_image;
                g.gallery_alt = pGallery.gallery_alt;
                g.gallery_date = pGallery.gallery_date;
                g.gallery_summery = pGallery.gallery_summery;
                g.gallery_summery_en = pGallery.gallery_summery_en;
                g.gallery_exp = pGallery.gallery_exp;
                g.gallery_exp_en = pGallery.gallery_exp_en;
                g.gallery_is_fa = pGallery.gallery_is_fa;
                g.gallery_cat = pGallery.gallery_cat;
                g.gallery_id =   vGallery.gallery_id + 1;
                Db.tbl_gallery.Add(g);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public mGallery fGetGallery(mGallery pGallery)
        {
            try
            {

                var vGallery = (from g in Db.tbl_gallery
                                where g.gallery_id.Equals(pGallery.gallery_id)
                                select new mGallery
                                {
                                    gallery_id = g.gallery_id,
                                    gallery_image = g.gallery_image,
                                    gallery_date = g.gallery_date,
                                    gallery_title = g.gallery_title,
                                    gallery_title_en = g.gallery_title_en,
                                    gallery_alt = g.gallery_alt,
                                    gallery_cat = g.gallery_cat,
                                    gallery_exp_en = g.gallery_exp_en,
                                    gallery_exp = g.gallery_exp,
                                    gallery_is_fa = g.gallery_is_fa,
                                    gallery_summery_en = g.gallery_summery_en,
                                    gallery_summery = g.gallery_summery,
                                    gallery_visit = g.gallery_visit
                                }).FirstOrDefault();
                return vGallery;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool fUpdateGallery(mGallery pGallery)
        {
            try
            {
                tbl_gallery g = new tbl_gallery();
                g.gallery_id = pGallery.gallery_id;
                g.gallery_image = pGallery.gallery_image;
                g.gallery_date = pGallery.gallery_date;
                g.gallery_title = pGallery.gallery_title;
                g.gallery_title_en = pGallery.gallery_title_en;
                g.gallery_alt = pGallery.gallery_alt;
                g.gallery_summery = pGallery.gallery_summery;
                g.gallery_summery_en = pGallery.gallery_summery_en;
                g.gallery_exp = pGallery.gallery_exp;
                g.gallery_is_fa = pGallery.gallery_is_fa;
                g.gallery_exp_en = pGallery.gallery_exp_en;
                g.gallery_cat = pGallery.gallery_cat;
                Db.tbl_gallery.Attach(g);
                Db.Entry(g).State = System.Data.Entity.EntityState.Modified;
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool fDeleteGallery(int pId)
        {
            try
            {
                var vGallery = (from g in Db.tbl_gallery
                                where g.gallery_id.Equals(pId)
                                select g).OrderBy(a => a.gallery_id).SingleOrDefault();
                Db.tbl_gallery.Remove(vGallery);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}