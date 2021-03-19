using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mProduct
    {
        public int product_id { get; set; }
        public string product_title { get; set; }
        public string product_img1 { get; set; }
        public string product_img_alt { get; set; }
        public string product_price { get; set; }
        public string product_exp { get; set; }
        public string product_summery { get; set; }
        public Nullable<int> product_cat { get; set; }
        public string product_save { get; set; }
        public Nullable<int> product_exist { get; set; }
        public int product_visit { get; set; }
        public string product_img2 { get; set; }
        public string product_title1 { get; set; }
        public string product_title2 { get; set; }
        public Nullable<int> product_parent { get; set; }
        public Nullable<int> product_lang { get; set; }
        public HttpPostedFileBase img_file1 { get; set; }
        public HttpPostedFileBase img_file2 { get; set; }
        public HttpPostedFileBase img_file3 { get; set; }
    }
}