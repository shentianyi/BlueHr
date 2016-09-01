using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Helpers;
using BlueHrWeb.Properties;
using MvcPaging;
using BlueHrLib.Helper.Excel;
using BlueHrLib.Data.Message;

namespace BlueHrWeb.Controllers
{
    public class AttendanceRecordDetailController : Controller
    {
        // GET: AttendanceRecordDetail
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);
            AttendanceRecordDetailSearchModel q = new AttendanceRecordDetailSearchModel();
            IAttendanceRecordService ss = new AttendanceRecordService(Settings.Default.db);

            IPagedList<AttendanceRecordDetailView> records = ss.SearchDetailView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View(records);
        }

        public ActionResult Search([Bind(Include = "CompanyId,DepartmentId,StaffNr,RecordAtFrom,RecordAtEnd")] AttendanceRecordDetailSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IAttendanceRecordService ss = new AttendanceRecordService(Settings.Default.db);

            IPagedList<AttendanceRecordDetailView> records = ss.SearchDetailView(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", records);
        }

        public ActionResult Import()
        {
            var ff = Request.Files[0];
            string fileName = FileHelper.SaveAsTmp(ff);
            AttendanceRecordDetailExcelHelper helper = new AttendanceRecordDetailExcelHelper(Settings.Default.db, fileName);
            ImportMessage msg = helper.Import();

            return Json(msg);
        }


        private void SetJobTitleList(int? type, bool allowBlank = true)
        {
            IJobTitleService js = new JobTitleService(Settings.Default.db);

            JobTitleSearchModel jtsm = new JobTitleSearchModel();

            List<JobTitle> jt = js.Search(jtsm).ToList();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var it in jt)
            {
                if (type.HasValue && type.ToString().Equals(it.id))
                {
                    select.Add(new SelectListItem { Text = it.name, Value = it.id.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.name, Value = it.id.ToString(), Selected = false });
                }
            }
            ViewData["jobTitleList"] = select;
        }

    }
}