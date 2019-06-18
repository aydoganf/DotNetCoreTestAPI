using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.RepositoryService
{
    public interface IAccountTypeRepository : IRepository<AccountType>
    {
        AccountType GetByKey(string typeKey);
    }
}
