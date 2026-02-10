using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using VideoAssetManager.CommonUtils;
using System.Data;
using VideoAssetManager.CommonUtils.Configuration;
using Microsoft.Extensions.Options;
using VideoAssetManager.Models;
using VideoAssetManager.DataAccess;
using Microsoft.EntityFrameworkCore;
using VideoAssetManager.Areas.Admin.Filters;

namespace VideoAssetManager.Learner.Controllers
{
    [Area("Learner")]
    [Authorize(Roles =RekhtaUtility.Role_Admin)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppConfig _appConfig;
        private readonly VideoAssetManagerDBContext _context;

        public HomeController(ILogger<HomeController> logger, IOptions<AppConfig> appConfig, VideoAssetManagerDBContext context)
        {
            _logger = logger;
            _appConfig = appConfig.Value;
            _context = context;
        }
        //[CheckPermission]
        public IActionResult Index()
        {
            RekhtaUtility.GetProperty.LinkId = 0;
            RekhtaUtility.GetProperty.LinkName = "";

            RekhtaUtility.GetProperty.siteUrl= _appConfig.URLPaths.ApplicationAdminPath;
            _logger.LogInformation("Home cotroller requested");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //public List<GetDayWiseReport> DayWiseData(int _month,int _year)
        //{
        //    string StoredProc = string.Empty;
        //    try
        //    {
        //        StoredProc = "exec SP_GetDayWiseReport " +
        //            "@month=" + _month + ",@year='" + _year + "'";

        //        List<GetDayWiseReport> DayWise = _context.GetDayWiseReport.FromSqlRaw(StoredProc).ToList();
        //        return DayWise;
        //    }
        //    catch (Exception ex)
        //    {
        //        string strMsg = ex.Message;
        //        return null;
        //    }
        //}
        //public List<GetMonthWiseReport> MonthWiseData()
        //{
        //    string StoredProc = string.Empty;
        //    try
        //    {
        //        StoredProc = "exec SP_GetMonthWiseReport ";


        //        List<GetMonthWiseReport> MonthWise = _context.GetMonthWiseReport.FromSqlRaw(StoredProc).ToList();
        //        return MonthWise;
        //    }
        //    catch (Exception ex)
        //    {
        //        string strMsg = ex.Message;
        //        return null;
        //    }
        //}
        //public List<TotalpurchaseReport> purchaseReportData()
        //{
        //    string StoredProc = string.Empty;
        //    try
        //    {
        //        StoredProc = "exec SP_TotalpurchaseReport ";


        //        List<TotalpurchaseReport> purchaseReport = _context.TotalpurchaseReport.FromSqlRaw(StoredProc).ToList();
        //        return purchaseReport;
        //    }
        //    catch (Exception ex)
        //    {
        //        string strMsg = ex.Message;
        //        return null;
        //    }
        //}

        [HttpPost]
        public JsonResult UserRegitration()
        {
           
            List<object> iData = new List<object>();
            //Creating sample data
            DataTable dt = new DataTable();
            dt.Columns.Add("Month", System.Type.GetType("System.String"));
            dt.Columns.Add("Registrations", System.Type.GetType("System.Int32"));

            DataRow dr = dt.NewRow();
            dr["Month"] = "January";
            dr["Registrations"] = 10;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Month"] = "Feburary";
            dr["Registrations"] = 50;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Month"] = "March";
            dr["Registrations"] = 60;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Month"] = "April";
            dr["Registrations"] = 60;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Month"] = "May";
            dr["Registrations"] = 90;
            dt.Rows.Add(dr);
            //Looping and extracting each DataColumn to List<Object>
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON
            return Json(iData);
        }

        //[HttpPost]
        //public JsonResult DayWiseReport(int _month, int _year)
        //{
        //    List<GetDayWiseReport> monthWise = DayWiseData(_month, _year);
        //    List<DaysReport> lstMonthwise = new List<DaysReport>();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Days", System.Type.GetType("System.String"));
        //    dt.Columns.Add("CourseCount", System.Type.GetType("System.Int32"));

        //    List<object> iData = new List<object>();
        //    if(monthWise!=null)
        //    {
        //        foreach (var itm in monthWise)
        //        {
        //            DataRow dr = dt.NewRow();
        //            dr["Days"] = itm.Days;
        //            dr["CourseCount"] = itm.CourseCount;
        //            dt.Rows.Add(dr);
        //        }
        //        //Creating sample           
        //        foreach (DataColumn dc in dt.Columns)
        //        {
        //            List<object> x = new List<object>();
        //            x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
        //            iData.Add(x);
        //        }
        //    }
            
        //    //Source data returned as JSON
        //    return Json(iData);
        //}
        //[HttpPost]
        //public JsonResult MonthWiseReport()
        //{
        //    List<GetMonthWiseReport> monthWise = MonthWiseData();
        //    List<MonthReport> lstMonthwise = new List<MonthReport>();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Month", System.Type.GetType("System.String"));
        //    dt.Columns.Add("MonthCount", System.Type.GetType("System.Int32"));
        //    foreach (var itm in monthWise)
        //    {
        //        DataRow dr = dt.NewRow();
        //        dr["Month"] = itm.Months;
        //        dr["MonthCount"] = itm.MonthCount;
        //        dt.Rows.Add(dr);
        //    }
        //    List<object> iData = new List<object>();
        //    //Creating sample           
        //    foreach (DataColumn dc in dt.Columns)
        //    {
        //        List<object> x = new List<object>();
        //        x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
        //        iData.Add(x);
        //    }
        //    //Source data returned as JSON
        //    return Json(iData);
        //}

        //[HttpPost]
        //public JsonResult CoursePurchaseReport()
        //{
        //    List<TotalpurchaseReport> totalpurchase = purchaseReportData();
        //    List<purchaseReport> lstpurchase = new List<purchaseReport>();

        //    List<object> iData = new List<object>();
        //    //Creating sample data
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("CourseName", System.Type.GetType("System.String"));
        //    dt.Columns.Add("CourseCount", System.Type.GetType("System.Int32"));
        //    foreach(var itm in totalpurchase)
        //    {
        //        purchaseReport purchase = new purchaseReport();
        //        DataRow dr = dt.NewRow();
        //        dr["CourseName"] = itm.CourseName;
        //        dr["CourseCount"] = itm.CourseCount;

        //        dt.Rows.Add(dr);
        //    }
        //    //Looping and extracting each DataColumn to List<Object>
        //    foreach (DataColumn dc in dt.Columns)
        //    {
        //        List<object> x = new List<object>();
        //        x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
        //        iData.Add(x);
        //    }
        //    //Source data returned as JSON
        //    return Json(iData);
        //}


        [HttpPost]
        public JsonResult CourseDistribution()
        {
            List<object> iData = new List<object>();
            //Creating sample data
            DataTable dt = new DataTable();
            dt.Columns.Add("CourseType", System.Type.GetType("System.String"));
            dt.Columns.Add("NoOfRegisteredUsers", System.Type.GetType("System.Int32"));
      

            DataRow dr = dt.NewRow();
            dr["CourseType"] = "Recorded Video";
            dr["NoOfRegisteredUsers"] = 15;

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["CourseType"] = "Live Classes";
            dr["NoOfRegisteredUsers"] = 10;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["CourseType"] = "Rasm-ul-Khat";
            dr["NoOfRegisteredUsers"] = 25;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["CourseType"] = "Alfaaz Part - I";
            dr["NoOfRegisteredUsers"] = 35;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["CourseType"] = "Alfaaz Part - II";
            dr["NoOfRegisteredUsers"] = 15;
            dt.Rows.Add(dr);
            //Looping and extracting each DataColumn to List<Object>
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON
            return Json(iData);
        }


    }
}
