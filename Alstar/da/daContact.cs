using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daContact
    {
        AlstarDb Db = new AlstarDb();
        public List<mContact> fContactList(int pGet, int pSkip,int pId)
        {

            var vContactUs = (from n in Db.tbl_contact
                              where n.contact_type == pId
                              select new mContact
                              {
                                  user_email = n.user_email,
                                  user_id = n.user_id,
                                  user_name = n.user_name,
                                  user_phone = n.user_phone,
                                  user_subject = n.user_subject,
                                  user_file = n.user_file,
                                  user_date = n.user_date,
                                  contact_type = n.contact_type,
                                  user_text = n.user_text
                              }).OrderBy(b => b.user_id).Skip(pSkip).Take(pGet);
            return vContactUs.ToList();

        }
        public bool InsertContact(mContact pContact)
        {
            try
            {
                tbl_contact vContact = new tbl_contact();
                var query = from b in Db.tbl_contact
                            orderby b.user_id descending
                            select b;
                vContact = query.FirstOrDefault();
                tbl_contact n = new tbl_contact();
                n.user_email = pContact.user_email;
                n.user_name = pContact.user_name;
                n.user_subject = pContact.user_subject;
                n.user_text = pContact.user_text;
                n.user_id = vContact.user_id + 1;
                n.user_date = DateTime.Now.Date.ToString();
                n.user_file = pContact.user_file;
                n.contact_type = pContact.contact_type;
                Db.tbl_contact.Add(n);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InsertCustomer(mContact pContact)
        {
            try
            {
                tbl_customer vContact = new tbl_customer();
                var query = from b in Db.tbl_customer
                            orderby b.user_id descending
                            select b;
                vContact = query.FirstOrDefault();
                tbl_customer n = new tbl_customer();
                n.user_email = pContact.user_email;
                n.user_name = pContact.user_name;
                n.user_subject = pContact.user_subject;
                n.user_text = pContact.user_text;
                n.user_phone = pContact.user_phone;
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
        public bool fDeleteContact(int pId)
        {
            try
            {
                var vContact = (from a in Db.tbl_contact
                                where a.user_id.Equals(pId)
                                select a).OrderBy(a => a.user_id).SingleOrDefault();
                Db.tbl_contact.Remove(vContact);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}