using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mArticle
    {
        public int article_id { get; set; }
        public string article_title { get; set; }
        public string article_date { get; set; }
        public string article_summery { get; set; }
        public string article_img { get; set; }
        public string article_file { get; set; }
        public string article_img2 { get; set; }
        public string article_img3 { get; set; }
        public string article_img_alt { get; set; }
        public string article_exp { get; set; }
        public string article_img_width { get; set; }
        public string article_img_height { get; set; }
        public string article_writer { get; set; }
        public Nullable<int> article_visit { get; set; }
        public string article_title_en { get; set; }
        public string article_summery_en { get; set; }
        public string article_exp_en { get; set; }
        public string article_href { get; set; }
        public string article_code { get; set; }
        public HttpPostedFileBase img_file { get; set; }
        public HttpPostedFileBase img_file2 { get; set; }
        public HttpPostedFileBase img_file3 { get; set; }
        public Nullable<int> article_type { get; set; }

    }
}