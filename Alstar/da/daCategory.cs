using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daCategory
    {
        AlstarDb Db = new AlstarDb();
        public List<mCategory> fCategoryList(int pGet, int pSkip, int? pId)
        {
            if (pId > 0)
            {
                var vCat = (from c in Db.tbl_category
                            where c.category_type == pId
                            select new mCategory
                            {
                                category_id = c.category_id,
                                category_parent = c.category_parent,
                                category_href = c.category_href,
                                category_title = c.category_title,
                                category_title_en = c.category_title_en,
                                category_href_en = c.category_href_en,
                                category_show_loc = c.category_show_loc,
                                category_type = c.category_type,
                                category_img = c.category_img
                            }).OrderBy(b => b.category_id).Skip(pSkip).Take(pGet);
                return vCat.ToList();
            }
            else
            {
                var vCat = (from c in Db.tbl_category
                            select new mCategory
                            {
                                category_id = c.category_id,
                                category_parent = c.category_parent,
                                category_href = c.category_href,
                                category_title = c.category_title,
                                category_title_en = c.category_title_en,
                                category_href_en = c.category_href_en,
                                category_show_loc = c.category_show_loc,
                                category_type = c.category_type,
                                category_img = c.category_img
                            }).OrderBy(b => b.category_id).Skip(pSkip).Take(pGet);
                return vCat.ToList();
            }

        }
        public List<mCategory> fCategoryListMenu()
        {
                var vCat = (from c in Db.tbl_category
                            select new mCategory
                            {
                                category_id = c.category_id,
                                category_parent = c.category_parent,
                                category_href = c.category_href,
                                category_title = c.category_title,
                                category_title_en = c.category_title_en,
                                category_href_en = c.category_href_en,
                                category_show_loc = c.category_show_loc,
                                category_type = c.category_type,
                                category_img = c.category_img
                            }).OrderByDescending(b => b.category_id);
                return vCat.ToList();          
        }
        public List<mCategory> fCategoryInsurance(int pId)
        {

            var vCat = (from c in Db.tbl_category
                        where c.category_parent  == pId
                        select new mCategory
                        {
                            category_id = c.category_id,
                            category_parent = c.category_parent,
                            category_href = c.category_href,
                            category_title = c.category_title,
                            category_title_en = c.category_title_en,
                            category_href_en = c.category_href_en,
                            category_show_loc = c.category_show_loc,
                            category_type = c.category_type,
                            category_img = c.category_img
                        }).OrderBy(b => b.category_id);
            return vCat.ToList();

        }
        public bool InsertCategory(mCategory pCat)
        {
            try
            {
                tbl_category vCat = new tbl_category();
                var query = from b in Db.tbl_category
                            orderby b.category_id descending
                            select b;
                vCat = query.FirstOrDefault();
                tbl_category c = new tbl_category();
                c.category_title = pCat.category_title;
                c.category_href = pCat.category_href;
                c.category_parent = pCat.category_parent;
                c.category_title_en = pCat.category_title_en;
                c.category_href_en = pCat.category_href_en;
                c.category_img = pCat.category_img;
                c.category_show_loc = pCat.category_show_loc;
                c.category_parent = pCat.category_parent;
                c.category_type = pCat.category_type;
                c.category_id = vCat.category_id + 1;
                Db.tbl_category.Add(c);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public mCategory fGetCategory(mCategory pCat)
        {
            try
            {
                var vCat = (from c in Db.tbl_category
                            where c.category_id.Equals(pCat.category_id)
                            select new mCategory
                            {
                                category_id = c.category_id,
                                category_href = c.category_href,
                                category_parent = c.category_parent,
                                category_title = c.category_title,
                                category_title_en = c.category_title_en,
                                category_href_en = c.category_href_en,
                                category_show_loc = c.category_show_loc,
                                category_type = c.category_type,
                                category_img = c.category_img
                            }).FirstOrDefault();
                return vCat;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool fUpdateCategory(mCategory pCat)
        {
            try
            {
                tbl_category c = new tbl_category();
                c.category_id = pCat.category_id;
                c.category_title = pCat.category_title;
                c.category_parent = pCat.category_parent;
                c.category_href = pCat.category_href;
                c.category_title_en = pCat.category_title_en;
                c.category_href_en = pCat.category_href_en;
                c.category_img = pCat.category_img;
                c.category_show_loc = pCat.category_show_loc;
                c.category_parent = pCat.category_parent;
                c.category_type = pCat.category_type;
                Db.tbl_category.Attach(c);
                Db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool fDeleteCategory(int pId)
        {
            try
            {
                var vCat = (from c in Db.tbl_category
                            where c.category_id.Equals(pId)
                            select c).OrderBy(a => a.category_id).SingleOrDefault();
                Db.tbl_category.Remove(vCat);
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
            if (pNumStr.Contains('.'))
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