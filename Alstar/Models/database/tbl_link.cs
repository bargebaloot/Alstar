//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alstar.Models.database
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_link
    {
        public int link_id { get; set; }
        public string link_title { get; set; }
        public string link_url { get; set; }
        public string link_img { get; set; }
        public string link_exp { get; set; }
        public Nullable<int> link_status { get; set; }
        public Nullable<int> link_cat { get; set; }
        public string link_title_en { get; set; }
        public string link_exp_en { get; set; }
        public string link_file { get; set; }
        public Nullable<int> link_lang { get; set; }
    }
}