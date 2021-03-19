using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mFaq
    {
        public int faq_id { get; set; }
        public string faq_question { get; set; }
        public string faq_answer { get; set; }
        public string faq_exp { get; set; }
        public string faq_question_en { get; set; }
        public string faq_answer_en { get; set; }
    }
}