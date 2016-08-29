using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service.Interface
{
    public interface IAttendanceRecordService
    {
        /// <summary>
        /// 将Detail的数据计算到Cal中
        /// </summary>
        /// <param name="date">计算的时间</param>
        void CalculateAttendRecord(DateTime date);
    }
}
