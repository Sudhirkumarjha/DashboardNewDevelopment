using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VideoAssetManager.CommonUtils;
using VideoAssetManager.DataAccess;
using VideoAssetManager.DataAccess.Repository.IRepository;
using VideoAssetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAssetManager.Areas.Admin.Filters
{
    public class CheckPermission : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string action = string.Empty;
            string controller = string.Empty;

            if (filterContext.RouteData.Values["Action"] != null) 
                action = filterContext.RouteData.Values["Action"].ToString();
            if (filterContext.RouteData.Values["controller"] != null)
                controller = filterContext.RouteData.Values["controller"].ToString();



            if (!RekhtaUtility.IsPermission(action, controller) && VideoAssetManager.CommonUtils.RekhtaUtility.GetProperty.TabMenuId==0)
            {
                if(!string.IsNullOrEmpty(RekhtaUtility.GetProperty.siteUrl))
                    filterContext.Result = new RedirectResult(RekhtaUtility.GetProperty.siteUrl);
            }
            else if (!RekhtaUtility.IsTabPermission() && VideoAssetManager.CommonUtils.RekhtaUtility.GetProperty.TabMenuId!=0)
            {
                if (!string.IsNullOrEmpty(RekhtaUtility.GetProperty.siteUrl))
                    filterContext.Result = new RedirectResult(RekhtaUtility.GetProperty.siteUrl);
            }
        }
    }
}
