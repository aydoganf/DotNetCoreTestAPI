using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;
using System.Linq;
using TestBussiness.Context;
using NHibernate;

namespace TestBussiness.Repository
{
    public class AccountRepository : BaseRepository<Account>, IRepositoryQuery
    {
        public AccountRepository(IContext context) : base(context)
        {
        }

        public string GetNextAccountNumber()
        {
            List<Account> accounts = GetAll();
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
        
        public Account GetAccountById(int id)
        {
            return GetById(id);
        }

        public Account GetAccountByAccountNumber(string accountNumber)
        {
            return GetBy(a => a.AccountNumber == accountNumber);
        }

        public List<Account> GetAccountListByIdentityNumber(string identityNumber)
        {
            return GetListBy(a => a.IdentityNumber == identityNumber);
        }

    }
}
