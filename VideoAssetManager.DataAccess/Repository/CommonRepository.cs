using Microsoft.AspNetCore.Mvc.Rendering;
using VideoAssetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoAssetManager.DataAccess.Repository
{
    public class CommonRepository
    {
        private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;
        public CommonRepository(VideoAssetManagerDBContext VideoAssetManagerDBContext) : base()
        {
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
        }

        //public List<SelectListItem> ContentListCommon(int ResourceId = 0,string type="")
        //{
        //    List<SelectListItem> contentitems = new List<SelectListItem>();
        //    IEnumerable<ContentMaster> ContentMaster = null;
        //    if(!string.IsNullOrEmpty(type))
        //    {
        //        ContentMaster = _VideoAssetManagerDBContext.ContentMaster.Where(c => c.ContentType == type).ToList();
        //    }
        //    else
        //    {
        //        ContentMaster = _VideoAssetManagerDBContext.ContentMaster.ToList();
        //    }
        //    foreach (var content in ContentMaster)
        //    {
        //        if (ResourceId == content.ContentId)
        //        {
        //            contentitems.Add(new SelectListItem
        //            {
        //                Text = content.ContentName,
        //                Value = content.ContentId.ToString(),
        //                Selected = true
        //            });
        //        }
        //        else
        //        {
        //            contentitems.Add(new SelectListItem
        //            {
        //                Text = content.ContentName,
        //                Value = content.ContentId.ToString(),
        //            });
        //        }

        //    }
        //    return contentitems;
        //}
    }
}
