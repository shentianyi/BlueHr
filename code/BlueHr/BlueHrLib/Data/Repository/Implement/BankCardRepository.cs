using System;
using System.Linq;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;

namespace BlueHrLib.Data.Repository.Implement
{
    public class BankCardRepository : RepositoryBase<BankCard>, IBankCardRepository
    {
        private BlueHrDataContext context;

        public BankCardRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public bool Create(BankCard bankCard)
        {
            try
            {
                this.context.GetTable<BankCard>().InsertOnSubmit(bankCard);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public BankCard CreateFromAjax(BankCard bankCard)
        {
            try
            {
                this.context.GetTable<BankCard>().InsertOnSubmit(bankCard);
                this.context.SubmitChanges();

                return bankCard;
            }
            catch (Exception e)
            {
                Console.Write(e);

                return null;
            }
        }

        public bool DeleteById(int id)
        {
            var bc = this.context.GetTable<BankCard>().FirstOrDefault(c => c.id.Equals(id));
            if (bc != null)
            {
                this.context.GetTable<BankCard>().DeleteOnSubmit(bc);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public BankCard FindById(int id)
        {
            return this.context.GetTable<BankCard>().FirstOrDefault(c => c.id.Equals(id));
        }

        public IQueryable<BankCard> Search(BankCardSearchModel searchModel)
        {
            IQueryable<BankCard> bankCards = this.context.BankCard;

            if (!string.IsNullOrEmpty(searchModel.Nr))
            {
                bankCards = bankCards.Where(c => c.nr.Contains(searchModel.Nr.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Bank))
            {
                bankCards = bankCards.Where(c => c.bank.Contains(searchModel.Bank.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.BankAddress))
            {
                bankCards = bankCards.Where(c => c.bankAddress.Contains(searchModel.BankAddress.Trim()));
            }

            return bankCards;
        }

        public bool Update(BankCard bankCard)
        {
            var bc = this.context.GetTable<BankCard>().FirstOrDefault(c => c.id.Equals(bankCard.id));
            if (bc != null)
            {
                bc.nr = bankCard.nr;
                bc.bank = bankCard.bank;
                bc.bankAddress = bankCard.bankAddress;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
