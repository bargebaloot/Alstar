using Alstar.Models.database;
using Alstar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daFeedback
    {
        AlstarDb Db = new AlstarDb();
        public List<mFeedback> fFeedbackList(int pGet, int pSkip)
        {
            var vFeedback = (from n in Db.tbl_customer_feedback
                             select new mFeedback
                             {
                                 cust_feedback_id = n.cust_feedback_id,
                                 cust_address = n.cust_address,
                                 cust_company_title = n.cust_company_title,
                                 cust_feedback_exp = n.cust_feedback_exp,
                                 cust_feedback_img = n.cust_feedback_img,
                                 cust_feedback_project_img = n.cust_feedback_project_img,
                                 cust_feedback_title = n.cust_feedback_title
                             }).OrderByDescending(b => b.cust_feedback_id).Skip(pSkip).Take(pGet);
            return vFeedback.ToList();
        }

        public bool InsertFeedback(mFeedback pFeedback)
        {
            try
            {
                tbl_customer_feedback vFeedback = new tbl_customer_feedback();
                var query = from b in Db.tbl_customer_feedback
                            orderby b.cust_feedback_id descending
                            select b;
                vFeedback = query.FirstOrDefault();

                tbl_customer_feedback f = new tbl_customer_feedback();
                f.cust_feedback_title = pFeedback.cust_feedback_title;
                f.cust_feedback_img = pFeedback.cust_feedback_img;
                f.cust_feedback_exp = pFeedback.cust_feedback_exp;
                f.cust_address = pFeedback.cust_address;
                f.cust_company_title = pFeedback.cust_company_title;
                f.cust_feedback_project_img = pFeedback.cust_feedback_project_img;
                f.cust_feedback_id = vFeedback.cust_feedback_id + 1;
                Db.tbl_customer_feedback.Add(f);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public mFeedback fGetFeedback(mFeedback pFeedback)
        {
            try
            {

                var vFeedback = (from f in Db.tbl_customer_feedback
                                 where f.cust_feedback_id.Equals(pFeedback.cust_feedback_id)
                                 select new mFeedback
                                 {
                                     cust_feedback_id = f.cust_feedback_id,
                                     cust_company_title = f.cust_company_title,
                                     cust_address = f.cust_address,
                                     cust_feedback_exp = f.cust_feedback_exp,
                                     cust_feedback_img = f.cust_feedback_img,
                                     cust_feedback_title = f.cust_feedback_title,
                                     cust_feedback_project_img = f.cust_feedback_project_img
                                 }).FirstOrDefault();
                return vFeedback;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool fUpdateFeedback(mFeedback pFeedback)
        {
            try
            {
                tbl_customer_feedback f = new tbl_customer_feedback();
                f.cust_feedback_id = pFeedback.cust_feedback_id;
                f.cust_company_title = pFeedback.cust_company_title;
                f.cust_feedback_img = pFeedback.cust_feedback_img;
                f.cust_feedback_exp = pFeedback.cust_feedback_exp;
                f.cust_address = pFeedback.cust_address;
                f.cust_feedback_title = pFeedback.cust_feedback_title;
                f.cust_feedback_project_img = pFeedback.cust_feedback_project_img;
                Db.tbl_customer_feedback.Attach(f);
                Db.Entry(f).State = System.Data.Entity.EntityState.Modified;
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool fDeleteFeedback(int pId)
        {
            try
            {
                var vFeedback = (from a in Db.tbl_customer_feedback
                                 where a.cust_feedback_id.Equals(pId)
                                 select a).OrderBy(a => a.cust_feedback_id).SingleOrDefault();
                Db.tbl_customer_feedback.Remove(vFeedback);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}