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
    
    public partial class tbl_category
    {
        public int category_id { get; set; }
        public Nullable<int> category_parent { get; set; }
        public string category_href { get; set; }
        public string category_title { get; set; }
        public Nullable<int> category_show_loc { get; set; }
        public string category_img { get; set; }
        public string category_title_en { get; set; }
        public string category_href_en { get; set; }
        public Nullable<int> category_type { get; set; }
        public Nullable<int> category_lang { get; set; }
    }
}