using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.RepositoryService
{
    public interface IAccountRepository : IRepository<Account>
    {
        string GetNextAccountNumber();
        Account GetAccountDetailById(int id);
        Account GetAccountDetailByAccountNumber(string accountNumber);
        List<Account> GetAccountListByIdentityNumber(string identityNumber);
    }
}
