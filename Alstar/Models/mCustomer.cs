using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mCustomer
    {
        public long user_id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_text { get; set; }
        public string user_subject { get; set; }
        public string user_phone { get; set; }
        public string user_date { get; set; }
    }
}