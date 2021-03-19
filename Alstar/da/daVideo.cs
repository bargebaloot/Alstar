using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daVideo
    {
        AlstarDb Db = new AlstarDb();
        public List<mVideo> fVidoeList(int pGet, int pSkip)
        {
            var vVideo = (from v in Db.tbl_video
                          select new mVideo
                          {
                              video_id = v.video_id,
                              video_alt = v.video_alt,
                              video_code = v.video_code,
                              video_href = v.video_href,
                              video_title = v.video_title
                          }).OrderByDescending(b => b.video_id).Skip(pSkip).Take(pGet);
            return vVideo.ToList();
        }

        public bool InsertVideo(mVideo pVideo)
        {
            try
            {
                tbl_video vVideo = new tbl_video();
                var query = from b in Db.tbl_video
                            orderby b.video_id descending
                            select b;
                vVideo = query.FirstOrDefault();

                tbl_video v = new tbl_video();
                v.video_title = pVideo.video_title;
                v.video_href = pVideo.video_href;
                v.video_code = pVideo.video_code;
                v.video_alt = pVideo.video_alt;
                v.video_id = vVideo.video_id + 1;
                Db.tbl_video.Add(v);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public mVideo fGetVideo(mVideo pVideo)
        {
            try
            {
                var vVideo = (from v in Db.tbl_video
                              where v.video_id.Equals(pVideo.video_id)
                              select new mVideo
                              {
                                  video_id = v.video_id,
                                  video_alt = v.video_alt,
                                  video_code = v.video_code,
                                  video_title = v.video_title,
                                  video_href = v.video_href,
                              }).FirstOrDefault();
                return vVideo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool fUpdateVideo(mVideo pVideo)
        {
            try
            {
                tbl_video v = new tbl_video();
                v.video_id = pVideo.video_id;
                v.video_title = pVideo.video_title;
                v.video_code = pVideo.video_code;
                v.video_alt = pVideo.video_alt;
                v.video_href = pVideo.video_href;
                Db.tbl_video.Attach(v);
                Db.Entry(v).State = System.Data.Entity.EntityState.Modified;
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool fDeleteVideo(int pId)
        {
            try
            {
                var vVideo = (from g in Db.tbl_video
                              where g.video_id.Equals(pId)
                              select g).OrderBy(a => a.video_id).SingleOrDefault();
                Db.tbl_video.Remove(vVideo);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}