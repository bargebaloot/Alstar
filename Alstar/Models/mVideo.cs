using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mVideo
    {
        public int video_id { get; set; }
        public string video_title { get; set; }
        public string video_date { get; set; }
        public string video_href { get; set; }
        public string video_alt { get; set; }
        public Nullable<long> video_code { get; set; }
    }
}