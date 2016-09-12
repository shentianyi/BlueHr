using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service.Interface
{
    public interface IStaffService
    {
        /// <summary>
        /// 根据身份证号查询员工
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns></returns>
        Staff FindByStaffId(string id);

        /// <summary>
        /// 根据员工号查询员工
        /// </summary>
        /// <param name="nr">员工号</param>
        /// <returns></returns>
        Staff FindByNr(string nr);

        /// <summary>
        /// 通过身份证号验证员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckStaffById(string id);

        /// <summary>
        /// 验证员工并且更新员工信息
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        bool CheckStaffAndUpdateInfo(StaffIdCard card);

        /// <summary>
        /// 创建员工信息，并设立是否系统验证
        /// </summary>
        /// <param name="card"></param>
        /// <param name="isIdChecked"></param>
        /// <returns></returns>
        bool CreateInfoAndSetCheck(StaffIdCard card,bool isIdChecked = false);

        /// <summary>
        /// 根据日期，获取排班的员工工号
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        List<string> GetOnShiftStaffs(DateTime date);

        IQueryable<Staff> Search(StaffSearchModel searchModel);

        void Creates(List<Staff> staffs);

        bool Create(Staff staff);

        bool Update(Staff staff);

        Staff FindById(int id);

        bool DeleteByNr(string nr);

        bool ChangeJob(string[] changeJob);
        /// <summary>
        /// 员工转正
        /// </summary>
        /// <param name="record"></param>
        ResultMessage ToFullMember(FullMemberRecord record);

        /// <summary>
        /// 获取需要被转中的员工，
        /// 如果员工的计划转正时间小于参数datetime，则需要被转
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        List<Staff> GetToBeFullsLessThanDate(DateTime datetime);

        List<Staff> FindByJobTitleId(int id);

        List<Staff> FindByStaffType(int id);

        List<Staff> FindByDegreeType(int id);

        List<Staff> FindByInsureType(int id);
    }
}
