using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Context;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;

namespace TestBussiness.Repository
{
    public class AccountTransactionRepository : BaseRepository<AccountTransaction>, IRepositoryQuery
    {
        public AccountTransactionRepository(IContext context) : base(context)
        {
        }

    }
}
