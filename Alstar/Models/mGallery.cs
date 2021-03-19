using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mGallery
    {
        public int gallery_id { get; set; }
        public string gallery_title { get; set; }
        public string gallery_title_en { get; set; }
        public string gallery_date { get; set; }
        public Nullable<short> gallery_is_fa { get; set; }
        public string gallery_image { get; set; }
        public Nullable<int> gallery_visit { get; set; }
        public string gallery_alt { get; set; }
        public string gallery_img_width { get; set; }
        public string gallery_img_height { get; set; }
        public Nullable<int> gallery_cat { get; set; }
        public string gallery_summery { get; set; }
        public string gallery_exp { get; set; }
        public string gallery_summery_en { get; set; }
        public string gallery_exp_en { get; set; }
        public HttpPostedFileBase img_file { get; set; }

    }
}