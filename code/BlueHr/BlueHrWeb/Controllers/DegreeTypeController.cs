using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Search;
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

namespace BlueHrWeb.Controllers
{
    public class DegreeTypeController : Controller
    {
        // GET: DegreeType
        [UserAuthorize]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            DegreeTypeSearchModel q = new DegreeTypeSearchModel();

            IDegreeTypeService ss = new DegreeTypeService(Settings.Default.db);

            IPagedList<DegreeType> degreeTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            DegreeTypeInfoModel info = ss.GetDegreeTypeInfo(q);
            ViewBag.Info = info;

            return View(degreeTypes);
        }

        public ActionResult Search([Bind(Include = "Name")] DegreeTypeSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IDegreeTypeService ss = new DegreeTypeService(Settings.Default.db);

            IPagedList<DegreeType> degreeTypes = ss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            return View("Index", degreeTypes);
        }

        // GET: DegreeType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DegreeType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DegreeType/Create
        [HttpPost]
        public JsonResult Create([Bind(Include = "Name, remark")] DegreeType degreeType)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(degreeType);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IDegreeTypeService cs = new DegreeTypeService(Settings.Default.db);
                    bool isSucceed = cs.Create(degreeType);

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

        // GET: DegreeType/Edit/5
        public ActionResult Edit(int id)
        {
            IDegreeTypeService cs = new DegreeTypeService(Settings.Default.db);

            DegreeType dgt = cs.FindById(id);

            return View(dgt);
        }

        // POST: DegreeType/Edit/5
        [HttpPost]
        public JsonResult Edit([Bind(Include = "id, name, remark")] DegreeType degreeType)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                msg = DoValidation(degreeType);

                if (!msg.Success)
                {
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IDegreeTypeService cs = new DegreeTypeService(Settings.Default.db);
                    bool isSucceed = cs.Update(degreeType);

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

        // GET: DegreeType/Delete/5
        public ActionResult Delete(int id)
        {
            IDegreeTypeService cs = new DegreeTypeService(Settings.Default.db);

            DegreeType dgt = cs.FindById(id);

            return View(dgt);
        }

        // POST: DegreeType/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            ResultMessage msg = new ResultMessage();

            try
            {
                //存在员工时不可删除
                IStaffService shfSi = new StaffService(Settings.Default.db);
                List<Staff> shf = shfSi.FindByDegreeType(id);

                if (null != shf && shf.Count() > 0)
                {
                    msg.Success = false;
                    msg.Content = "学历信息正在使用,不能删除!";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    IDegreeTypeService cs = new DegreeTypeService(Settings.Default.db);
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

        [HttpPost]
        //4.3	学历管理
        //（列表（分页）、新建、编辑、删除（存在员工时不可删除）
        //）：名称（不可空），备注（可空） 
        public ResultMessage DoValidation(DegreeType model)
        {
            ResultMessage msg = new ResultMessage();

            if (string.IsNullOrEmpty(model.name))
            {
                msg.Success = false;
                msg.Content = "学历名称不能为空";

                return msg;
            }

            IDegreeTypeService cs = new DegreeTypeService(Settings.Default.db);
            List<DegreeType> shift = cs.GetAll();

            if (model.id <= 0)
            {
                bool isRecordExists = shift.Where(p => p.name == model.name).ToList().Count() > 0;

                if (isRecordExists)
                {
                    msg.Success = false;
                    msg.Content = "数据已经存在!";

                    return msg;
                }
            }
            else
            {
                bool isRecordExists = shift.Where(p => p.name == model.name && p.id != model.id).ToList().Count() > 0;

                if (isRecordExists)
                {
                    msg.Success = false;
                    msg.Content = "数据已经存在!";

                    return msg;
                }
            }

            return new ResultMessage() { Success = true, Content = "" };
        }
    }
}
