using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueHrLib.Data.Model.Search;

namespace BlueHrLib.Data.Repository.Implement
{
    public class PersonScheduleRepository : RepositoryBase<PersonSchedule>, IPersonScheduleRepository
    {
        private BlueHrDataContext context;

        public PersonScheduleRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public bool Create(PersonSchedule personSchedule)
        {
            try
            {
                this.context.GetTable<PersonSchedule>().InsertOnSubmit(personSchedule);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            var staffType = this.context.GetTable<PersonSchedule>().FirstOrDefault(c => c.id.Equals(id));
            if (staffType != null)
            {
                this.context.GetTable<PersonSchedule>().DeleteOnSubmit(staffType);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public PersonSchedule FindById(int id)
        {
            return this.context.GetTable<PersonSchedule>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(PersonSchedule personSchedule)
        {
            var st = this.context.GetTable<PersonSchedule>().FirstOrDefault(c => c.id.Equals(personSchedule.id));
            if (st != null)
            {
                st.startTime = personSchedule.startTime;
                st.endTime = personSchedule.endTime;
                st.category = personSchedule.category;
                st.subject = personSchedule.subject;
                st.context = personSchedule.context; 
                st.isDeleted = personSchedule.isDeleted; 
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<PersonSchedule> Search(PersonScheduleSearchModel searchModel)
        {
            IQueryable<PersonSchedule> personSchedules = this.context.PersonSchedule;
            if (!string.IsNullOrEmpty(searchModel.Subject))
            {
                personSchedules = personSchedules.Where(c => c.subject.Contains(searchModel.Subject.Trim()));
            }

            if (!string.IsNullOrEmpty(searchModel.Context))
            {
                personSchedules = personSchedules.Where(c => c.context.Contains(searchModel.Context.Trim()));
            }

            return personSchedules;
        }
    }
}
