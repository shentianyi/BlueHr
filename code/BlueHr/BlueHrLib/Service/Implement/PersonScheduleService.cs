using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Model.PageViewModel;

namespace BlueHrLib.Service.Implement
{
    public class PersonScheduleService : ServiceBase, IPersonScheduleService
    {

        private IPersonScheduleRepository personScheduleRep;

        public PersonScheduleService(string dbString) : base(dbString)
        {
            personScheduleRep = new PersonScheduleRepository(this.Context);
        }

        public bool Create(PersonSchedule personSchedule)
        {
            return personScheduleRep.Create(personSchedule);
        }

        public bool DeleteById(int id)
        {
            return personScheduleRep.DeleteById(id);
        }

        public PersonSchedule FindById(int id)
        {
            return personScheduleRep.FindById(id);
        }

        public IQueryable<PersonSchedule> Search(PersonScheduleSearchModel searchModel)
        {
            return personScheduleRep.Search(searchModel);
        }

        public bool Update(PersonSchedule personSchedule)
        {
            return personScheduleRep.Update(personSchedule);
        }

    }
}
