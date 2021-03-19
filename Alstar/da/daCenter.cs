using Alstar.Models;
using Alstar.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alstar.da
{
    public class daCenter
    {
        AlstarDb Db = new AlstarDb();
        public List<mCenter> fCenterList(int pGet, int pSkip, mCenter pCenter, mHome pHome)
        {
            
                if (pCenter.center_cat > 0)
                {
                    var vCenter = (from a in Db.tbl_center
                                     where a.center_cat == pCenter.center_cat
                                    select new mCenter
                                     {
                                        center_id = a.center_id,
                                        center_title = a.center_title,
                                        center_info = a.center_info,
                                        center_img = a.center_img,
                                        center_city = a.center_city,
                                        center_cat = a.center_cat
                                    }).OrderBy(b => b.center_id).Skip(pSkip).Take(pGet);
                    return vCenter.ToList();
                }
                else
                {
                    var vCenter = (from a in Db.tbl_center
                                   select new mCenter
                                     {
                                         center_id = a.center_id,
                                         center_title = a.center_title,
                                         center_info = a.center_info,
                                         center_img = a.center_img,
                                         center_city = a.center_city,
                                         center_cat = a.center_cat
                                     }).OrderBy(b => b.center_id).Skip(pSkip).Take(pGet);
                    return vCenter.ToList();
                }
            
        }
        public List<mCenter> fCenterSearchList(mCenter pCenter)
        {

                var vCenter = (from a in Db.tbl_center
                               where a.center_city.Contains(pCenter.center_city) || a.center_title.Contains(pCenter.center_city)
                               select new mCenter
                               {
                                   center_id = a.center_id,
                                   center_title = a.center_title,
                                   center_info = a.center_info,
                                   center_img = a.center_img,
                                   center_city = a.center_city,
                                   center_cat = a.center_cat
                               }).OrderBy(b => b.center_id);
                return vCenter.ToList();
        }

        public bool InsertCenter(mCenter pCenter)
        {
            try
            {
                tbl_center vCenter = new tbl_center();
                var query = from b in Db.tbl_center
                            orderby b.center_id descending
                            select b;
                vCenter = query.FirstOrDefault();
                tbl_center a = new tbl_center();
                a.center_cat = pCenter.center_cat;
                a.center_city = pCenter.center_city;
                a.center_img = pCenter.center_img;
                a.center_info = pCenter.center_info;
                a.center_title = pCenter.center_title;
                a.center_id = vCenter.center_id + 1;
                Db.tbl_center.Add(a);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public mCenter fGetCenter(mCenter pCenter)
        {
            try
            {
                var vCenter = (from a in Db.tbl_center
                                 where a.center_id.Equals(pCenter.center_id)
                                 select new mCenter
                                 {
                                     center_id = a.center_id,
                                     center_cat = a.center_cat,
                                     center_city = a.center_city,
                                     center_title = a.center_title,
                                     center_info = a.center_info,
                                     center_img = a.center_img
                                 }).FirstOrDefault();
                return vCenter;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool fUpdateCenter(mCenter pCenter)
        {
            tbl_center a = new tbl_center();
            a.center_id = pCenter.center_id;
            a.center_cat = pCenter.center_cat;
            a.center_city = pCenter.center_city;
            a.center_img = pCenter.center_img;
            a.center_info = pCenter.center_info;
            a.center_title = pCenter.center_title;
            Db.tbl_center.Attach(a);
            Db.Entry(a).State = System.Data.Entity.EntityState.Modified;
            return Convert.ToBoolean(Db.SaveChanges());
        }
        public bool fDeleteCenter(int pId)
        {
            try
            {
                var vCenter = (from a in Db.tbl_center
                                 where a.center_id.Equals(pId)
                                 select a).OrderBy(a => a.center_id).SingleOrDefault();
                Db.tbl_center.Remove(vCenter);
                return Convert.ToBoolean(Db.SaveChanges());
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool IsDecimal(decimal pNum)
        {
            string pNumStr = pNum.ToString();
            if (pNumStr.Contains('.'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}