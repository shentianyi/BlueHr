using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IBankCardRepository
    {
        IQueryable<BankCard> Search(BankCardSearchModel searchModel);

        bool Create(BankCard bankCard);

        BankCard FindById(int id);

        bool Update(BankCard bankCard);

        bool DeleteById(int id);

        BankCard CreateFromAjax(BankCard bankCard);
    }
}
