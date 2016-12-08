using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IPersonScheduleRepository
    {
        IQueryable<PersonSchedule> Search(PersonScheduleSearchModel searchModel);
        PersonSchedule Create(PersonSchedule personSchedule);

        PersonSchedule FindById(int id);

        bool Update(PersonSchedule personSchedule);

        bool DeleteById(int id);
    }
}
