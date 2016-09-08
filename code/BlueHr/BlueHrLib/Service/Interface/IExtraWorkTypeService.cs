using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
  
   public interface IExtraWorkTypeService
    {
        List<ExtraWorkType> All();
    }
}
