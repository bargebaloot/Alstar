using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mFeedback
    {
        public int cust_feedback_id { get; set; }
        public string cust_feedback_title { get; set; }
        public string cust_feedback_exp { get; set; }
        public string cust_feedback_img { get; set; }
        public string cust_company_title { get; set; }
        public string cust_address { get; set; }
        public string cust_feedback_img_width { get; set; }
        public string cust_feedback_img_height { get; set; }
        public string cust_feedback_project_img { get; set; }
        public HttpPostedFileBase img_file { get; set; }
    }
}