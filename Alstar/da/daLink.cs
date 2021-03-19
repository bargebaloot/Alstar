using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daLink
    {
        AlstarDb Db = new AlstarDb();

        public List<mLink> fLinkList(int pGet, int pSkip, mLink pLink)
        {
            IQueryable<mLink> vLink;
            if (pLink.link_status > 0 && pLink.link_cat>0)
            {
                vLink = (from a in Db.tbl_link
                         where a.link_cat == pLink.link_cat &&
                         a.link_status == pLink.link_status
                         select new mLink
                         {
                             link_id = a.link_id,
                             link_exp = a.link_exp,
                             link_img = a.link_img,
                             link_file = a.link_file,
                             link_title = a.link_title,
                             link_url = a.link_url,
                             link_status = a.link_status,
                             link_exp_en = a.link_exp_en,
                             link_title_en = a.link_title_en,
                             link_cat = a.link_cat
                         }).OrderByDescending(b => b.link_id).Skip(pSkip).Take(pGet);
            }
           else if (pLink.link_status > 0)
            {
                vLink = (from a in Db.tbl_link
                         select new mLink
                         {
                             link_id = a.link_id,
                             link_exp = a.link_exp,
                             link_img = a.link_img,
                             link_file = a.link_file,
                             link_title = a.link_title,
                             link_url = a.link_url,
                             link_status = a.link_status,
                             link_exp_en = a.link_exp_en,
                             link_title_en = a.link_title_en,
                             link_cat = a.link_cat
                         }).Where(a => a.link_status==(pLink.link_status)).OrderByDescending(b => b.link_id).Skip(pSkip).Take(pGet);
            }
            else
            {
                vLink = (from a in Db.tbl_link
                         select new mLink
                         {
                             link_id = a.link_id,
                             link_exp = a.link_exp,
                             link_img = a.link_img,
                             link_file = a.link_file,
                             link_title = a.link_title,
                             link_url = a.link_url,
                             link_status = a.link_status,
                             link_exp_en = a.link_exp_en,
                             link_title_en = a.link_title_en,
                             link_cat = a.link_cat
                         }).OrderByDescending(b => b.link_status).Skip(pSkip).Take(pGet);
            }
            return vLink.ToList();
        }
        public bool InsertLink(mLink pLink)
        {
            try
            {
                tbl_link vLink = new tbl_link();
                var query = from b in Db.tbl_link
                            orderby b.link_id descending
                            select b;
                vLink = query.FirstOrDefault();
                tbl_link l = new tbl_link();
                l.link_title = pLink.link_title;
                l.link_url = pLink.link_url;
                l.link_img = pLink.link_img;
                l.link_file = pLink.link_file;
                l.link_exp = pLink.link_exp;
                l.link_status = pLink.link_status;
                l.link_title_en = pLink.link_title_en;
                l.link_exp_en = pLink.link_exp_en;
                l.link_cat = pLink.link_cat;
                l.link_id = vLink.link_id+1;
                Db.tbl_link.Add(l);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public mLink fGetLink(mLink pLink)
        {
            try
            {
                var vLink = (from s in Db.tbl_link
                             where s.link_id.Equals(pLink.link_id)
                             select new mLink
                             {
                                 link_id = s.link_id,
                                 link_exp = s.link_exp,
                                 link_img = s.link_img,
                                 link_file = s.link_file,
                                 link_url = s.link_url,
                                 link_title = s.link_title,
                                 link_status = s.link_status,
                                 link_exp_en = s.link_exp_en,
                                 link_title_en = s.link_title_en,
                                 link_cat = s.link_cat
                             }).FirstOrDefault();
                return vLink;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool fUpdateLink(mLink pLink)
        {
            try
            {
                tbl_link l = new tbl_link();
                l.link_id = pLink.link_id;
                l.link_title = pLink.link_title;
                l.link_exp = pLink.link_exp;
                l.link_url = pLink.link_url;
                l.link_img = pLink.link_img;
                l.link_file = pLink.link_file;
                l.link_status = pLink.link_status;
                l.link_exp_en = pLink.link_exp_en;
                l.link_title_en = pLink.link_title_en;
                l.link_cat = pLink.link_cat;
                Db.tbl_link.Attach(l);
                Db.Entry(l).State = System.Data.Entity.EntityState.Modified;
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool fDeleteLink(int pId)
        {
            try
            {
                var vLink = (from a in Db.tbl_link
                             where a.link_id.Equals(pId)
                             select a).OrderBy(a => a.link_id).SingleOrDefault();
                Db.tbl_link.Remove(vLink);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}