using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nancy.Json;
using VideoAssetManager.Areas.Admin.Filters;
using VideoAssetManager.Business;
using VideoAssetManager.CommonUtils;
using VideoAssetManager.CommonUtils.Configuration;
using VideoAssetManager.DataAccess;
using VideoAssetManager.DataAccess.Repository.IRepository;
using VideoAssetManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAssetManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    //  [Authorize(Roles = RekhtaUtility.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IWrapperRepository _iwrapperRepository;
        private readonly VideoAssetManagerDBContext _Context;
        private readonly AppConfig _appConfig;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;



        public UserController(IWrapperRepository iwrapperRepository, VideoAssetManagerDBContext Context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<AppConfig> appConfig)
        {
            _iwrapperRepository = iwrapperRepository;
            _appConfig = appConfig.Value;
            _Context = Context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [CheckPermission]
        public IActionResult ManageUser()
        {
            ViewData["appurl"] = _appConfig.URLPaths.ApplicationAdminPath;
            return View();
        }

        [HttpGet]
        public JsonResult UserDetails(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int Userlistpageid)
        {
            int irows = Convert.ToInt16(rows);


            var details = GetManageContentList(sidx, sord, page, rows, _search, filters, TopSearch, Userlistpageid);
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = irows;

            int totalRecords = details.Count();
            details = details.Skip(pageIndex * pageSize).Take(pageSize);

            var jsonData = new
            {
                total = (int)Math.Ceiling((float)totalRecords / (float)irows),
                page,
                records = totalRecords,
                rows = details
            };

            return Json(jsonData);

        }

        private IEnumerable<UserDetail> GetManageContentList(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int Userlistpageid)
        {

            IEnumerable<UserDetail> userMaster = null;
            IEnumerable<UserMaster> userData = null;
            IEnumerable<AspNetRoles> aspnetroles = null;
            IEnumerable<AspNetUserRoles> aspnetuserroles = null;

            userMaster = _iwrapperRepository.UserDetail.GetAll();
            userData = _iwrapperRepository.UserMaster.GetAll();
            aspnetroles = _iwrapperRepository.AspNetRoles.GetAll();
            aspnetuserroles = _iwrapperRepository.AspNetUserRoles.GetAll();


            IEnumerable<UserDetail> userDetails = (from userDetailData in userMaster
                                                   join userMasterData in userData on userDetailData.Id equals userMasterData.Id
                                                   join aspnetuserData in aspnetuserroles on userDetailData.Id equals aspnetuserData.UserId
                                                   join aspnetroleData in aspnetroles on aspnetuserData.RoleId equals aspnetroleData.Id
                                                   select new UserDetail
                                                   {
                                                       UserId = userDetailData.UserId,
                                                       Name = userMasterData.Name,
                                                       UserType = userDetailData.UserType.Trim().ToUpper() == "N" ? "Normal" : userDetailData.UserType,
                                                       Email = userDetailData.Email,
                                                       MobileNumber = userDetailData.MobileNumber ?? "",
                                                       City = userDetailData.City ?? "",
                                                       State = userDetailData.State ?? "",
                                                       Active = userDetailData.Active,
                                                       RoleName = aspnetroleData.Name,
                                                       CreatedOn = userDetailData.CreatedOn.Date,
                                                       ModifiedOn = userDetailData.ModifiedOn.Date
                                                   }).ToList();

            if (_search)
            {
                if (!string.IsNullOrEmpty(filters))
                {
                    Rootobject searchobject = new JavaScriptSerializer().Deserialize<Rootobject>(filters);

                    if (searchobject != null)
                    {
                        foreach (Rules rule in searchobject.rules)
                        {
                            string data = rule.data.ToLower().Trim();
                            if (rule.field == "name")
                            {
                                if (rule.op == "cn")
                                {
                                    userDetails = userDetails.Where(x => x.Name.ToLower().Contains(data));
                                }
                                else if (rule.op == "nc")
                                {
                                    userDetails = userDetails.Where(x => !x.Name.ToLower().Contains(data));
                                }
                                else if (rule.op == "eq")
                                {
                                    userDetails = userDetails.Where(x => x.Name.ToLower().Trim() == data);
                                }
                                else if (rule.op == "ne")
                                {
                                    userDetails = userDetails.Where(x => x.Name.ToLower().Trim() != data);
                                }
                                else if (rule.op == "nu")
                                {
                                    userDetails = userDetails.Where(x => x.Name.Trim() == "");
                                }
                                else if (rule.op == "nn")
                                {
                                    userDetails = userDetails.Where(x => x.Name.Trim() != "");
                                }
                            }
                            if (rule.field == "userType")
                            {
                                if (rule.op == "cn")
                                {
                                    userDetails = userDetails.Where(x => x.UserType.ToLower().Contains(data));
                                }
                                else if (rule.op == "nc")
                                {
                                    userDetails = userDetails.Where(x => !x.UserType.ToLower().Contains(data));
                                }
                                else if (rule.op == "eq")
                                {
                                    userDetails = userDetails.Where(x => x.UserType.ToLower().Trim() == data);
                                }
                                else if (rule.op == "ne")
                                {
                                    userDetails = userDetails.Where(x => x.UserType.ToLower().Trim() != data);
                                }
                                else if (rule.op == "nu")
                                {
                                    userDetails = userDetails.Where(x => x.UserType.Trim() == "");
                                }
                                else if (rule.op == "nn")
                                {
                                    userDetails = userDetails.Where(x => x.UserType.Trim() != "");
                                }
                            }
                            if (rule.field == "email")
                            {
                                if (rule.op == "cn")
                                {
                                    userDetails = userDetails.Where(x => x.Email.ToLower().Contains(data));
                                }
                                else if (rule.op == "nc")
                                {
                                    userDetails = userDetails.Where(x => !x.Email.ToLower().Contains(data));
                                }
                                else if (rule.op == "eq")
                                {
                                    userDetails = userDetails.Where(x => x.Email.ToLower().Trim() == data);
                                }
                                else if (rule.op == "ne")
                                {
                                    userDetails = userDetails.Where(x => x.Email.ToLower().Trim() != data);
                                }
                                else if (rule.op == "nu")
                                {
                                    userDetails = userDetails.Where(x => x.Email.Trim() == "");
                                }
                                else if (rule.op == "nn")
                                {
                                    userDetails = userDetails.Where(x => x.Email.Trim() != "");
                                }
                            }
                            if (rule.field == "mobileNumber")
                            {
                                if (rule.op == "cn")
                                {
                                    userDetails = userDetails.Where(x => x.MobileNumber.ToLower().Contains(data));
                                }
                                else if (rule.op == "nc")
                                {
                                    userDetails = userDetails.Where(x => !x.MobileNumber.ToLower().Contains(data));
                                }
                                else if (rule.op == "eq")
                                {
                                    userDetails = userDetails.Where(x => x.MobileNumber.ToLower().Trim() == data);
                                }
                                else if (rule.op == "ne")
                                {
                                    userDetails = userDetails.Where(x => x.MobileNumber.ToLower().Trim() != data);
                                }
                                else if (rule.op == "nu")
                                {
                                    userDetails = userDetails.Where(x => x.MobileNumber.Trim() == "");
                                }
                                else if (rule.op == "nn")
                                {
                                    userDetails = userDetails.Where(x => x.MobileNumber.Trim() != "");
                                }
                            }
                            if (rule.field == "city")
                            {
                                if (rule.op == "cn")
                                {
                                    userDetails = userDetails.Where(x => x.City.ToLower().Contains(data));
                                }
                                else if (rule.op == "nc")
                                {
                                    userDetails = userDetails.Where(x => !x.City.ToLower().Contains(data));
                                }
                                else if (rule.op == "eq")
                                {
                                    userDetails = userDetails.Where(x => x.City.ToLower().Trim() == data);
                                }
                                else if (rule.op == "ne")
                                {
                                    userDetails = userDetails.Where(x => x.City.ToLower().Trim() != data);
                                }
                                else if (rule.op == "nu")
                                {
                                    userDetails = userDetails.Where(x => x.City.Trim() == "");
                                }
                                else if (rule.op == "nn")
                                {
                                    userDetails = userDetails.Where(x => x.City.Trim() != "");
                                }
                            }
                            if (rule.field == "state")
                            {
                                if (rule.op == "cn")
                                {
                                    userDetails = userDetails.Where(x => x.State.ToLower().Contains(data));
                                }
                                else if (rule.op == "nc")
                                {
                                    userDetails = userDetails.Where(x => !x.State.ToLower().Contains(data));
                                }
                                else if (rule.op == "eq")
                                {
                                    userDetails = userDetails.Where(x => x.State.ToLower().Trim() == data);
                                }
                                else if (rule.op == "ne")
                                {
                                    userDetails = userDetails.Where(x => x.State.ToLower().Trim() != data);
                                }
                                else if (rule.op == "nu")
                                {
                                    userDetails = userDetails.Where(x => x.State.Trim() == "");
                                }
                                else if (rule.op == "nn")
                                {
                                    userDetails = userDetails.Where(x => x.State.Trim() != "");
                                }
                            }

                            if (rule.field == "createdOn")
                            {
                                DateTime dt_data = new DateTime();
                                if (!string.IsNullOrEmpty(rule.data))
                                {
                                    dt_data = Convert.ToDateTime(rule.data);
                                }

                                if (rule.op == "gt")
                                {
                                    userDetails = userDetails.Where(x => x.CreatedOn > dt_data);
                                }
                                else if (rule.op == "lt")
                                {
                                    userDetails = userDetails.Where(x => x.CreatedOn < dt_data);
                                }
                                else if (rule.op == "ge")
                                {
                                    userDetails = userDetails.Where(x => x.CreatedOn >= dt_data);
                                }
                                else if (rule.op == "le")
                                {
                                    userDetails = userDetails.Where(x => x.CreatedOn <= dt_data);
                                }
                                else if (rule.op == "eq")
                                {
                                    userDetails = userDetails.Where(x => x.CreatedOn.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) == dt_data.ToString("dd/M/yyyy", CultureInfo.InvariantCulture));
                                }
                                else if (rule.op == "ne")
                                {
                                    userDetails = userDetails.Where(x => x.CreatedOn.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) != dt_data.ToString("dd/M/yyyy", CultureInfo.InvariantCulture));
                                }
                            }
                            if (rule.field == "modifiedOn")
                            {
                                DateTime dt_data = new DateTime();
                                if (!string.IsNullOrEmpty(rule.data))
                                {
                                    dt_data = Convert.ToDateTime(rule.data);
                                }

                                if (rule.op == "gt")
                                {
                                    userDetails = userDetails.Where(x => x.ModifiedOn > dt_data);
                                }
                                else if (rule.op == "lt")
                                {
                                    userDetails = userDetails.Where(x => x.ModifiedOn < dt_data);
                                }
                                else if (rule.op == "ge")
                                {
                                    userDetails = userDetails.Where(x => x.ModifiedOn >= dt_data);
                                }
                                else if (rule.op == "le")
                                {
                                    userDetails = userDetails.Where(x => x.ModifiedOn <= dt_data);
                                }
                                else if (rule.op == "eq")
                                {
                                    userDetails = userDetails.Where(x => x.ModifiedOn.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) == dt_data.ToString("dd/M/yyyy", CultureInfo.InvariantCulture));
                                }
                                else if (rule.op == "ne")
                                {
                                    userDetails = userDetails.Where(x => x.ModifiedOn.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) != dt_data.ToString("dd/M/yyyy", CultureInfo.InvariantCulture));
                                }
                            }
                            if (rule.field == "Active")
                            {
                                bool bool_data = false;
                                bool.TryParse(rule.data, out bool_data);
                                userDetails = userDetails.Where(x => x.Active == bool_data);
                            }
                        }


                    }
                }
            }

            if (!string.IsNullOrEmpty(TopSearch))
            {
                string data = TopSearch.ToLower().Trim();

                DateTime dt_data = new DateTime();
                if (DateTime.TryParseExact(data, "dd-M-yy", null, System.Globalization.DateTimeStyles.None, out dt_data))
                {
                    userDetails = userDetails.Where(x => (x.CreatedOn.Day == dt_data.Day && x.CreatedOn.Month == dt_data.Month && x.CreatedOn.Year == dt_data.Year) || (x.ModifiedOn.Day == dt_data.Day && x.ModifiedOn.Month == dt_data.Month && x.ModifiedOn.Year == dt_data.Year));
                }
                else
                {
                    userDetails = userDetails.Where(x =>
                                x.UserType.ToLower().Contains(data) || x.Email.ToLower().Contains(data) || x.MobileNumber.ToLower().Contains(data) || x.City.ToLower().Contains(data) || x.State.ToLower().Contains(data) || x.Name.ToLower().Contains(data));

                }
            }
            if (sord == "desc")
            {
                switch (sidx)
                {
                    case "name":
                        userDetails = userDetails.OrderByDescending(s => s.Name);
                        break;
                    case "email":
                        userDetails = userDetails.OrderByDescending(s => s.Email);
                        break;
                    case "mobileNumber":
                        userDetails = userDetails.OrderByDescending(s => s.MobileNumber);
                        break;
                    case "city":
                        userDetails = userDetails.OrderByDescending(s => s.City);
                        break;
                    case "state":
                        userDetails = userDetails.OrderByDescending(s => s.State);
                        break;
                    case "userType":
                        userDetails = userDetails.OrderByDescending(s => s.UserType);
                        break;
                    case "Active":
                        userDetails = userDetails.OrderByDescending(s => s.Active);
                        break;
                    case "createdOn":
                        userDetails = userDetails.OrderByDescending(s => s.CreatedOn);
                        break;
                    case "modifiedOn":
                        userDetails = userDetails.OrderByDescending(s => s.ModifiedOn);
                        break;
                }
            }
            else
            {
                switch (sidx)
                {
                    case "name":
                        userDetails = userDetails.OrderBy(s => s.Name);
                        break;
                    case "email":
                        userDetails = userDetails.OrderBy(s => s.Email);
                        break;
                    case "mobileNumber":
                        userDetails = userDetails.OrderBy(s => s.MobileNumber);
                        break;
                    case "city":
                        userDetails = userDetails.OrderBy(s => s.City);
                        break;
                    case "state":
                        userDetails = userDetails.OrderBy(s => s.State);
                        break;
                    case "userType":
                        userDetails = userDetails.OrderBy(s => s.UserType);
                        break;
                    case "Active":
                        userDetails = userDetails.OrderBy(s => s.Active);
                        break;
                    case "createdOn":
                        userDetails = userDetails.OrderBy(s => s.CreatedOn);
                        break;
                    case "modifiedOn":
                        userDetails = userDetails.OrderBy(s => s.ModifiedOn);
                        break;
                }
            }
            return userDetails;
        }

        public IActionResult UserRole()
        {
            return View();
        }

        public JsonResult UserRolesDetails(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int categoryListPageId)
        {
            int irows = Convert.ToInt16(rows);
            var details = GetserRolesList(sidx, sord, page, rows, _search, filters, TopSearch, categoryListPageId);

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = irows;
            int totalRecords = details.Count();
            var jsonData = new
            {
                total = (int)Math.Ceiling((float)totalRecords / (float)irows),
                page,
                records = totalRecords,
                rows = details
            };
            return Json(jsonData);
        }

        private IEnumerable<UserRoles> GetserRolesList(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int categoryListPageId)
        {
            IEnumerable<UserRoles> UserRolesDetails = this._Context.UserRoles("", "");

            if (_search)
            {
                if (!string.IsNullOrEmpty(filters))
                {
                    Rootobject searchobject = new JavaScriptSerializer().Deserialize<Rootobject>(filters);

                    if (searchobject != null)
                    {
                        foreach (Rules rule in searchobject.rules)
                        {
                            string data = rule.data.ToLower().Trim();
                            if (rule.field == "name")
                            {
                                if (rule.op == "cn")
                                {
                                    UserRolesDetails = UserRolesDetails.Where(x => x.Name.ToLower().Contains(data));
                                }
                                else if (rule.op == "nc")
                                {
                                    UserRolesDetails = UserRolesDetails.Where(x => !x.Name.ToLower().Contains(data));
                                }
                                else if (rule.op == "eq")
                                {
                                    UserRolesDetails = UserRolesDetails.Where(x => x.Name.ToLower().Trim() == data);
                                }
                                else if (rule.op == "ne")
                                {
                                    UserRolesDetails = UserRolesDetails.Where(x => x.Name.ToLower().Trim() != data);
                                }
                                else if (rule.op == "nu")
                                {
                                    UserRolesDetails = UserRolesDetails.Where(x => x.Name.Trim() == "");
                                }
                                else if (rule.op == "nn")
                                {
                                    UserRolesDetails = UserRolesDetails.Where(x => x.Name.Trim() != "");
                                }
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(TopSearch))
            {
                string data = TopSearch.ToLower().Trim();
                UserRolesDetails = UserRolesDetails.Where(x =>
                                x.Name.ToLower().Contains(data));
            }
            if (sord == "desc")
            {
                switch (sidx)
                {
                    case "name":
                        UserRolesDetails = UserRolesDetails.OrderByDescending(s => s.Name);
                        break;

                }
            }
            else
            {
                switch (sidx)
                {
                    case "name":
                        UserRolesDetails = UserRolesDetails.OrderBy(s => s.Name);
                        break;

                }
            }
            return UserRolesDetails;
        }

        //public IActionResult SaveEdit(string oper, string id = "")
        //{
        //    try
        //    {
        //        if (oper == "del")
        //        {
        //            DataAccessLearning objDataAccess = new DataAccessLearning();
        //            objDataAccess.DeleteRoleAndPermission(id);
        //        }

        //        //IEnumerable<AspNetRoles> UserRoles = _iwrapperRepository.AspNetRoles.GetAll();
        //        //if (oper != "del")
        //        //{
        //        //    var UserDetails = UserRoles.FirstOrDefault(c => c.Id == Id);
        //        //    if (UserDetails != null)
        //        //    {
        //        //        UserDetails.Name = category.Name;
        //        //        _iwrapperRepository.AspNetRoles.Update(UserDetails);
        //        //        _iwrapperRepository.Save();
        //        //    }
        //        //    TempData["success"] = "Category updated successfully";
        //        //}
        //        //else
        //        //{
        //        //    var UserDetails = UserRoles.FirstOrDefault(c => c.Id == Id);
        //        //    _iwrapperRepository.AspNetRoles.Remove(UserDetails);
        //        //    _iwrapperRepository.Save();
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //    return StatusCode(StatusCodes.Status200OK);
        //}

        public IActionResult CreateUserRole(string RoleId)
        {
            string Name = string.Empty;
            string UserRoleId = string.Empty;

            if (!string.IsNullOrEmpty(RoleId))
            {
                var UserRolesDetails = _iwrapperRepository.QuicklinkPermission.GetAll().FirstOrDefault(a => a.RoleId == RoleId);
                IQueryable<UserRoles> UserRoles = this._Context.UserRoles(RoleId, "");
                foreach (var itm in UserRoles)
                {
                    if (UserRoles != null)
                    {
                        if (!string.IsNullOrEmpty(itm.Name))
                        {
                            Name = itm.Name;
                            UserRoleId = itm.Id;
                        }
                        else
                        {
                            Name = "";
                            UserRoleId = "";
                        }
                    }
                }
                if (UserRolesDetails != null)
                {
                    UserRolesDetails.RoleName = Name;
                    ViewBag.RoleName = Name;
                    ViewBag.UserRoleId = UserRoleId;
                    RekhtaUtility.GetProperty.UserRoleName = Name;
                    RekhtaUtility.GetProperty.UserRoleId = UserRoleId;
                    ViewBag.QuickLinkIds = GetQuickLinkId(UserRoleId);
                    return View(UserRolesDetails);
                }
                else
                {
                    ViewBag.RoleName = Name;
                    ViewBag.UserRoleId = RoleId;
                    RekhtaUtility.GetProperty.UserRoleName = Name;
                    RekhtaUtility.GetProperty.UserRoleId = UserRoleId;
                    return View();
                }
            }
            else
            {
                ViewBag.RoleName = Name;
                ViewBag.UserRoleId = RoleId;
                RekhtaUtility.GetProperty.UserRoleName = Name;
                RekhtaUtility.GetProperty.UserRoleId = UserRoleId;
                return View();
            }


        }

        public void DeletePermission(string RoleId)
        {
            IEnumerable<QuicklinkPermission> Permission = _iwrapperRepository.QuicklinkPermission.GetAll().Where(a => a.RoleId == RoleId).ToList();
            foreach (var itm in Permission)
            {
                _iwrapperRepository.QuicklinkPermission.Remove(itm);
                _iwrapperRepository.Save();
            }
        }

        public void DeleteTabPermission(string RoleId)
        {
            IEnumerable<TabMenuPermission> Permission = _iwrapperRepository.TabMenuPermission.GetAll().Where(a => a.RoleId == RoleId).ToList();
            foreach (var itm in Permission)
            {
                _iwrapperRepository.TabMenuPermission.Remove(itm);
                _iwrapperRepository.Save();
            }
        }

        public string GetQuickLinkId(string RoleId)
        {
            IEnumerable<QuicklinkPermission> Permission = _iwrapperRepository.QuicklinkPermission.GetAll().Where(a => a.RoleId == RoleId).ToList();
            string QuickLinkIds = string.Empty;
            foreach (var itm in Permission)
            {
                QuickLinkIds += itm.QuicklinkId + ",";
            }
            if (QuickLinkIds != "")
                QuickLinkIds = QuickLinkIds.Substring(0, QuickLinkIds.Length - 1);

            return QuickLinkIds;
        }

        public int GetMenuId(int MenuId)
        {
            int TabMenuId = 0;
            //var QuickLink = _VideoAssetManagerDBContext.TabMenu.FirstOrDefault(a => a.MenuId == MenuId && a.IsSoftDelete == false);
            var QuickLink = _Context.TabMenu.FirstOrDefault(a => a.MenuId == MenuId && a.IsSoftDelete == false);
            if (QuickLink != null)
            {
                TabMenuId = QuickLink.MenuId;
            }
            return TabMenuId;
        }

        public IActionResult CreateRole(QuicklinkPermission QuicklinkPermission)
        {
            if (QuicklinkPermission.QuicklinkIds == null && QuicklinkPermission.TabMenuIds == null)
            {
                TempData["error"] = "Please select atleast one permissions";
                return RedirectToAction("CreateUserRole");
            }
            List<string> strQuicklinkIds = null;
            List<string> strTablinkIds = null;
            if (QuicklinkPermission.QuicklinkIds != null)
            {
                var QuicklinkIds = QuicklinkPermission.QuicklinkIds.TrimEnd(',');
                strQuicklinkIds = QuicklinkIds.Split(',').Distinct().ToList();
            }
            if (QuicklinkPermission.TabMenuIds != null)
            {
                var TablinkIds = QuicklinkPermission.TabMenuIds.TrimEnd(',');
                strTablinkIds = TablinkIds.Split(',').Distinct().ToList();
            }
            IQueryable<UserRoles> UserRolesDetails = null;
            string RoleId = string.Empty;

            if (!string.IsNullOrEmpty(QuicklinkPermission.RoleName))
                UserRolesDetails = this._Context.UserRoles("", QuicklinkPermission.RoleName);

            foreach (var itm in UserRolesDetails)
            {
                RoleId = itm.Id;
            }

            if (!string.IsNullOrEmpty(RoleId))
            {
                DeletePermission(QuicklinkPermission.RoleId);
                if (strQuicklinkIds != null)
                {
                    foreach (var itm in strQuicklinkIds)
                    {
                        QuicklinkPermission Permission = new QuicklinkPermission();
                        Permission.RoleId = QuicklinkPermission.RoleId;
                        Permission.QuicklinkId = Convert.ToInt32(itm);
                        _iwrapperRepository.QuicklinkPermission.Add(Permission);
                        _iwrapperRepository.Save();
                    }
                }

                DeleteTabPermission(QuicklinkPermission.RoleId);
                if (strTablinkIds != null)
                {
                    foreach (var itm in strTablinkIds)
                    {

                        TabMenuPermission Permission = new TabMenuPermission();
                        Permission.RoleId = QuicklinkPermission.RoleId;
                        Permission.TabMenuId = GetMenuId(Convert.ToInt32(itm));
                        _iwrapperRepository.TabMenuPermission.Add(Permission);
                        _iwrapperRepository.Save();
                    }
                }


            }
            else
            {
                if (!_roleManager.RoleExistsAsync(QuicklinkPermission.RoleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(QuicklinkPermission.RoleName)).GetAwaiter().GetResult();
                    //if roles are not created, then we will create admin user as well

                    _userManager.CreateAsync(new UserMaster
                    {

                        UserName = "admin@VideoAssetManager.com",
                        Email = "admin@VideoAssetManager.com",
                        Name = "Super Admin",
                        PhoneNumber = "1112223333"

                    }, "Pass@123").GetAwaiter().GetResult();

                    UserMaster user = _Context.UserMasters.FirstOrDefault(u => u.Email == "admin@VideoAssetManager.com");
                    UserDetail _userDetail = new UserDetail();


                    _userDetail.Id = user.Id;
                    _userDetail.Email = "admin@VideoAssetManager.com";
                    _userDetail.Occupation = "Gov Job";
                    _userDetail.MobileNumber = "2222000000";
                    _userDetail.IsDeleted = false;
                    _userDetail.Active = true;
                    _userDetail.CountryCode = "+91";
                    _userDetail.StreetAddress = "B 37";
                    _userDetail.State = "UP";
                    _userDetail.PostalCode = "201301";
                    _userDetail.City = "Noida";
                    _userDetail.HostName = "rekhta";
                    _userDetail.UserType = "N";
                    _userDetail.CreatedOn = DateTime.UtcNow;
                    _userDetail.ModifiedOn = DateTime.UtcNow;



                    _Context.UserDetail.Add(_userDetail);

                    _userManager.AddToRoleAsync(user, RekhtaUtility.Role_Admin).GetAwaiter().GetResult();


                    UserRolesDetails = this._Context.UserRoles("", QuicklinkPermission.RoleName);
                    string UserRoleId = "";
                    foreach (var role in UserRolesDetails)
                    {
                        UserRoleId = role.Id;
                    }
                    DeletePermission(UserRoleId);

                    if (strQuicklinkIds != null)
                    {
                        foreach (var itm in strQuicklinkIds)
                        {
                            QuicklinkPermission Permission = new QuicklinkPermission();
                            Permission.RoleId = UserRoleId;
                            Permission.QuicklinkId = Convert.ToInt32(itm);
                            _iwrapperRepository.QuicklinkPermission.Add(Permission);
                            _iwrapperRepository.Save();
                        }
                    }

                    DeleteTabPermission(UserRoleId);
                    if (strTablinkIds != null)
                    {
                        foreach (var itm in strTablinkIds)
                        {
                            TabMenuPermission Permission = new TabMenuPermission();
                            Permission.RoleId = UserRoleId;
                            Permission.TabMenuId = GetMenuId(Convert.ToInt32(itm));
                            _iwrapperRepository.TabMenuPermission.Add(Permission);
                            _iwrapperRepository.Save();
                        }
                    }

                }
            }
            return RedirectToAction("UserRole");
        }

        public List<SelectListItem> GetQuickLinksList(int QuickLinkId)
        {

            IDictionary<int, string> QuickLinksList = new Dictionary<int, string>();
            List<SelectListItem> contentitems = new List<SelectListItem>();
            string StoredProc = "exec SP_GetQuickLinks ";
            var QuickLinks = this._Context.QuickLinks.FromSqlRaw(StoredProc);

            if (QuickLinks != null)
            {
                QuickLinksList = QuickLinks.ToDictionary(x => x.QuickLinkId, x => x.QuickLinkName);
            }
            contentitems = RekhtaUtility.GetSelectItem(QuickLinksList, QuickLinkId);
            int iCount = contentitems.Count();
            for (int i = 0; i < iCount; i++)
            {
                if (contentitems[i].Text.ToString() == "Select Items")
                {
                    contentitems.Remove(contentitems[i]);
                    iCount--;
                }
            }
            return contentitems;
        }

        [HttpGet]
        public IActionResult ChangeUStatus(int Number)
        {
            var isContentUpdate = 0;
            IEnumerable<UserDetail> master = _iwrapperRepository.UserDetail.GetAll();
            var details = master.FirstOrDefault(c => c.UserId == Number);
            if (details != null)
            {
                details.Active = details.Active == true ? false : true;
                _iwrapperRepository.Save();
                isContentUpdate = 1;
            }
            return Json(new { Status = isContentUpdate });
        }
    }
}
