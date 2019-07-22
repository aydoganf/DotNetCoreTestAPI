using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;
using System.Linq;
using NHibernate;
using TestBussiness.Context;

namespace TestBussiness.Repository
{
    public class AccountTypeRepository : BaseRepository<AccountType>, IRepositoryQuery
    {
        public AccountTypeRepository(IContext context) : base(context)
        {
        }

        public AccountType GetByKey(string typeKey)
        {
            return GetBy(at => at.TypeKey == typeKey);
        }
    }
}
