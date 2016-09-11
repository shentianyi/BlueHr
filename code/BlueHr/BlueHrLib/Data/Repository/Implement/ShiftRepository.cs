using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Data.Repository.Implement
{
    public class ShiftRepository : RepositoryBase<Shift>, IShiftRepository
    {
        private BlueHrDataContext context;

        public ShiftRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public List<Shift> All()
        {
            return this.context.GetTable<Shift>().ToList();
        }


        public IQueryable<Shift> Search(ShiftSearchModel searchModel)
        {
            //TODO
            IQueryable<Shift> shifts = this.context.Shift;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                shifts = shifts.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }
            return shifts;
        }

        public bool Create(Shift parModel)
        {
            try
            {
                this.context.GetTable<Shift>().InsertOnSubmit(parModel);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            var dep = this.context.GetTable<Shift>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<Shift>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Shift FindById(int id)
        {
            return this.context.GetTable<Shift>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(Shift parModel)
        {
            var dep = this.context.GetTable<Shift>().FirstOrDefault(c => c.id.Equals(parModel.id));
            if (dep != null)
            {
                dep.code = parModel.code;
                dep.name = parModel.name;
                dep.startAt = parModel.startAt;
                dep.endAt = parModel.endAt;
                dep.shiftType = parModel.shiftType;
                dep.remark = parModel.remark;

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
