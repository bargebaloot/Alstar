using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daLogin
    {
        Models.database.AlstarDb db = new Models.database.AlstarDb();
        public bool CheckLogin(string UserEmail, string Password)
        {
            int vLogin = (from a in db.tbl_users
                          where a.user_email.Equals(UserEmail) && a.user_pass.Equals(Password)
                          select a).Count();
            return Convert.ToBoolean(vLogin);
        }
    }
}