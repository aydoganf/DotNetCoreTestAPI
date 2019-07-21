using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;

namespace TestBussiness.Repository
{
    public class TransactionStatusRepository : BaseRepository<TransactionStatus>, ITransactionStatusRepository
    {
        public TransactionStatusRepository(INHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
        }

        public TransactionStatus GetByKey(string key)
        {
            return session.Query<TransactionStatus>()
                .FirstOrDefault(ts => ts.StatusKey == key);
        }

        public override TransactionStatus Instance()
        {
            return new TransactionStatus(this);
        }
    }
}
