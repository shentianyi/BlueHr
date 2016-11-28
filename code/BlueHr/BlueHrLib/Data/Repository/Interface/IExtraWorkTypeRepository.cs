﻿using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IExtraWorkTypeRepository
    {
        List<ExtraWorkType> All();
        List<ExtraWorkType> GetAllTableName();
    }
}