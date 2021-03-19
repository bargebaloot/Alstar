using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mContact
    {
        public long user_id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_text { get; set; }
        public string user_subject { get; set; }
        public string user_phone { get; set; }
        public string user_date { get; set; }
        public string user_file { get; set; }
        public Nullable<int> contact_type { get; set; }
        public string pError { get; set; }
        public string pSuccess { get; set; }
        public HttpPostedFileBase img_file2 { get; set; }
    }
}