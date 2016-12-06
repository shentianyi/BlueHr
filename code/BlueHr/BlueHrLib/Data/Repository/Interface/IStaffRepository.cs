using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IStaffRepository
    {
        IQueryable<Staff> Search(StaffSearchModel searchModel);

        bool Create(Staff staff);

        bool Update(Staff staff);

        Staff FindById(int id);

        bool DeleteByNr(string nr);

        List<Staff> FindByJobTitleId(int id);

        List<Staff> FindByStaffType(int id);

        List<Staff> FindByDegreeType(int id);

        List<Staff> FindByInsureType(int id);

        bool IsStaffExist(string nr);
        List<Staff> SearchPermanentStaff(StaffSearchModel searchModel);
        //List<Staff> getStaffUserIDCard();

        List<Staff> FindByCompanyAndDepartment(int companyId, int? departmentId);

        List<Staff> GetAllTableName();

        IQueryable<Staff> AdvancedSearch(string AllTableName, string SearchConditions, string SearchValueFirst);
        
        int CountStaff(int workStatus);
        
        List<Staff> SearchOnTrialStaff(StaffSearchModel q);
        
        Dictionary<string, string> StaffCount();

        List<Staff> StaffBirthday(int v);
        List<Staff> ContractExpiredDetail(int v);
        List<Staff> ToEmployeesDetail(int v);
    }
}
