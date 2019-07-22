using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Context;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;

namespace TestBussiness.Repository
{
    public class TransactionStatusRepository : BaseRepository<TransactionStatus>, IRepositoryQuery
    {
        public TransactionStatusRepository(IContext context) : base(context)
        {
        }

        public TransactionStatus GetByKey(string key)
        {
            return GetBy(ts => ts.StatusKey == key);
        }
    }
}
