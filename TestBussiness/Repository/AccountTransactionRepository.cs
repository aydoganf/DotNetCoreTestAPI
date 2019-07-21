using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;

namespace TestBussiness.Repository
{
    public class AccountTransactionRepository : BaseRepository<AccountTransaction>, IAccountTransactionRepository
    {
        public AccountTransactionRepository(INHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
        }

        public override AccountTransaction Instance()
        {
            return new AccountTransaction(this);
        }
    }
}
