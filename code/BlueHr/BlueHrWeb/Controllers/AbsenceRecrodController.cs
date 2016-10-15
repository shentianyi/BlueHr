using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Helper;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Helpers;
using BlueHrWeb.Properties;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BlueHrWeb.CustomAttributes;
using BlueHrLib.Helper.Excel;
using BlueHrLib.Data.Message;

namespace BlueHrWeb.Controllers
{
    public class AbsenceRecrodController : Controller
    {
        // GET: AbsenceRecrod
        [UserAuthorize]
        public ActionResult Index(int? page)
        {
            SetDropDownList(null);

            int pageIndex = PagingHelper.GetPageIndex(page);

            AbsenceRecrodSearchModel q = new AbsenceRecrodSearchModel();

            IAbsenceRecordService ss = new AbsenceRecordService(Settings.Default.db);

            IPagedList<AbsenceRecrod> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q; 

            AbsenceRecrodInfoModel info = ss.GetAbsenceRecrodInfo(q);
            ViewBag.Info = info;

            return View(models);
        }

        public ActionResult Search([Bind(Include = "staffNr,absenceTypeId,durStart,durEnd")] AbsenceRecrodSearchModel q)
        {
            SetDropDownList(null);

            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IAbsenceRecordService ss = new AbsenceRecordService(Settings.Default.db);

            IPagedList<AbsenceRecrod> models = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", models);
        }

        // GET: AbsenceRecrod/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AbsenceRecrod/Create
        public ActionResult Create()
        {
            SetDropDownList(null);
            return View();
        }

        // POST: AbsenceRecrod/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "absenceTypeId,staffNr,startHour,endHour, duration,durationType,remark,absenceDate")] AbsenceRecrod model)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(model);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.durationType = (int)DurationType.Hour;
                    IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);
                    bool isSucceed = cs.Create(model);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "添加失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: AbsenceRecrod/Edit/5
        public ActionResult Edit(int id)
        {
            IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);

            AbsenceRecrod jt = cs.FindById(id);
            SetDropDownList(jt);
            return View(jt);
        }

        // POST: AbsenceRecrod/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, absenceTypeId,staffNr,startHour,endHour, duration,durationType,remark,absenceDate")] AbsenceRecrod model)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(model);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.durationType = (int)DurationType.Hour;
                    IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);
                    bool isSucceed = cs.Update(model);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "更新失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: AbsenceRecrod/Delete/5
        public ActionResult Delete(int id)
        {
            IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);

            AbsenceRecrod cp = cs.FindById(id);
            SetDropDownList(cp);
            return View(cp);
        }

        // POST: AbsenceRecrod/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                ////存在员工时不可删除
                //IAbsenceRecordService shfSi = new AbsenceRecordService(Settings.Default.db);
                //List<AbsenceRecrod> shf = shfSi.FindByAbsenceType(id);

                //if (null != shf && shf.Count() > 0)
                //{
                //    msg.Success = false;
                //    msg.Content = "缺勤类型正在使用,不能删除!";

                //    return Json(msg, JsonRequestBehavior.AllowGet);
                //}
                //else
                {
                    IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);
                    bool isSucceed = cs.DeleteById(id);

                    msg.Success = isSucceed;
                    msg.Content = isSucceed ? "" : "删除失败";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage() { Success = false, Content = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void SetDropDownList(AbsenceRecrod model)
        {
            if (model != null)
            {
                SetAbsenceTypeList(model.absenceTypeId);
                SetDurationTypeCodeList(model.durationType);
            }
            else
            {
                SetAbsenceTypeList(null);
                SetDurationTypeCodeList(100);
            }
        }

        private void SetAbsenceTypeList(int? type, bool allowBlank = true)
        {
            IAbsenceTypeService cs = new AbsenceTypeService(Settings.Default.db);

            AbsenceTypeSearchModel csm = new AbsenceTypeSearchModel();

            List<AbsenceType> certType = cs.Search(csm).ToList();

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var certt in certType)
            {
                if (type.HasValue && type.ToString().Equals(certt.id))
                {
                    select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = certt.name, Value = certt.id.ToString(), Selected = false });
                }
            }
            ViewData["absenceTypeList"] = select;
        }

        private void SetDurationTypeCodeList(int? model, bool allowBlank = true)
        {
            List<EnumItem> item = EnumHelper.GetList(typeof(DurationType));

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var it in item)
            {
                if (model.HasValue && model.ToString().Equals(it.Value))
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
                }
            }
            ViewData["durationTypeList"] = select;
        }

        public ActionResult Import()
        {
            var ff = Request.Files[0];
            string fileName = BlueHrWeb.Helpers.FileHelper.SaveAsTmp(ff);
            AbsenceRecordExcelHelper helper = new AbsenceRecordExcelHelper(Settings.Default.db, fileName);
            ImportMessage msg = helper.Import();

            //添加"text/html",防止IE 自动下载json 格式返回的数据
            return Json(msg, "text/html");
        }

        [HttpPost]
        //•	员工号（输入，不可空）
        //•	缺勤类别（选择，不可空）
        //•	缺勤原因（输入，可空），
        //•	缺勤的小时或天长。（输入，不可空）
        //•	时间单位（选择，不可空，选项为： 小时/天，默认为小时） 
        public ResultMessage DoValidation(AbsenceRecrod model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.staffNr))
            {
                msg.Success = false;
                msg.Content = "员工号不能为空";

                return msg;
            }
            else
            {
                IStaffService ss = new StaffService(Settings.Default.db);
                if (ss.FindByNr(model.staffNr) == null)
                {
                    msg.Success = false;
                    msg.Content = "员工号不存在";

                    return msg;
                }
            }

            if (model.absenceTypeId <= 0)
            {
                msg.Success = false;
                msg.Content = "缺勤类别不能为空";

                return msg;
            }

            if (model.absenceDate ==null)
            {
                msg.Success = false;
                msg.Content = "缺勤时间不能为空，或格式必须正确";

                return msg;
            }

            if ( model.duration <= 0)
            {
                msg.Success = false;
                msg.Content = "缺勤时长不能为空";

                return msg;
            }

            //IAbsenceRecordService cs = new AbsenceRecordService(Settings.Default.db);
            //List<AbsenceRecrod> abs = cs.GetAll();

            //if (model.id <= 0)
            //{
            //    bool isRecordExists = abs.Where(p => p.staffNr == model.staffNr || p.absenceTypeId == model.absenceTypeId
            //    || p.remark == model.remark || p.duration == model.duration).ToList().Count() > 0;

            //    if (isRecordExists)
            //    {
            //        msg.Success = false;
            //        msg.Content = "数据已经存在!";

            //        return msg;
            //    }
            //}
            //else
            //{
            //    bool isRecordExists = abs.Where(p => (p.staffNr == model.staffNr || p.absenceTypeId == model.absenceTypeId
            //    || p.remark == model.remark || p.duration == model.duration) && p.id != model.id).ToList().Count() > 0;

            //    if (isRecordExists)
            //    {
            //        msg.Success = false;
            //        msg.Content = "数据已经存在!";

            //        return msg;
            //    }
            //}

            return new ResultMessage() { Success = true, Content = "" };
        }
    }
}

