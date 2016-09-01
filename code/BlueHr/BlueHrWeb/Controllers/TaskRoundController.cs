using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueHrLib.Data;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Helpers;
using BlueHrWeb.Properties;
using MvcPaging;

namespace BlueHrWeb.Controllers
{
    public class TaskRoundController : Controller
    {
        // GET: TaskRound
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);
            ITaskRoundService ss = new TaskRoundService(Settings.Default.db);
            IPagedList<TaskRound> rounds = ss.List().ToPagedList(pageIndex, Settings.Default.pageSize);
            return View(rounds);
        }
    }
}