using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Excel;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Service.Interface
{
    public interface IExcelReportService
    {
        /// <summary>
        /// SearchModel，使用 公司，部门，是否离职，员工号 做搜索
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        List<WorkSumExcelModel> GetSumExcelModels(DateTime startDate, DateTime endDate, StaffSearchModel searchModel, double maxExtraTotalHour = 36);
    }
}
