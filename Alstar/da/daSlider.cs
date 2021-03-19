using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daSlider
    {
        AlstarDb Db = new AlstarDb();

        public List<mSlider> fSliderList()
        {
            var vSlider = (from s in Db.tbl_slider
                           select new mSlider
                           {
                               slider_id = s.slider_id,
                               slider_link = s.slider_link,
                               slider_sort = s.slider_sort,
                               slider_img = s.slider_img,
                               slider_title = s.slider_title,
                               slider_exp = s.slider_exp,
                               slider_img_alt = s.slider_img_alt
                           }).OrderByDescending(b => b.slider_id);
            return vSlider.ToList();
        }

        public bool InsertSlider(mSlider pSliders)
        {
            try
            {
                tbl_slider vSlider = new tbl_slider();
                var query = from b in Db.tbl_slider
                            orderby b.slider_id descending
                            select b;
                vSlider = query.FirstOrDefault();
                tbl_slider s = new tbl_slider();
                s.slider_link = pSliders.slider_link;
                s.slider_img = pSliders.slider_img;
                s.slider_sort = pSliders.slider_sort;
                s.slider_exp = pSliders.slider_exp;
                s.slider_title = pSliders.slider_title;
                s.slider_id = vSlider.slider_id + 1;
                s.slider_img_alt = pSliders.slider_img_alt;
                Db.tbl_slider.Add(s);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public mSlider fGetSlider(mSlider pSlider)
        {
            try
            {

                var vSlider = (from s in Db.tbl_slider
                               where s.slider_id.Equals(pSlider.slider_id)
                               select new mSlider
                               {
                                   slider_id = s.slider_id,
                                   slider_link = s.slider_link,
                                   slider_sort = s.slider_sort,
                                   slider_img = s.slider_img,
                                   slider_title = s.slider_title,
                                   slider_exp = s.slider_exp,
                                   slider_img_alt = s.slider_img_alt
                               }).FirstOrDefault();
                return vSlider;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool fUpdateSlider(mSlider pSliders)
        {
            try
            {
                tbl_slider s = new tbl_slider();
                s.slider_id = pSliders.slider_id;
                s.slider_link = pSliders.slider_link;
                s.slider_img = pSliders.slider_img;
                s.slider_sort = pSliders.slider_sort;
                s.slider_exp = pSliders.slider_exp;
                 s.slider_title = pSliders.slider_title;
                s.slider_img_alt = pSliders.slider_img_alt;
                Db.tbl_slider.Attach(s);
                Db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool fDeleteSlider(int pId)
        {
            try
            {
                var vSlider = (from a in Db.tbl_slider
                               where a.slider_id.Equals(pId)
                               select a).OrderBy(a => a.slider_id).SingleOrDefault();
                Db.tbl_slider.Remove(vSlider);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}