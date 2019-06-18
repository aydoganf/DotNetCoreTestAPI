using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;
using System.Linq;
using TestBussiness.Context;

namespace TestBussiness.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(
            INHibernateHelper nHibernateHelper
            ) 
            : base(nHibernateHelper)
        {

        }

        public override Account Instance()
        {
            return new Account(this);
        }

        public string GetNextAccountNumber()
        {
            List<Account> accounts = base.GetAll();
            if (accounts.Count != 0)
            {
                int accountNumber = 
                    Convert.ToInt32(accounts
                    .OrderByDescending(i => i.CreateDate)
                    .FirstOrDefault()
                    .AccountNumber);
                accountNumber += 1;
                return accountNumber.ToString();
            }
            // this is just a number, not important.
            // we just want to make start number of accounts is that value
            return "1000000";
        }
        
        public Account GetAccountDetailById(int id)
        {
            return base.GetById(id);
        }

        public Account GetAccountDetailByAccountNumber(string accountNumber)
        {
            return session.Query<Account>()
                    .FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public List<Account> GetAccountListByIdentityNumber(string identityNumber)
        {
            return session.Query<Account>()
                    .Where(a => a.IdentityNumber == identityNumber)
                    .ToList();
        }

    }
}
