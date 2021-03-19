using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daAdmin
    {
        AlstarDb Db = new AlstarDb();
        public List<mUser> fGetAdminusers()
        {
            var vUsers = (from a in Db.tbl_users
                          select new mUser
                          {
                              user_id = a.user_id,
                              user_email = a.user_email,
                              user_name = a.user_name,
                              user_pass = a.user_pass,
                              first_name = a.first_name
                          }).OrderByDescending(b => b.user_id);
            return vUsers.ToList();
        }

        public bool InsertUsers(mUser pUsers)
        {
            try
            {
                Random rnd = new Random();
                tbl_users u = new tbl_users();
                u.user_email = pUsers.user_email;
                u.user_name = pUsers.user_name;
                u.user_pass = pUsers.user_pass;
                u.first_name = pUsers.first_name;
                u.last_name = pUsers.last_name;
                u.user_phone = pUsers.user_phone;
                u.user_id = rnd.Next();
                Db.tbl_users.Add(u);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool checkAdminuserExists(string pEmail)
        {
            try
            {
                int vUser = (from a in Db.tbl_users
                             where a.user_email.Equals(pEmail)
                             select a).Count();
                if (vUser > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }
        public mUser fGetUsers(mUser pAdminUsers)
        {
            try
            {
                var vUsers = (from a in Db.tbl_users
                              where a.user_id.Equals(pAdminUsers.user_id)
                              select new mUser
                              {
                                  user_id = a.user_id,
                                  user_email = a.user_email,
                                  user_name = a.user_name,
                                  user_pass = a.user_pass,
                                  user_phone = a.user_phone,
                                  first_name = a.first_name,
                                  last_name = a.last_name
                              }).FirstOrDefault();
                return vUsers;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool fUpdateAdminuser(mUser pUsers)
        {
            try
            {
                tbl_users u = new tbl_users();
                u.user_email = pUsers.user_email;
                u.user_name = pUsers.user_name;
                u.user_pass = pUsers.user_pass;
                u.user_id = pUsers.user_id;
                u.user_phone = pUsers.user_phone;
                u.first_name = pUsers.first_name;
                u.last_name = pUsers.last_name;
                Db.tbl_users.Attach(u);
                Db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool fDeleteAdminuser(long pId)
        {
            try
            {
                var q = (from a in Db.tbl_users
                         where a.user_id.Equals(pId)
                         select a).OrderBy(a => a.user_id).SingleOrDefault();
                Db.tbl_users.Remove(q);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}