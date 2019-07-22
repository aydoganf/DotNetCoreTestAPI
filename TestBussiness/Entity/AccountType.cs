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
        private readonly IRepository<AccountType> repository;

        protected AccountType()
        {

        }

        public AccountType(IRepository<AccountType> repository)
        {
            this.repository = repository;
        }
        #endregion

        public virtual int Id { get; protected set; }
        public virtual string TypeName { get; protected set; }
        public virtual string TypeKey { get; protected set; }
        public virtual List<Account> Accounts { get; protected set; }
    }
}
