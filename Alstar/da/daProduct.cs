using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;

namespace Alstar.da
{
    public class daProduct
    {
        AlstarDb Db = new AlstarDb();
        public List<mProduct> fInsList(int pGet, int pSkip, int? pId)
        {
            if (pId > 0)
            {
                var vPrd = (from p in Db.tbl_product
                            where p.product_parent == pId
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
                            }).OrderByDescending(b => b.product_id).Skip(pSkip).Take(pGet); ;
                return vPrd.ToList();
            }
            else
            {
                var vPrd = (from p in Db.tbl_product
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
                                product_visit = p.product_visit
                            }).OrderByDescending(b => b.product_id).Skip(pSkip).Take(pGet); ;
                return vPrd.ToList();
            }

        }


        public bool InsertIns(mProduct pPrd)
        {
            try
            {
                tbl_product vPrd = new tbl_product();
                var query = from b in Db.tbl_product
                            orderby b.product_id descending
                            select b;
                vPrd = query.FirstOrDefault();
                tbl_product p = new tbl_product();
                Random rnd = new Random();
                p.product_img1 = pPrd.product_img1;
                p.product_img2 = pPrd.product_img2;
                p.product_img_alt = pPrd.product_img_alt;
                p.product_price = pPrd.product_price;
                p.product_summery = pPrd.product_summery;
                p.product_title1 = pPrd.product_title1;
                p.product_title2 = pPrd.product_title2;
                p.product_cat = pPrd.product_cat;
                p.product_parent = pPrd.product_parent;
                p.product_exist = pPrd.product_exist;
                p.product_exp = pPrd.product_exp;
                p.product_save = pPrd.product_save;
                p.product_visit = 200;
                p.product_id = vPrd.product_id + 1;
                Db.tbl_product.Add(p);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public mProduct fGetIns(mProduct pPrd)
        {
            try
            {
                var vPrd = (from p in Db.tbl_product
                            where p.product_id == pPrd.product_id
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
                                product_visit = p.product_visit
                            }).FirstOrDefault();
                return vPrd;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public mProduct fGetInsCat(mProduct pPrd)
        {
            try
            {
                var vPrd = (from p in Db.tbl_product
                            where p.product_cat == pPrd.product_id
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
                                product_visit = p.product_visit
                            }).FirstOrDefault();
                return vPrd;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool fUpdateFaq(mFaq pFaq)
        {
            try
            {
                tbl_faq c = new tbl_faq();
                c.faq_id = pFaq.faq_id;
                c.faq_answer = pFaq.faq_answer;
                c.faq_exp = pFaq.faq_exp;
                c.faq_question = pFaq.faq_question;
                Db.tbl_faq.Attach(c);
                Db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool fUpdateIns(mProduct pPrd)
        {

            tbl_product p = new tbl_product();
            p.product_id = pPrd.product_id;
            p.product_img1 = pPrd.product_img1;
            p.product_img2 = pPrd.product_img2;
            p.product_img_alt = pPrd.product_img_alt;
            p.product_price = pPrd.product_price;
            p.product_summery = pPrd.product_summery;
            p.product_title1 = pPrd.product_title1;
            p.product_title2 = pPrd.product_title2;
            p.product_cat = pPrd.product_cat;
            p.product_parent = pPrd.product_parent;
            p.product_exist = pPrd.product_exist;
            p.product_exp = pPrd.product_exp;
            p.product_save = pPrd.product_save;
            Db.tbl_product.Attach(p);
            Db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            return Convert.ToBoolean(Db.SaveChanges());

        }

        public bool fDeleteIns(int pId)
        {
            try
            {
                var vPrd = (from p in Db.tbl_product
                            where p.product_id.Equals(pId)
                            select p).OrderBy(a => a.product_id).SingleOrDefault();
                Db.tbl_product.Remove(vPrd);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}