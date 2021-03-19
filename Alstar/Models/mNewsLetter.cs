using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mNewsLetter
    {
        public long newsletter_id { get; set; }
        public string newsletter_email { get; set; }
        public string newsletter_phone { get; set; }
        public string newsletter_username { get; set; }
    }
}