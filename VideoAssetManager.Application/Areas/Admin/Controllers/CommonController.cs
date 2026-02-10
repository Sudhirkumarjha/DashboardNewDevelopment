using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VideoAssetManager.CommonUtils;
using VideoAssetManager.DataAccess;
using VideoAssetManager.DataAccess.Repository.IRepository;
using VideoAssetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAssetManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = RekhtaUtility.Role_Admin)]
    public class CommonController : Controller
    {
        private readonly IWrapperRepository _iwrapperRepository;
        private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;

        public CommonController(IWrapperRepository iwrapperRepository, DataAccess.VideoAssetManagerDBContext VideoAssetManagerDBContext)
        {
            _iwrapperRepository = iwrapperRepository;
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
        }

        public IActionResult Index()
        {
            return View();
        }
                

        [HttpGet]
        public IActionResult SetTabName(string TabName)
        {
            int status = 0;

            var TabMenu = _VideoAssetManagerDBContext.TabMenu.FirstOrDefault(a => a.MenuName.Replace(" ", "") == TabName);
            if (TabMenu != null)
            {
                VideoAssetManager.CommonUtils.RekhtaUtility.GetProperty.TabMenuId = TabMenu.MenuId;
            }

            var jsonData = new
            {
                Status = status
            };
            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult ReSetTabMenuId()
        {
            int status = 0;
            VideoAssetManager.CommonUtils.RekhtaUtility.GetProperty.TabMenuId = 0;

            var jsonData = new
            {
                Status = status
            };
            return Json(jsonData);
        }


        [HttpGet]
        public IActionResult IsFromPublishCourse(int Id, string Name, string Flag)
        {
            int status = 0;
            VideoAssetManager.CommonUtils.RekhtaUtility.GetProperty.TabMenuId = 0;
            if (!string.IsNullOrEmpty(Flag))
            {
                RekhtaUtility.GetProperty.isFromPublishCourse = false;
                RekhtaUtility.GetProperty.LinkId = Id;
                RekhtaUtility.GetProperty.LinkName = Name;
            }
            else
            {
                RekhtaUtility.GetProperty.LinkId = Id;
                RekhtaUtility.GetProperty.LinkName = Name;
            }

            var jsonData = new
            {
                Status = status
            };
            return Json(jsonData);
        }

    }
}