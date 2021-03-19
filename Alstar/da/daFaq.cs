using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daFaq
    {
        AlstarDb Db = new AlstarDb();
        public List<mFaq> fFaqList()
        {
            var vFaq = (from c in Db.tbl_faq
                        select new mFaq
                        {
                            faq_id = c.faq_id,
                            faq_answer = c.faq_answer,
                            faq_question = c.faq_question,
                            faq_answer_en = c.faq_question_en,
                            faq_question_en = c.faq_question_en,
                            faq_exp = c.faq_exp
                        }).OrderBy(b => b.faq_id);
            return vFaq.ToList();
        }
        public bool InsertFaq(mFaq pCat)
        {
            try
            {
                tbl_faq vFaq = new tbl_faq();
                var query = from b in Db.tbl_faq
                            orderby b.faq_id descending
                            select b;
                vFaq = query.FirstOrDefault();
                tbl_faq c = new tbl_faq();
                c.faq_answer = pCat.faq_answer;
                c.faq_exp = pCat.faq_exp;
                c.faq_question = pCat.faq_question;
                c.faq_question_en = pCat.faq_question_en;
                c.faq_answer_en = pCat.faq_answer_en;
                c.faq_id = vFaq.faq_id + 1;
                Db.tbl_faq.Add(c);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public mFaq fGetFaq(mFaq pFaq)
        {
            try
            {
                var vFaq = (from c in Db.tbl_faq
                            where c.faq_id.Equals(pFaq.faq_id)
                            select new mFaq
                            {
                                faq_id = c.faq_id,
                                faq_question = c.faq_question,
                                faq_exp = c.faq_exp,
                                faq_answer_en = c.faq_question_en,
                                faq_question_en = c.faq_question_en,
                                faq_answer = c.faq_answer
                            }).FirstOrDefault();
                return vFaq;
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
                c.faq_question_en = pFaq.faq_question_en;
                c.faq_answer_en = pFaq.faq_answer_en;
                Db.tbl_faq.Attach(c);
                Db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool fDeleteFaq(int pId)
        {
            try
            {
                var vFaq = (from c in Db.tbl_faq
                            where c.faq_id.Equals(pId)
                            select c).OrderBy(a => a.faq_id).SingleOrDefault();
                Db.tbl_faq.Remove(vFaq);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}