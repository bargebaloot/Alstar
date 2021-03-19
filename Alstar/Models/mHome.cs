using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.Models
{
    public class mHome
    {
        public string pError { get; set; }
        public string path { get; set; }
        public string pSuccess { get; set; }
        public mArticle Articles { get; set; }
        public mNews Blog { get; set; }
        public mLink Link { get; set; }
        public mProduct Ins { get; set; }
        public mArticle Article { get; set; }
        public List<mCategory> aCategory { get; set; }
        public List<mCategory> aCategoryGallery { get; set; }
        public List<mProduct> aIns { get; set; }
        public List<mNews> aNews { get; set; }
        public List<mSlider> aSlider { get; set; }
        public List<mGallery> aGallery { get; set; }
        public List<mAbout> aAboutUs { get; set; }
        public List<mArticle> aArticles { get; set; }
        public List<mCenter> aCenter { get; set; }
        public List<mArticle> aMagazines { get; set; }
        public List<mCategory> aMagazinesCategory { get; set; }
        public List<mArticle> aLastArticles { get; set; }
        public List<mCustomer> aCustomer { get; set; }
        public List<mFaq> aFaq { get; set; }
        public List<mVideo> aVideo { get; set; }
        public mFaq vmFAQ { get; set; }
        public List<mLink> aLink { get; set; }
        public List<mLink> aLink5 { get; set; }
        public List<mLink> aLink7 { get; set; }
        public List<mFeedback> aFeedback { get; set; }
        public mAbout About { get; set; }
        public mAbout About2 { get; set; }
        public string pSearch { get; set; }

        public int is_search { get; set; }
        public int page_number { get; set; }
        public int search_type { get; set; }
        public Nullable<int> type { get; set; }
        public string search_title { get; set; }
    }
}