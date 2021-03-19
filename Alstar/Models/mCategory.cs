using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mCategory
    {  public int category_id { get; set; }
        public Nullable<int> category_parent { get; set; }
        public string category_href { get; set; }
        public string category_title { get; set; }
        public Nullable<int> category_show_loc { get; set; }
        public string category_img { get; set; }
        public string category_title_en { get; set; }
        public string category_href_en { get; set; }
        public Nullable<int> category_type { get; set; }
        public HttpPostedFileBase img_file { get; set; }
    }
}