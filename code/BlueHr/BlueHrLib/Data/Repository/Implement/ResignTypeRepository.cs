using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{

    public class ResignTypeRepository : RepositoryBase<ResignType>, IResignTypeRepository
    {
        private BlueHrDataContext context;

        public ResignTypeRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<ResignType> Search(ResignTypeSearchModel searchModel)
        {
            IQueryable<ResignType> resignTypes = this.context.ResignType;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                resignTypes = resignTypes.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }
            return resignTypes;
        }

        public bool Create(ResignType resignType)
        {
            try
            {
                this.context.GetTable<ResignType>().InsertOnSubmit(resignType);
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
            var dep = this.context.GetTable<ResignType>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<ResignType>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ResignType FindById(int id)
        {
            return this.context.GetTable<ResignType>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(ResignType title)
        {
            var dep = this.context.GetTable<ResignType>().FirstOrDefault(c => c.id.Equals(title.id));
            if (dep != null)
            {
                dep.name = title.name;
                dep.remark = title.remark;
                dep.code = title.code;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ResignType> GetAll()
        {
            return this.context.GetTable<ResignType>().ToList();
        }

        public ResignType FindByName(string name)
        {
            try
            {
                return this.context.GetTable<ResignType>().FirstOrDefault(c => c.name.Equals(name));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ResignType IsResignTypeExit(string resignType)
        {
            ResignType getResignType = FindByName(resignType);

            if (getResignType != null)
            {
                //代表已经存在
                return getResignType;
            }else
            {
                ResignType rt = new ResignType();

                rt.code = resignType;
                rt.name = resignType;
                rt.remark = resignType;

                try
                {
                    bool Result = Create(rt);

                    if (Result)
                    {
                        return IsResignTypeExit(rt.name);
                    }else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
