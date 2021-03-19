using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mSlider
    {
        public int slider_id { get; set; }
        public string slider_img { get; set; }
        public string slider_link { get; set; }
        public Nullable<int> slider_sort { get; set; }
        public string slider_title { get; set; }
        public string slider_img_alt { get; set; }
        public string slider_exp { get; set; }
        public string slider_title_en { get; set; }
        public string slider_exp_en { get; set; }
        public HttpPostedFileBase img_file { get; set; }

    }
}