using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.RepositoryService;

namespace TestBussiness.Entity
{
    public class AccountType : IEntity
    {
        #region IoC
        private INHibernateHelper nHibernateHelper;
        private IAccountTypeRepository accountRepository;

        protected AccountType()
        {

        }

        public AccountType(INHibernateHelper nHibernateHelper, IAccountTypeRepository accountTypeRepository)
        {
            this.nHibernateHelper = nHibernateHelper;
            this.accountRepository = accountTypeRepository;
        }
        #endregion

        public virtual int Id { get; protected set; }
        public virtual string TypeName { get; protected set; }
        public virtual string TypeKey { get; protected set; }
    }
}
