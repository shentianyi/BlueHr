﻿using System;
using System.Linq;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;

namespace BlueHrLib.Service.Implement
{
    public class BankCardService : ServiceBase, IBankCardService
    {
        private IBankCardRepository bankCardRep;

        public BankCardService(string dbString) : base(dbString)
        {
            bankCardRep = new BankCardRepository(this.Context);
        }

        public IQueryable<BankCard> Search(BankCardSearchModel searchModel)
        {
            return bankCardRep.Search(searchModel);
        }

        public bool Create(BankCard bankCard)
        {
            return bankCardRep.Create(bankCard);
        }

        public bool DeleteById(int id)
        {
            return bankCardRep.DeleteById(id);
        }

        public BankCard FindById(int id)
        {
            return bankCardRep.FindById(id);
        }

        public bool Update(BankCard bankCard)
        {
            return bankCardRep.Update(bankCard);
        }

        public BankCard CreateFromAjax(BankCard bankCard)
        {
            return bankCardRep.CreateFromAjax(bankCard);
        }
    }
}
