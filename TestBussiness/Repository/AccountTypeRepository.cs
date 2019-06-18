using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;
using System.Linq;

namespace TestBussiness.Repository
{
    public class AccountTypeRepository : BaseRepository<AccountType>, IAccountTypeRepository
    {
        public AccountTypeRepository(INHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
        }

        public AccountType GetByKey(string typeKey)
        {
            using (var session = base.OpenSession())
            {
                return session.Query<AccountType>()
                    .FirstOrDefault(at => at.TypeKey == typeKey);                    
            }
        }

        public override AccountType Instance()
        {
            return new AccountType(nhibernateHelper, this);
        }
    }
}
