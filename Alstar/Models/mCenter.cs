using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mCenter
    {
        public int center_id { get; set; }
        public string center_title { get; set; }
        public Nullable<int> center_cat { get; set; }
        public string center_info { get; set; }
        public string center_img { get; set; }
        public string center_city { get; set; }
        public HttpPostedFileBase img_file { get; set; }
    }
}