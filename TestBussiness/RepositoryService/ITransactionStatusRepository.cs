using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.RepositoryService
{
    public interface ITransactionStatusRepository : IRepository<TransactionStatus>
    {
        TransactionStatus GetByKey(string key);
    }
}
