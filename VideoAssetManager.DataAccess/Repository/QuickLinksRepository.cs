using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VideoAssetManager.CommonUtils;
using VideoAssetManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace VideoAssetManager.DataAccess.Repository
{
    [Area("Admin")]
    [Authorize(Roles = RekhtaUtility.Role_Admin)]
    public class QuickLinksRepository
    {
        private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;
        public QuickLinksRepository(VideoAssetManagerDBContext VideoAssetManagerDBContext) : base()
        {
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
        }

        public List<QuickLinkMaster> GetQuickLink()
        {
            string RoleName = string.Empty;
            if (string.IsNullOrEmpty(RekhtaUtility.RoleName))
                RoleName = "Admin";
            else
                RoleName = VideoAssetManager.CommonUtils.RekhtaUtility.RoleName;

            IQueryable<QuickLinkMaster> QuickLink = _VideoAssetManagerDBContext.QuickLinkMaster.Where(a => a.QuickLinkParentId == 0 && a.IsDeleted==false);

            string RoleId = GetUserRolesId();
            var Permission = _VideoAssetManagerDBContext.QuicklinkPermission.Where(a => a.RoleId == RoleId);
            if (RoleName.ToLower() == "admin")
            {
                List<QuickLinkMaster> QuickLinkPermission = (from ql in QuickLink
                                                             join p in Permission
                                                             on ql.QuickLinkId equals p.QuicklinkId
                                                             select new QuickLinkMaster
                                                             {
                                                                 QuickLinkId = ql.QuickLinkId,
                                                                 QuickLinkParentId = ql.QuickLinkParentId,
                                                                 QuickLinkName = ql.QuickLinkName,
                                                                 Type = ql.Type,
                                                                 Action = ql.Action,
                                                                 Area = ql.Area,
                                                                 OrderNo = ql.OrderNo,
                                                                 IsDeleted = ql.IsDeleted
                                                                 //PermissionId = p.PermissionId
                                                             })
                                                                 .OrderBy(q => q.OrderNo)
                                                                 .ThenBy(q => q.QuickLinkId)
                                                             .ToList();
                //if (RoleName.ToLower() == "super admin")
                //  QuickLinkPermission = QuickLinkPermission.Where(a => a.QuickLinkName == "Event Management").ToList();
                return QuickLinkPermission;
            }
            else
            {
                List<QuickLinkMaster> QuickLinkPermission = (from ql in QuickLink
                                                             join p in Permission
                                                             on ql.QuickLinkId equals p.QuicklinkId                                                             
                                                             select new QuickLinkMaster
                                                             {
                                                                 QuickLinkId = ql.QuickLinkId,
                                                                 QuickLinkParentId = ql.QuickLinkParentId,
                                                                 QuickLinkName = ql.QuickLinkName,
                                                                 Type = ql.Type,
                                                                 Action = ql.Action,
                                                                 Area = ql.Area,
                                                                 OrderNo = ql.OrderNo,
                                                                 IsDeleted = ql.IsDeleted,
                                                                 //PermissionId=link.PermissionId
                                                             }).ToList();


                return QuickLinkPermission;

            }            
        }
        public string GetUserRolesId()
        {
            string RolesId = "";
            string RoleName = RekhtaUtility.RoleName;
            IQueryable<UserRoles> UserRolesDetails = this._VideoAssetManagerDBContext.UserRoles("", RoleName);
            foreach (var role in UserRolesDetails)
            {
                RolesId = role.Id;
            }
            return RolesId;
        }
        public string GetUserRolesName(string RoleId)
        {
            string RolesName= "";
            IQueryable<UserRoles> UserRolesDetails = this._VideoAssetManagerDBContext.UserRoles(RoleId, "");
            foreach (var role in UserRolesDetails)
            {
                RolesName = role.Name;
            }
            return RolesName;
        }

        public List<QuickLinkMaster> GetQuickSubLink(int linkId)
        {
            string QuickLinkName = string.Empty;
            string RoleName = string.Empty;
            if (string.IsNullOrEmpty(RekhtaUtility.RoleName))
                RoleName = "Admin";
            else
                RoleName = VideoAssetManager.CommonUtils.RekhtaUtility.RoleName;


            List<QuickLinkMaster> QuickLinkPermission = null;
            try
            {
                IQueryable<QuickLinkMaster> QuickLink = _VideoAssetManagerDBContext.QuickLinkMaster.Where(a => a.QuickLinkParentId == linkId && a.IsDeleted == false);

                string RoleId = GetUserRolesId();
                var Permission = _VideoAssetManagerDBContext.QuicklinkPermission.Where(a => a.RoleId == RoleId);
                 if(RoleName.ToLower() == "admin")
                {
                    QuickLinkPermission = (from ql in QuickLink
                                           join p in Permission
                                           on ql.QuickLinkId equals p.QuicklinkId
                                           into qlink
                                           from link in qlink.DefaultIfEmpty()
                                           select new QuickLinkMaster
                                           {
                                               QuickLinkId = ql.QuickLinkId,
                                               QuickLinkParentId = ql.QuickLinkParentId,
                                               QuickLinkName = ql.QuickLinkName,
                                               Type = ql.Type,
                                               Action = ql.Action,
                                               Area = ql.Area,
                                               OrderNo = ql.OrderNo,
                                               IsDeleted = ql.IsDeleted
                                           })
                                           .OrderBy(q => q.OrderNo)
                                           .ThenBy(q => q.QuickLinkId)
                                           .ToList();


                    //if (RoleName.ToLower() == "super admin")
                    //    QuickLinkPermission = QuickLinkPermission.Where(a => (a.Action == "VideoAssetManager" || a.Action == "GetYourFreePass") && a.Area == "EventMasters").ToList();
                    //else
                    //    QuickLinkPermission = QuickLinkPermission.Where(a => a.Action != "VideoAssetManager" && a.Action != "GetYourFreePass").ToList();

                    return QuickLinkPermission;
                }
                else
                {
                    QuickLinkPermission = (from ql in QuickLink
                                           join p in Permission
                                           on ql.QuickLinkId equals p.QuicklinkId
                                           select new QuickLinkMaster
                                           {
                                               QuickLinkId = ql.QuickLinkId,
                                               QuickLinkParentId = ql.QuickLinkParentId,
                                               QuickLinkName = ql.QuickLinkName,
                                               Type = ql.Type,
                                               Action = ql.Action,
                                               Area = ql.Area,
                                               OrderNo = ql.OrderNo,
                                               IsDeleted = ql.IsDeleted
                                           })
                                            .OrderBy(q => q.OrderNo)
                                           .ThenBy(q => q.QuickLinkId)
                                           .ToList();




                    return QuickLinkPermission;
                }
               


            }
            catch (Exception ex)
            {
                string strErrorMsg = ex.Message;
            }


            return QuickLinkPermission;
        }
        public void GetQuicklinkIds()
        {
            try
            {
                string QuickLinkId = string.Empty;
                string Controller = string.Empty;
                List<QuickLinkMaster> QuickLinkPermission = null;
                List<QuickLinkMaster> QuickLink = _VideoAssetManagerDBContext.QuickLinkMaster.Where(a => a.QuickLinkParentId != 0 && a.IsDeleted == false).ToList();

                string RoleId = GetUserRolesId();
                var Permission = _VideoAssetManagerDBContext.QuicklinkPermission.Where(a => a.RoleId == RoleId);

                QuickLinkPermission = (from ql in QuickLink
                                       join p in Permission on ql.QuickLinkId equals p.QuicklinkId
                                       select ql).ToList();

               
                var convertQuickLink = QuickLinkPermission.Select(x => new RekhtaUtility.QuickLinkPermission
                {
                    QuickLinkId = x.QuickLinkId,
                    QuickLinkParentId=x.QuickLinkParentId,
                    QuickLinkName=x.QuickLinkName,
                    Action=x.Action,
                    Area=x.Area
                });

                RekhtaUtility.GetProperty.QuickLinkPermission = convertQuickLink;

            }
            catch (Exception ex)
            {
                string strErrorMsg = ex.Message;
            }

        }
        public void SetTabLinks()
        {
            
            string RoleId = GetUserRolesId();
             IQueryable<Sp_GetTabMenuPermission> TabMenu = null;
            try
            {
                string StoredProc = "exec Sp_GetTabMenuLink " +
                  "@RoleId='" + RoleId + "'";


              
                var TabMenus = _VideoAssetManagerDBContext.TabMenuPermission.ToList();

                var convertTabLink = TabMenus.Select(x => new RekhtaUtility.TabLinkPermission
                {
                    MenuId = x.TabMenuId,                   
                    RoleId = x.RoleId,

                });


                RekhtaUtility.GetProperty.TabLinkPermission = convertTabLink;
            }
            catch(Exception ex)
            {
                string strErrorMsg = ex.Message;
            }
            

        }
        public string GetUserRoleName()
        {
            string RoleId = GetUserRolesId();
            string RoleName = GetUserRolesName(RoleId);
            RekhtaUtility.GetProperty.UserRoleName = RoleName;
            return RoleName;
        }
        
        public IQueryable<QuickLinkMaster> GetParentQuickLink()
        {
            IQueryable<QuickLinkMaster> QuickLink = _VideoAssetManagerDBContext.QuickLinkMaster.Where(a => a.QuickLinkParentId == 0 && a.IsDeleted == false);

            return QuickLink;
        }
        public List<QuickLinkMaster> GetSubQuickLink(int linkId)
        {
            string QuickLinkName = string.Empty;
            List<QuickLinkMaster> QuickLink = new List<QuickLinkMaster>();
            try
            {
                QuickLink = _VideoAssetManagerDBContext.QuickLinkMaster.Where(a => a.QuickLinkParentId == linkId && a.IsDeleted == false && a.QuickLinkId!=3).ToList();

                return QuickLink;

            }
            catch (Exception ex)
            {
                string strErrorMsg = ex.Message;
            }


            return QuickLink;
        }
        public List<TabMenu> GetTabkLink(int linkId)
        {
            string QuickLinkName = string.Empty;
            List<TabMenu> QuickLink = new List<TabMenu>();
            try
            {
                QuickLink = _VideoAssetManagerDBContext.TabMenu.Where(a => a.QuickLinkId == linkId && a.IsSoftDelete == false).ToList();

                return QuickLink;

            }
            catch (Exception ex)
            {
                string strErrorMsg = ex.Message;
            }


            return QuickLink;
        }

        public bool GetQuickLinkId(string RoleId,int QuicklinkId)
        {
            bool QuicklinkExists = false;
            var Permission = _VideoAssetManagerDBContext.QuicklinkPermission.FirstOrDefault(a => a.RoleId == RoleId && a.QuicklinkId== QuicklinkId);
            
            if(Permission!=null)
            {
                QuicklinkExists = true;
            }
            
            return QuicklinkExists;
        }
        public bool GetTabLinkId(string RoleId, int TablinkId)
        {
            bool QuicklinkExists = false;
            var Permission = _VideoAssetManagerDBContext.TabMenuPermission.FirstOrDefault(a => a.RoleId == RoleId && a.TabMenuId == TablinkId);

            if (Permission != null)
            {
                QuicklinkExists = true;
            }

            return QuicklinkExists;
        }

        public IQueryable<Sp_GetTabMenu> GetTabMenu(string controllerName,string actionName)
        {
            IQueryable<Sp_GetTabMenu> TabMenu = null;
            string roleName = "kids";
            if (RekhtaUtility.RoleName == null || string.IsNullOrEmpty(RekhtaUtility.RoleName) || !RekhtaUtility.RoleName.Contains(roleName))
            {
                roleName = string.Empty;
            }
            try
            {
                string StoredProc = "exec Sp_GetTabMenu " +
                  "@ControllerName='" + controllerName + "',"+
                  "@ActionName='" + actionName + "',"+
                  "@RoleName='" + roleName + "'";

                 TabMenu = _VideoAssetManagerDBContext.GetTabMenu.FromSqlRaw(StoredProc);


                return TabMenu;
            }
            catch(Exception ex)
            {
                string strMsg = ex.Message;
                return TabMenu;
            } 
        }

        public bool GetStageId(Guid Id, int id)
        {
            bool presenterExists = false;
            var Permission = _VideoAssetManagerDBContext.VM_StageLookup.FirstOrDefault(a => a.StageId == Id && a.ProjectId == id);

            if (Permission != null)
            {
                presenterExists = true;
            }

            return presenterExists;
        }

    }
}