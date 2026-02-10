using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VideoAssetManager.CommonUtils;
using VideoAssetManager.DataAccess;
using VideoAssetManager.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAssetManager.Areas.Admin.Common
{
    //[Area("Admin")]
    //[Authorize(Roles = RekhtaUtility.Role_Admin)]

    public class Common
    {
    //    private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;
        private readonly IWrapperRepository _iwrapperRepository;
       
        public Common(IWrapperRepository iwrapperRepository) : base()
        {
            _iwrapperRepository = iwrapperRepository;
        }

        [HttpGet]
        public bool Ispermission(string TabName, string controllerName)
        {
            bool Ispermission = false;
            var TabMenu = _iwrapperRepository.TabMenu.GetFirstOrDefault(a => a.TabdivId == TabName && a.area.Contains(controllerName));
            if (TabMenu != null)
            {
                var chekcPermission = RekhtaUtility.GetProperty.TabLinkPermission.FirstOrDefault(a => a.MenuId == TabMenu.MenuId);
                if (chekcPermission != null)
                {
                    Ispermission = true;
                }
            }
            return Ispermission;
        }
    }
}
