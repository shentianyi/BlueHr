using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service.Interface
{
    public interface IBankCardService
    {
        IQueryable<BankCard> Search(BankCardSearchModel searchModel);

        BankCard FindById(int id);

        bool Create(BankCard title);

        bool Update(BankCard title);

        bool DeleteById(int id);
    }
}
