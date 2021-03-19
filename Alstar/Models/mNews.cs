using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mNews
    {
        public int news_id { get; set; }
        public string news_title { get; set; }
        public string news_img_alt { get; set; }
        public string news_image { get; set; }
        public int news_visit { get; set; }
        public Nullable<short> news_index { get; set; }
        public string news_date { get; set; }
        public Nullable<int> news_category { get; set; }
        public string news_exp { get; set; }
        public string news_summery { get; set; }
        public string news_writer { get; set; }
        public string news_title_en { get; set; }
        public string news_exp_en { get; set; }
        public string news_summery_en { get; set; }
        public string news_code { get; set; }
        public string news_href { get; set; }
        public HttpPostedFileBase img_file { get; set; }

    }
}