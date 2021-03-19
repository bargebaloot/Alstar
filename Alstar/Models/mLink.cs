using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mLink
    {
        public int link_id { get; set; }
        public string link_title { get; set; }
        public string link_url { get; set; }
        public string link_img { get; set; }
        public string link_exp { get; set; }
        public string link_file { get; set; }
        public Nullable<int> link_status { get; set; }
        public Nullable<int> link_cat { get; set; }
        public string link_title_en { get; set; }
        public string link_exp_en { get; set; }
        public HttpPostedFileBase img_file { get; set; }
        public HttpPostedFileBase img_file2 { get; set; }

    }
}