using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nancy.Json;
using VideoAssetManager.Areas.Admin.Controllers;
using VideoAssetManager.CommonUtils;
using VideoAssetManager.CommonUtils.Configuration;
using VideoAssetManager.DataAccess;
//using VideoAssetManager.DataAccess.Business;
using VideoAssetManager.DataAccess.Repository.IRepository;
using VideoAssetManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static VideoAssetManager.DataAccess.Common.MailUtility;
using VideoAssetManager.Encoding;
using VideoAssetManager.DataAccess.Business;

namespace VideoAssetManager.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = RekhtaUtility.Role_Admin)]
    public class VideoManagementController : Controller
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> SignInManager;
        private readonly IWrapperRepository _iwrapperRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<VideoManagementController> _logger;
        private readonly AppConfig _appConfig;
        //  private readonly UserManager<UserMaster> _userManager;
        private readonly VideoAssetManagerDBContext _context;
        public VideoManagementController(IWrapperRepository iwrapperRepository, IWebHostEnvironment hostEnvironment, ILogger<VideoManagementController> logger, IOptions<AppConfig> appConfig, UserManager<IdentityUser> userManager, VideoAssetManagerDBContext context)
        {
            _iwrapperRepository = iwrapperRepository;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
            _appConfig = appConfig.Value;
            _userManager = userManager;
            _context = context;
        }

        //[CheckPermission]
        public IActionResult Index()
        {
            ViewData["appurl"] = _appConfig.URLPaths.ApplicationAdminPath;
            return View();
        }

        [HttpGet]
        public JsonResult RawFootageDetails(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int rawFootageListPageId)
        {
            int irows = Convert.ToInt16(rows);

            var details = GetManageRawFootageList(sidx, sord, page, rows, _search, filters, TopSearch, rawFootageListPageId);

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


        private IEnumerable<VM_RawFootage> GetManageRawFootageList(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int rawFootageListPageId)
        {

            List<VM_City> cityList = _context.VM_City
            .Select(x => new VM_City
            {
                CityId = x.CityId,
                City = x.City,  // Adjust if your column name is different
                CountryId = x.CountryId
            }).ToList();


            IEnumerable<VM_RawFootage> projectDetails = _iwrapperRepository.RawFootage.GetAll()
                .Where(c => !c.IsSoftDelete)
                .Select(c => new VM_RawFootage
                {
                    Id = c.Id,
                    RFId = c.RFId,
                    VideoTitle = c.VideoTitle,
                    NoOfCameraUsed = c.NoOfCameraUsed,
                    Venue = c.Venue,
                    Country = c.CountryId == 1 ? "India" : c.CountryId == 2 ? "United Arab Emirates (UAE)" : "United Kingdom (UK)",
                    City = GetCityNameById(c.CityId, cityList),
                    DateOfEvent = c.DateOfEvent,
                    EventTitle = c.EventTitle,
                    Day = c.Day,
                    Place = c.Place,
                    DaySession = c.DaySession,
                    Description = c.Description,
                    Lang = c.Lang,
                    CreatedOn = c.CreatedOn.Date,
                    ModifyOn = c.ModifyOn.Date,
                    Active = c.Active
                }).OrderByDescending(x => x.ModifyOn).ToList();


            if (_search && !string.IsNullOrEmpty(filters))
            {
                var filterObject = new JavaScriptSerializer().Deserialize<Rootobject>(filters);
                projectDetails = projectDetails.ApplyFilters(filterObject);
            }

            if (!string.IsNullOrEmpty(TopSearch))
            {
                string data = TopSearch.ToLower().Trim();
                if (DateTime.TryParseExact(data, "dd-M-yy", null, System.Globalization.DateTimeStyles.None, out DateTime dt_data))
                {
                    projectDetails = projectDetails.Where(x => x.CreatedOn.Date == dt_data || x.ModifyOn.Date == dt_data);
                }
                else
                {
                    projectDetails = projectDetails.Where(x => x.VideoTitle.ToLower().Contains(data));
                }
            }

            if (sord == "desc")
            {
                projectDetails = projectDetails.ApplySorting(sidx, sord);
            }
            else
            {
                projectDetails = projectDetails.ApplySorting(sidx, sord);
            }
            return projectDetails;
        }



        public IActionResult CreateRawFootage(Guid Id = default)
        {
            ViewBag.NoOfCameraUsedList = new List<SelectListItem>
                {
                    new SelectListItem { Text = "1", Value = "1" },
                    new SelectListItem { Text = "2", Value = "2" },
                    new SelectListItem { Text = "3", Value = "3" },
                    new SelectListItem { Text = "4", Value = "4" },
                    new SelectListItem { Text = "5", Value = "5" },
                    new SelectListItem { Text = "6", Value = "6" },
                    new SelectListItem { Text = "7", Value = "7" },
                    new SelectListItem { Text = "8", Value = "8" },
                    new SelectListItem { Text = "9", Value = "9" },
                    new SelectListItem { Text = "10", Value = "10" },
                };

            ViewBag.CountryList = new List<SelectListItem>
                {
                    new SelectListItem { Text = "India", Value = "1" },
                    new SelectListItem { Text = "United Arab Emirates (UAE)", Value = "2" },
                    new SelectListItem { Text = "United Kingdom (UK)", Value = "3" },
                };

            ViewBag.ProjectList = _context.VM_Project
                .Where(r => r.IsSoftDelete == false)
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = $"{r.Name} ({r.Code})"
                })
                .ToList();

            if (Id != Guid.Empty)
            {
                IEnumerable<VM_RawFootage> rawFootage = _iwrapperRepository.RawFootage.GetAll();
                IEnumerable<VM_RawFootage> rawFootageDetails = null;

                rawFootageDetails = (from c in rawFootage
                                     select new VM_RawFootage
                                     {
                                         Id = c.Id,
                                         RFId = c.RFId,
                                         VideoTitle = c.VideoTitle,
                                         Description = c.Description,
                                         NoOfCameraUsed = c.NoOfCameraUsed,
                                         IsLivePerformance = c.IsLivePerformance,
                                         Venue = c.Venue,
                                         CountryId = c.CountryId,
                                         CityId = c.CityId,
                                         DateOfEvent = c.DateOfEvent,
                                         EventTitle = c.EventTitle,
                                         Day = c.Day,
                                         //Stage = c.Stage,
                                         Place = c.Place,
                                         ProjectId = c.ProjectId,
                                         ProjectCode = c.ProjectCode,
                                         ProjectName = c.ProjectName,
                                         StageId = c.StageId,
                                     }).ToList();

                var rawFootageData = rawFootageDetails.FirstOrDefault(a => a.Id == Id);

                var stages = _context.VM_Stage.ToList()
                .Where(s => s.StageId == rawFootageData.StageId) // Filter by project
                .Select(s => new SelectListItem
                {
                    Value = s.StageId.ToString(),
                    Text = s.Name,
                    Selected = s.StageId == rawFootageData.StageId
                });

                // Populate city list if CountryId is present
                if (rawFootageData?.CountryId != null)
                {
                    var cities = _context.VM_City
                        .Where(c => c.CountryId == rawFootageData.CountryId)
                        .Select(c => new SelectListItem
                        {
                            Value = c.CityId.ToString(),
                            Text = c.City
                        })
                        .ToList();

                    ViewBag.CityList = new SelectList(cities, "Value", "Text", rawFootageData?.CityId);
                }
                
                ViewBag.Stages = new SelectList(stages, "Value", "Text", rawFootageData?.StageId);

                return View(rawFootageData);
            }
            else
            {
                var model = new VM_RawFootage
                {
                    Day = 1,
                    Lang = 1,
                    DateOfEvent = DateTime.Today
                };

                ViewBag.Stages = new SelectList(new List<SelectListItem>(), "Value", "Text");
                ViewBag.CityList = new SelectList(new List<SelectListItem>(), "Value", "Text");

                return View(model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRawFootage(VM_RawFootage rawFootage)
        {

            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                    userId = "00000000-0000-0000-0000-000000000000";

                ModelState.Remove("Id");
                ModelState.Remove("RFId");
                ModelState.Remove("ProjectCode");
                ModelState.Remove("HiddenProjectId");
                ModelState.Remove("HiddenDateOfEvent");
                ModelState.Remove("HiddenNoOfCameraUsed");
                ModelState.Remove("HiddenStageId");
                ModelState.Remove("IsLivePerformanceHidden");
                ModelState.Remove("HiddenCountryId");

                if (!rawFootage.IsLivePerformance)
                {
                    ModelState.Remove("CityId");
                    ModelState.Remove("CountryId");
                }

                if (ModelState.IsValid)
                {
                    if (rawFootage.Id != Guid.Empty)
                    {
                        if (!IsExists(rawFootage.VideoTitle, rawFootage.Id))
                        {
                            IEnumerable<VM_RawFootage> rawFootageAll = _iwrapperRepository.RawFootage.GetAll();
                            var details = rawFootageAll.FirstOrDefault(c => c.Id == rawFootage.Id);

                            details.VideoTitle = rawFootage.VideoTitle;

                            details.NoOfCameraUsed = rawFootage.HiddenNoOfCameraUsed;
                            details.IsLivePerformance = rawFootage.IsLivePerformance;
                            details.Venue = rawFootage.Venue;
                            details.CountryId = rawFootage.CountryId;
                            details.DateOfEvent = rawFootage.HiddenDateOfEvent;
                            details.EventTitle = rawFootage.EventTitle;
                            details.Day = rawFootage.Day;
                            //details.Stage = rawFootage.Stage;
                            details.Place = rawFootage.Place;

                            string desc = RekhtaUtility.removeTags(rawFootage.Description);
                            details.Description = desc;

                            details.ModifyBy = Guid.Parse(userId);
                            details.ModifyOn = DateTime.Now;
                            details.Active = details.Active;
                            details.ProjectId = rawFootage.HiddenProjectId;
                            details.StageId = rawFootage.StageId;
                            details.CityId = rawFootage.CityId;

                            _iwrapperRepository.RawFootage.Update(details);
                            _iwrapperRepository.Save();

                            TempData["success"] = "Raw Footage updated successfully";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["error"] = "Raw Footage already exists";
                            return RedirectToAction("CreateRawFootage", new { Id = rawFootage.Id });
                        }
                    }
                    else
                    {

                        if (!IsExists(rawFootage.VideoTitle, Guid.Empty))
                        {
                            var todayDate = DateTime.Now.Date;
                            string formattedDate = todayDate.ToString("yyyyMMdd");

                            rawFootage.Id = Guid.NewGuid();

                            // Fetch the latest RFId from the database, ensuring real-time accuracy
                            var latestAddedRFId = _iwrapperRepository.RawFootage
                                                        .GetAll()
                                                        .OrderByDescending(c => c.CreatedOn)
                                                        .Select(c => c.RFId)
                                                        .FirstOrDefault();

                            string lastFiveDigits = "00000"; // Default if no previous RFId exists

                            if (!string.IsNullOrEmpty(latestAddedRFId) && latestAddedRFId.Length >= 5)
                            {
                                // Extract the last five digits
                                lastFiveDigits = latestAddedRFId.Substring(latestAddedRFId.Length - 5);
                            }

                            // Convert to integer, increment, and format
                            if (!int.TryParse(lastFiveDigits, out int lastNumber))
                            {
                                lastNumber = 0; // Default to 0 if parsing fails
                            }

                            lastNumber++; // Ensure unique increment
                            string newLastFiveDigits = lastNumber.ToString("D5"); // Keep it five digits

                            string desc = RekhtaUtility.removeTags(rawFootage.Description);
                            rawFootage.Description = desc;

                            //rawFootage.DateOfEvent = DateTime.Now;
                            formattedDate = rawFootage.DateOfEvent.ToString("yyyyMMdd");
                            rawFootage.RFId = $"{rawFootage.ProjectCode}-{formattedDate}-CAM{rawFootage.NoOfCameraUsed}-{newLastFiveDigits}";
                            rawFootage.Day = 0;

                            rawFootage.CreatedOn = DateTime.Now;
                            rawFootage.CreatedBy = Guid.Parse(userId);
                            rawFootage.ModifyOn = DateTime.Now;
                            rawFootage.Active = true;

                            //string rootPath = Path.Combine(_appConfig.AppSettings.RawFootagePath, rawFootage.ProjectName, rawFootage.RFId);

                            //string rawFolderPath = Path.Combine(rootPath, "RAW");
                            //string exportFolderPath = Path.Combine(rootPath, "EXPORTS");
                            //string thumbFolderPath = Path.Combine(rootPath, "THUMB");
                            //string audioFolderPath = Path.Combine(rootPath, "AUDIO");
                            //string graphicsFolderPath = Path.Combine(rootPath, "GRAPHICS");
                            //string projectFolderPath = Path.Combine(rootPath, "PROJECTS");

                            //Directory.CreateDirectory(rawFolderPath);
                            //Directory.CreateDirectory(exportFolderPath);
                            //Directory.CreateDirectory(thumbFolderPath);
                            //Directory.CreateDirectory(audioFolderPath);
                            //Directory.CreateDirectory(graphicsFolderPath);

                            // Save the new record immediately to prevent duplicate RFIds
                            _iwrapperRepository.RawFootage.Add(rawFootage);
                            _iwrapperRepository.Save();

                            TempData["success"] = "Raw Footage created successfully";
                            return RedirectToAction("Index");
                        }

                        else
                        {
                            TempData["error"] = "Raw Footage already exists";
                            return View("CreateRawFootage", rawFootage);
                        }
                    }
                }
                _logger.LogError("Created");
            }
            catch (Exception ex)
            {
                string strErrormsg = ex.Message;
                _logger.LogError(ex.Message);
            }
            return RedirectToAction(default);
        }

        public bool IsExists(string videoTitle, Guid id)
        {
            bool Isduplicate = false;
            var category = _iwrapperRepository.RawFootage.GetAll().FirstOrDefault(c => (c.VideoTitle.ToLower() == videoTitle.ToLower()) && (c.Id != id && c.IsSoftDelete == false));
            if (category != null)
            {
                Isduplicate = true;
            }
            return Isduplicate;
        }


        public IActionResult Delete(string oper, Guid id = default)
        {
            try
            {
                IEnumerable<VM_RawFootage> rawFootage = _iwrapperRepository.RawFootage.GetAll();
                if (oper == "del")
                {
                    var rawFootageDetails = rawFootage.FirstOrDefault(c => c.Id == id);
                    rawFootageDetails.IsSoftDelete = true;
                    _iwrapperRepository.RawFootage.Update(rawFootageDetails);
                    _iwrapperRepository.Save();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet]
        public IActionResult ChangeRawFootageStatus(Guid Id = default)
        {
            var israwFootageUpdate = 0;
            IEnumerable<VM_RawFootage> rawFootage = _iwrapperRepository.RawFootage.GetAll();
            var rawFootageDetails = rawFootage.FirstOrDefault(c => c.Id == Id);
            if (rawFootageDetails != null)
            {
                rawFootageDetails.Active = rawFootageDetails.Active == true ? false : true;
                _iwrapperRepository.Save();
                israwFootageUpdate = 1;
            }
            return Json(new { Status = israwFootageUpdate });
        }

        public IActionResult EditedVideosIndex()
        {
            ViewData["appurl"] = _appConfig.URLPaths.ApplicationAdminPath;

            ViewBag.VideoFormatName = new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Reel", Value = "1" },
                    new SelectListItem { Text = "Video", Value = "2" },
                };
            return View();
        }

        public IActionResult CreateEditedVideos(int Id = 0)
        {
          
            ViewBag.ResolutionList = _context.VM_VideosResolution
                                        .Select(r => new SelectListItem
                                        {
                                            Value = r.Id.ToString(),
                                            Text = r.Resolution
                                        })
                                        .ToList();

            ViewBag.RawFootageList = _context.VM_RawFootage
                                        .Where(r => r.IsSoftDelete == false)
                                        .Select(r => new SelectListItem
                                        {
                                            Value = r.RFId.ToString(),
                                            Text = $"{r.VideoTitle} ({r.RFId})"
                                        })
                                        .ToList();

            ViewBag.VideoTypeList = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Reel", Value = "1" },
                    new SelectListItem { Text = "Video", Value = "2" }
                };
                      
            ViewBag.LanguageList = new List<SelectListItem>
            {
                new SelectListItem { Text = "English", Value = "English" },
                new SelectListItem { Text = "Hindi", Value = "Hindi" },
                new SelectListItem { Text = "Urdu", Value = "Urdu" },
                new SelectListItem { Text = "Gujarati", Value = "Gujarati" },
                new SelectListItem { Text = "Rajasthani", Value = "Rajasthani" },
                new SelectListItem { Text = "Other", Value = "Other" }
            };

            ViewBag.CategoryList = _context.VM_CategoryMaster
               .Select(c => new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Name
               })
               .ToList();

            if (Id != 0)
            {
               
                    var editedVideo = _context.VM_EditedVideos
                        .Include(v => v.Participants)
                        .FirstOrDefault(v => v.Id == Id);
                    return View(editedVideo ?? new VM_EditedVideos());                
            }
            else
            {               
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEditedVideos(VM_EditedVideos editedVideos)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                    userId = "00000000-0000-0000-0000-000000000000";

                ModelState.Remove("Id");
                ModelState.Remove("RFId");

                if (ModelState.IsValid)
                {

                    if (editedVideos.Id != 0)
                    {
                        var existingVideo = _context.VM_EditedVideos
                            .Include(v => v.Participants) // Load participants
                            .FirstOrDefault(v => v.Id == editedVideos.Id);

                        if (existingVideo != null)
                        {
                            // Update core video properties
                            existingVideo.Title = editedVideos.Title;
                            existingVideo.RFId = editedVideos.RFId;
                            existingVideo.Resolution = editedVideos.Resolution;
                            existingVideo.VideoType = editedVideos.VideoType;
                            existingVideo.IsForRekhtaApp = editedVideos.IsForRekhtaApp;
                            existingVideo.ModifyOn = DateTime.Now;
                            existingVideo.ModifyBy = Guid.Parse(userId);
                            existingVideo.VideoFileName = editedVideos.VideoFileName;
                            existingVideo.Thumbnail = editedVideos.Thumbnail;
                            existingVideo.Language = editedVideos.Language;
                            existingVideo.CategoryId = editedVideos.CategoryId;
                            existingVideo.Duration = editedVideos.Duration;
                            existingVideo.Size = editedVideos.Size;

                            // Track participants to keep (using a temporary key, e.g., ArtistName + Type)
                            var incomingParticipants = editedVideos.Participants
                                .Select(p => new { p.ArtistName, p.Type, p.Gender })
                                .ToList();

                            // Delete participants NOT in the incoming list (based on business key)
                            var participantsToDelete = existingVideo.Participants
                                .Where(ep => !incomingParticipants.Any(ip =>
                                    ip.ArtistName == ep.ArtistName &&
                                    ip.Type == ep.Type &&
                                    ip.Gender == ep.Gender))
                                .ToList();

                            foreach (var participant in participantsToDelete)
                            {
                                _context.VM_ArtistMapping.Remove(participant);
                            }

                            // Add or update remaining participants
                            foreach (var incoming in editedVideos.Participants)
                            {
                                var existing = existingVideo.Participants
                                    .FirstOrDefault(ep =>
                                        ep.ArtistName == incoming.ArtistName &&
                                        ep.Type == incoming.Type &&
                                        ep.Gender == incoming.Gender);

                                if (existing == null)
                                {
                                    // Add new participant
                                    incoming.ExportId = existingVideo.ExportGuid;
                                    incoming.Id = 0; // Force new record
                                    _context.VM_ArtistMapping.Add(incoming);
                                }
                                else
                                {
                                    // Update existing participant (if needed)
                                    existing.ArtistName = incoming.ArtistName;
                                    existing.Type = incoming.Type;
                                    existing.Gender = incoming.Gender;
                                }
                            }

                            _context.SaveChanges();
                            TempData["success"] = "Edited Video updated successfully";
                        }
                        else
                        {
                            TempData["error"] = "Edited Video not found!";
                        }
                    }
                    else
                    {
                        if (!IsExistsEditedVideos(editedVideos.Title, 0))
                        {
                            editedVideos.IsForRekhtaApp = editedVideos.IsForRekhtaApp;
                            editedVideos.CreatedOn = DateTime.Now;
                            editedVideos.CreatedBy = Guid.Parse(userId);
                            editedVideos.ModifyOn = DateTime.Now;
                            editedVideos.Active = true;
                            editedVideos.ExportGuid = Guid.NewGuid();

                            // Save the parent first to generate ExportGuid
                            _iwrapperRepository.EditedVideos.Add(editedVideos);
                            //_iwrapperRepository.Save();

                            // Add participants (Id will be auto-generated)
                            foreach (var participant in editedVideos.Participants)
                            {
                                participant.Id = 0;
                                participant.ExportId = editedVideos.ExportGuid;
                                //_iwrapperRepository.VM_ArtistMapping.Add(participant);
                            }

                            _iwrapperRepository.Save(); // Save participants

                            TempData["success"] = "Edited Video created successfully";
                        }
                        else
                        {
                            TempData["error"] = "Edited Video already exists";
                        }
                    }
                    return RedirectToAction("EditedVideosIndex");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred: " + ex.Message;
            }
            return RedirectToAction("CreateEditedVideos", Guid.Empty);
        }


        public bool IsExistsEditedVideos(string videoTitle, int id)
        {
            bool Isduplicate = false;
            var editedVideos = _iwrapperRepository.EditedVideos.GetAll().FirstOrDefault(c => (c.Title.ToLower() == videoTitle.ToLower()) && (c.Id != id && c.IsSoftDelete == false));
            if (editedVideos != null)
            {
                Isduplicate = true;
            }
            return Isduplicate;
        }

        [HttpGet]
        public JsonResult EditedVideosDetails(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int editedVideosPageId)
        {
            int irows = Convert.ToInt16(rows);
            var details = GetManageEditedVideosList(sidx, sord, page, rows, _search, filters, TopSearch, editedVideosPageId);

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


        private IEnumerable<VM_EditedVideos> GetManageEditedVideosList(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int editedVideosPageId)
        {
           
            IEnumerable<VM_EditedVideos> editedVideos = _iwrapperRepository.EditedVideos.GetAll();

            // First get all projects, then handle grouping in memory
            var projects = _context.VM_Project
                .AsEnumerable()  // Switch to client evaluation
                .GroupBy(p => p.Code)
                .Select(g => g.First())
                .ToList();

            var projectDict = projects.ToDictionary(p => p.Code, p => p.Name);

            IEnumerable<VM_EditedVideos> editedVideosDetails =
                (from c in editedVideos.Where(c => !c.IsSoftDelete)
                 let rfIdCode = c.RFId.Length >= 5 ? c.RFId.Substring(0, 5) : c.RFId
                 select new VM_EditedVideos
                 {
                     Id = c.Id,
                     RFId = c.RFId,
                     Title = c.Title,
                     VideoType = c.VideoType,
                     VideoTypeName = c.VideoType == 1 ? "Reel" : "Video",
                     Active = c.Active,
                     VideoFileName = c.VideoFileName,
                     Thumbnail = c.Thumbnail,
                     IsForRekhtaApp = c.IsForRekhtaApp,
                     Duration = c.Duration,
                     Size = c.Size,
                     ProjectName = projectDict.TryGetValue(rfIdCode, out var name)
                         ? $"{name}"
                         : $"{rfIdCode} (Project Not Found)"
                 }).ToList();


            if (_search && !string.IsNullOrEmpty(filters))
            {
                var filterObject = new JavaScriptSerializer().Deserialize<Rootobject>(filters);
                editedVideosDetails = editedVideosDetails.ApplyFilters(filterObject);
            }

            if (!string.IsNullOrEmpty(TopSearch))
            {
                string data = TopSearch.ToLower().Trim();
                if (DateTime.TryParseExact(data, "dd-M-yy", null, System.Globalization.DateTimeStyles.None, out DateTime dt_data))
                {
                    editedVideosDetails = editedVideosDetails.Where(x => x.CreatedOn.Date == dt_data || x.ModifyOn.Date == dt_data);
                }
                else
                {
                    editedVideosDetails = editedVideosDetails.Where(x => x.Title.ToLower().Contains(data));
                }
            }

            if (sord == "desc")
            {
                editedVideosDetails = editedVideosDetails.ApplySorting(sidx, sord);
            }
            else
            {
                editedVideosDetails = editedVideosDetails.ApplySorting(sidx, sord);
            }
            return editedVideosDetails;
        }


        public IActionResult DeleteEditedVideos(string oper, int Id)
        {
            try
            {
                IEnumerable<VM_EditedVideos> editedVideos = _iwrapperRepository.EditedVideos.GetAll();
                if (oper == "del")
                {
                    var tagDetails = editedVideos.FirstOrDefault(c => c.Id == Id);
                    tagDetails.IsSoftDelete = true;
                    _iwrapperRepository.EditedVideos.Update(tagDetails);
                    _iwrapperRepository.Save();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet]
        public IActionResult ChangeEditedVideoStatus(int Id)
        {
            var isContentUpdate = 0;
            IEnumerable<VM_EditedVideos> editedVideos = _iwrapperRepository.EditedVideos.GetAll();
            var contentDetails = editedVideos.FirstOrDefault(c => c.Id == Id);
            if (contentDetails != null)
            {
                contentDetails.Active = contentDetails.Active == true ? false : true;
                _iwrapperRepository.Save();
                isContentUpdate = 1;
            }
            return Json(new { Status = isContentUpdate });
        }
        [HttpGet]
        public IActionResult ChangeIsMobileAppStatus(int Id)
        {
            var isContentUpdate = 0;
            IEnumerable<VM_EditedVideos> editedVideos = _iwrapperRepository.EditedVideos.GetAll();
            var contentDetails = editedVideos.FirstOrDefault(c => c.Id == Id);
            if (contentDetails != null)
            {
                contentDetails.IsForRekhtaApp = contentDetails.IsForRekhtaApp == true ? false : true;
                _iwrapperRepository.Save();
                isContentUpdate = 1;
            }
            return Json(new { Status = isContentUpdate });
        }

        public IActionResult ProjectMasterIndex()
        {
            RekhtaUtility.GetProperty.ClassName = "firstTab";
            ViewData["appurl"] = _appConfig.URLPaths.ApplicationAdminPath;
            return View();
        }

        public IActionResult DeleteProjectMaster(string oper, int Id)
        {
            try
            {
                IEnumerable<VM_Project> projectMaster = _iwrapperRepository.VM_Project.GetAll();
                if (oper == "del")
                {
                    var projectDetails = projectMaster.FirstOrDefault(c => c.Id == Id);
                    projectDetails.IsSoftDelete = true;
                    _iwrapperRepository.VM_Project.Update(projectDetails);
                    _iwrapperRepository.Save();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet]
        public IActionResult ChangeProjectStatus(int Id)
        {
            var isContentUpdate = 0;
            IEnumerable<VM_Project> project = _iwrapperRepository.VM_Project.GetAll();
            var projectDetails = project.FirstOrDefault(c => c.Id == Id);
            if (projectDetails != null)
            {
                projectDetails.Active = projectDetails.Active == true ? false : true;
                _iwrapperRepository.Save();
                isContentUpdate = 1;
            }
            return Json(new { Status = isContentUpdate });
        }

        public IActionResult CreateProjectMaster(int Id)
        {
            ViewBag.TypeList = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Events", Value = "1" },
                    new SelectListItem { Text = "Others", Value = "2" },
                };

            ViewBag.EventList = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Jashn-e-Rekhta (India)", Value = "1" },
                    new SelectListItem { Text = "Jashn-e-Rekhta (Dubai)", Value = "2" },
                    new SelectListItem { Text = "Jashn-e-Rekhta (London)", Value = "3" },
                    new SelectListItem { Text = "Shaam-e-Rekhta", Value = "4" },
                    new SelectListItem { Text = "Baitbazi", Value = "5" },
                    new SelectListItem { Text = "Hindwi Utsav", Value = "6" },
                    new SelectListItem { Text = "Gujarati Utsav", Value = "7" },
                    new SelectListItem { Text = "Anjas Mahotsav", Value = "8" },
                };

            GetStageList();

            if (Id != 0)
            {
                IEnumerable<VM_Project> project = _iwrapperRepository.VM_Project.GetAll();
                IEnumerable<VM_Project> projectDetails = null;

                projectDetails = (from c in project
                                  select new VM_Project
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      Code = c.Code,
                                      Type = c.Type,
                                      Event = c.Event,
                                  }).ToList();

                var tag = projectDetails.FirstOrDefault(a => a.Id == Id);

                return View(tag);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProjectMaster(VM_Project project, List<Guid> StagesGuid)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                    userId = "00000000-0000-0000-0000-000000000000";

                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    if (project.Id != 0)
                    {
                        if (!IsExistsProject(project.Name, project.Id))
                        {
                            IEnumerable<VM_Project> projectMaster = _iwrapperRepository.VM_Project.GetAll();
                            var projectDetails = projectMaster.FirstOrDefault(c => c.Id == project.Id);
                            projectDetails.Name = project.Name;
                            projectDetails.Code = project.Code;
                            projectDetails.ModifyBy = Guid.Parse(userId);
                            projectDetails.ModifyOn = DateTime.Now;
                            projectDetails.Active = projectDetails.Active;
                            projectDetails.Type = project.Type;
                            projectDetails.Event = project.Event;

                            _iwrapperRepository.VM_Project.Update(projectDetails);
                            _iwrapperRepository.Save();

                            CourseStageData(project.Id, StagesGuid);

                            TempData["success"] = "Project Name updated successfully";
                            return RedirectToAction("ProjectMasterIndex");
                        }
                        else
                        {
                            TempData["error"] = "Project Name already exists";
                            return RedirectToAction("CreateProjectMaster", new { Id = project.Id });
                        }
                    }
                    else
                    {
                        if (!IsExistsProject(project.Name, 0))
                        {
                            project.CreatedOn = DateTime.Now;
                            project.CreatedBy = Guid.Parse(userId);
                            project.ModifyOn = DateTime.Now;

                            _iwrapperRepository.VM_Project.Add(project);
                            //string projectPath = Path.Combine(_appConfig.AppSettings.RawFootagePath, project.Name);
                            //if (!Directory.Exists(projectPath))
                            //{
                            //    Directory.CreateDirectory(projectPath);
                            //}

                            project.Active = true;
                            _iwrapperRepository.Save();

                            CourseStageData(project.Id, StagesGuid);

                            TempData["success"] = "Project Name created successfully";
                            return RedirectToAction("ProjectMasterIndex");
                        }
                        else
                        {
                            TempData["error"] = "Project Name already exists";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrormsg = ex.Message;
            }
            return RedirectToAction("CreateProjectMaster", Guid.Empty);
        }

        public bool IsExistsProject(string checkString, int id = 0)
        {
            bool isExists = false;
            IEnumerable<VM_Project> project = _iwrapperRepository.VM_Project.GetAll();

            var projectDetails = project.FirstOrDefault(c => c.Name.ToLower() == checkString.ToLower() && c.Id != id && c.IsSoftDelete == false);
            if (projectDetails != null)
            {
                isExists = true;
            }

            return isExists;
        }

        [HttpGet]
        public JsonResult ProjectDetails(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int projectPageId)
        {
            int irows = Convert.ToInt16(rows);
            var details = GetManageProjectList(sidx, sord, page, rows, _search, filters, TopSearch, projectPageId);

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

        private IEnumerable<VM_Project> GetManageProjectList(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int projectPageId)
        {
            IEnumerable<VM_Project> projectDetails = _iwrapperRepository.VM_Project.GetAll()
                .Where(c => !c.IsSoftDelete)
                .Select(c => new VM_Project
                {
                    Id = c.Id,
                    Name = c.Name,
                    Code = c.Code,
                    Active = c.Active,
                    CreatedOn = c.CreatedOn.Date,
                    ModifyOn = c.ModifyOn.Date,
                }).OrderByDescending(x => x.ModifyOn).ToList();

            if (_search && !string.IsNullOrEmpty(filters))
            {
                var filterObject = new JavaScriptSerializer().Deserialize<Rootobject>(filters);
                projectDetails = projectDetails.ApplyFilters(filterObject);
            }

            if (!string.IsNullOrEmpty(TopSearch))
            {
                string data = TopSearch.ToLower().Trim();
                if (DateTime.TryParseExact(data, "dd-M-yy", null, System.Globalization.DateTimeStyles.None, out DateTime dt_data))
                {
                    projectDetails = projectDetails.Where(x => x.CreatedOn.Date == dt_data || x.ModifyOn.Date == dt_data);
                }
                else
                {
                    projectDetails = projectDetails.Where(x => x.Name.ToLower().Contains(data));
                }
            }

            if (sord == "desc")
            {
                projectDetails = projectDetails.ApplySorting(sidx, sord);
            }
            else
            {
                projectDetails = projectDetails.ApplySorting(sidx, sord);
            }
            return projectDetails;
        }

        public void GetStageList()
        {
            List<VM_Stage> stagesList = _context.VM_Stage.ToList();

            if (stagesList != null)
            {
                ViewBag.stagesList = stagesList;
            }
        }

        private void CourseStageData(int Id, List<Guid> Ids)
        {
            var StagesId = string.Join(",", Ids);
            PublishCourseLogic pcl = new PublishCourseLogic();
            pcl.InsertStageData(Id, StagesId);
        }
        [HttpGet]
        public IActionResult GetStagesByProject(int projectId)
        {
            // Get project type
            var projectType = _context.VM_Project
                .Where(p => p.Id == projectId)
                .Select(p => p.Type)
                .FirstOrDefault();

            // Get stages
            var stages = _context.VM_StageLookup
                .Where(sl => sl.ProjectId == projectId)
                .Join(_context.VM_Stage,
                    lookup => lookup.StageId,
                    stage => stage.StageId,
                    (lookup, stage) => new { Value = stage.StageId, Text = stage.Name })
                .ToList();

            // Return both project type and stages
            return Json(new { ProjectType = projectType, Stages = stages });
        }

        public IActionResult CategoryMasterIndex()
        {
            RekhtaUtility.GetProperty.ClassName = "secondTab";
            ViewData["appurl"] = _appConfig.URLPaths.ApplicationAdminPath;
            return View();
        }

        [HttpGet]
        public JsonResult CategoryDetails(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int categoryListPageId)
        {
            int irows = Convert.ToInt16(rows);
            var details = GetManageCategoryList(sidx, sord, page, rows, _search, filters, TopSearch, categoryListPageId);

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

        private IEnumerable<VM_CategoryMaster> GetManageCategoryList(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int categoryListPageId)
        {
            IEnumerable<VM_CategoryMaster> categoryDetails = _iwrapperRepository.VM_CategoryMaster.GetAll()
                .Where(c => !c.IsSoftDelete)
                .Select(c => new VM_CategoryMaster
                {
                    Id = c.Id,
                    Name = c.Name,
                    DisplayOrder = c.DisplayOrder,
                    CreatedOn = c.CreatedOn.Date,
                    ModifyOn = c.ModifyOn.Date,
                    Active = c.Active
                }).ToList();

            if (_search && !string.IsNullOrEmpty(filters))
            {
                var filterObject = new JavaScriptSerializer().Deserialize<Rootobject>(filters);
                categoryDetails = categoryDetails.ApplyFilters(filterObject);
            }

            if (!string.IsNullOrEmpty(TopSearch))
            {
                string data = TopSearch.ToLower().Trim();
                if (DateTime.TryParseExact(data, "dd-M-yy", null, System.Globalization.DateTimeStyles.None, out DateTime dt_data))
                {
                    categoryDetails = categoryDetails.Where(x => x.CreatedOn.Date == dt_data || x.ModifyOn.Date == dt_data);
                }
                else
                {
                    categoryDetails = categoryDetails.Where(x => x.Name.ToLower().Contains(data));
                }
            }

            if (sord == "desc")
            {
                categoryDetails = categoryDetails.ApplySorting(sidx, sord);
            }
            else
            {
                categoryDetails = categoryDetails.ApplySorting(sidx, sord);
            }
            return categoryDetails;
        }



        public IActionResult DeleteCategory(string oper, int id = 0)
        {
            try
            {
                IEnumerable<VM_CategoryMaster> categorys = _iwrapperRepository.VM_CategoryMaster.GetAll();
                if (oper == "del")
                {
                    var categoryDetails = categorys.FirstOrDefault(c => c.Id == id);
                    categoryDetails.IsSoftDelete = true;
                    _iwrapperRepository.VM_CategoryMaster.Update(categoryDetails);
                    _iwrapperRepository.Save();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet]
        public IActionResult ChangeCategoryStatus(int Id)
        {
            var isCategoryUpdate = 0;
            IEnumerable<VM_CategoryMaster> categoryMaster = _iwrapperRepository.VM_CategoryMaster.GetAll();
            var categoryDetails = categoryMaster.FirstOrDefault(c => c.Id == Id);
            if (categoryDetails != null)
            {
                categoryDetails.Active = categoryDetails.Active == true ? false : true;
                _iwrapperRepository.Save();
                isCategoryUpdate = 1;
            }
            return Json(new { Status = isCategoryUpdate });
        }

        public IActionResult CreateCategory(int Id = 0)
        {
            if (Id > 0)
            {
                IEnumerable<VM_CategoryMaster> categoryMaster = _iwrapperRepository.VM_CategoryMaster.GetAll();
                IEnumerable<VM_CategoryMaster> categoryDetails = null;

                categoryDetails = (from c in categoryMaster
                                   select new VM_CategoryMaster
                                   {
                                       Id = c.Id,
                                       Name = c.Name,
                                       DisplayOrder = c.DisplayOrder,
                                       Description = c.Description,
                                   }).ToList();

                var category = categoryDetails.FirstOrDefault(a => a.Id == Id);
                return View(category);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(VM_CategoryMaster category)
        {
            RekhtaUtility.GetProperty.ClassName = "secondTab";
            try
            {
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                    userId = "00000000-0000-0000-0000-000000000000";
                ModelState.Remove("Id");

                if (ModelState.IsValid)
                {
                    if (category.Id > 0)
                    {
                        if (!IsExists(category.Name, category.DisplayOrder, category.Id))
                        {
                            IEnumerable<VM_CategoryMaster> categoryMaster = _iwrapperRepository.VM_CategoryMaster.GetAll();
                            var details = categoryMaster.FirstOrDefault(c => c.Id == category.Id);

                            details.Name = category.Name;
                            details.DisplayOrder = category.DisplayOrder;
                            details.ModifyBy = Guid.Parse(userId);
                            details.ModifyOn = DateTime.Now;
                            details.Active = details.Active;
                            details.Description = category.Description;

                            _iwrapperRepository.VM_CategoryMaster.Update(details);
                            _iwrapperRepository.Save();
                            ViewBag.Id = RekhtaUtility.Id;
                            TempData["success"] = "Category updated successfully";
                            return RedirectToAction("CategoryMasterIndex");
                        }
                        else
                        {
                            TempData["error"] = "Category already exists";
                            return RedirectToAction("CreateCategory", new { Id = category.Id });
                        }
                    }
                    else
                    {
                        if (!IsExists(category.Name, category.DisplayOrder, 0))
                        {
                            category.CreatedOn = DateTime.Now;
                            category.CreatedBy = Guid.Parse(userId);
                            category.ModifyOn = DateTime.Now;
                            category.Active = true;
                            _iwrapperRepository.VM_CategoryMaster.Add(category);
                            _iwrapperRepository.Save();
                            TempData["success"] = "Category created successfully";
                            return RedirectToAction("CategoryMasterIndex");
                        }
                        else
                        {
                            TempData["error"] = "Category Name or Display Order already exists";
                            return View("CreateCategory", category);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrormsg = ex.Message;
            }
            return RedirectToAction("CreateCategory", 0);
        }

        public bool IsExists(string name, int displayOrder, int id)
        {
            bool Isduplicate = false;
            var category = _iwrapperRepository.VM_CategoryMaster.GetAll().FirstOrDefault(c => (c.Name.ToLower() == name.ToLower() || c.DisplayOrder == displayOrder) && (c.Id != id && c.IsSoftDelete == false));
            if (category != null)
            {
                Isduplicate = true;
            }
            return Isduplicate;
        }


        public IActionResult GetCitiesByCountry(int countryId)
        {
            var cities = _context.VM_City // Replace 'Cities' with your actual city DbSet
                .Where(c => c.CountryId == countryId)
                .Select(c => new SelectListItem
                {
                    Value = c.CityId.ToString(),
                    Text = c.City
                })
                .ToList();

            return Json(cities);
        }

        public IActionResult OverviewIndex()
        {
            ViewData["appurl"] = _appConfig.URLPaths.ApplicationAdminPath;

            var projects = _iwrapperRepository.VM_Project.GetAll()
                .Where(c => c.IsSoftDelete == false)
                .OrderByDescending(x => x.ModifyOn);

            return View(projects); // Pass the model to view
        }


        [HttpGet]
        public JsonResult OverviewDetails(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int overviewPageId)
        {
            int irows = Convert.ToInt16(rows);
            var details = GetManageOverviewList(sidx, sord, page, rows, _search, filters, TopSearch, overviewPageId);

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


        private IEnumerable<VM_Project> GetManageOverviewList(string sidx, string sord, string page, int rows, bool _search, string filters, string TopSearch, int overviewPageId)
        {
            IEnumerable<VM_Project> projectDetails = _iwrapperRepository.VM_Project.GetAll()
                .Where(c => !c.IsSoftDelete)
                .Select(c => new VM_Project
                {
                    Id = c.Id,
                    Name = c.Name,
                    NumberOfVideos = c.NumberOfVideos,
                    TotalVideoHours = c.TotalVideoHours,
                    TotalSpaceOccupied = c.TotalSpaceOccupied,
                }).OrderByDescending(x => x.ModifyOn).ToList();

            if (_search && !string.IsNullOrEmpty(filters))
            {
                var filterObject = new JavaScriptSerializer().Deserialize<Rootobject>(filters);
                projectDetails = projectDetails.ApplyFilters(filterObject);
            }

            if (!string.IsNullOrEmpty(TopSearch))
            {
                string data = TopSearch.ToLower().Trim();
                if (DateTime.TryParseExact(data, "dd-M-yy", null, System.Globalization.DateTimeStyles.None, out DateTime dt_data))
                {
                    projectDetails = projectDetails.Where(x => x.CreatedOn.Date == dt_data || x.ModifyOn.Date == dt_data);
                }
                else
                {
                    projectDetails = projectDetails.Where(x => x.Name.ToLower().Contains(data));
                }
            }

            if (sord == "desc")
            {
                projectDetails = projectDetails.ApplySorting(sidx, sord);
            }
            else
            {
                projectDetails = projectDetails.ApplySorting(sidx, sord);
            }
            return projectDetails;
        }

    }
}