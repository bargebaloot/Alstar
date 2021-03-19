using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mAbout
    {
        public int about_us_id { get; set; }
        public string about_us_img { get; set; }
        public string about_us_exp_one { get; set; }
        public string about_us_exp_two { get; set; }
        public string about_us_title { get; set; }
        public string about_us_img_width { get; set; }
        public string about_us_img_height { get; set; }
        public string about_us_img_alt { get; set; }
        public string about_us_title_en { get; set; }
        public Nullable<int> about_us_type { get; set; }
        public string about_exp_en_two { get; set; }
        public string about_exp_en_one { get; set; }
        public string about_summery { get; set; }
        public string about_summery_en { get; set; }
        public HttpPostedFileBase img_file { get; set; }

    }
}