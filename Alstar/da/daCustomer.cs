using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daCustomer
    {
        AlstarDb Db = new AlstarDb();
        public List<mCustomer> fCustomerList(int pGet, int pSkip)
        {

            var vCustomer = (from n in Db.tbl_customer
                             select new mCustomer
                             {
                                 user_email = n.user_email,
                                 user_id = n.user_id,
                                 user_name = n.user_name,
                                 user_phone = n.user_phone,
                                 user_subject = n.user_subject,
                                 user_text = n.user_text
                             }).OrderBy(b => b.user_id).Skip(pSkip).Take(pGet);
            return vCustomer.ToList();

        }
        public bool InsertCustomer(mCustomer pCustomer)
        {
            try
            {
                tbl_customer vContact = new tbl_customer();
                var query = from b in Db.tbl_customer
                            orderby b.user_id descending
                            select b;
                vContact = query.FirstOrDefault();
                tbl_customer n = new tbl_customer();
                n.user_email = pCustomer.user_email;
                n.user_name = pCustomer.user_name;
                n.user_subject = pCustomer.user_subject;
                n.user_text = pCustomer.user_text;
                n.user_id = vContact.user_id + 1;
                n.user_date = DateTime.Now.Date.ToString();
                Db.tbl_customer.Add(n);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool fDeleteCustomer(int pId)
        {
            try
            {
                var vCustomer = (from a in Db.tbl_customer
                                 where a.user_id.Equals(pId)
                                 select a).OrderBy(a => a.user_id).SingleOrDefault();
                Db.tbl_customer.Remove(vCustomer);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}