﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Repository
{
    public class RepositoryBase<T> : IRepository<T>
         where T : class
    {
        protected IDataContextFactory _dataContextFactory;

        public RepositoryBase(IDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;
        }

        public System.Data.Linq.Table<T> GetTable
        {
            get { return _dataContextFactory.Context.GetTable<T>(); }
        }
    }
}
