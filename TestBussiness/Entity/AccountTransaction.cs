using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.RepositoryService;

namespace TestBussiness.Entity
{
    public class AccountTransaction : IEntity
    {
        #region IoC
        private readonly IRepository<AccountTransaction> repository;

        protected AccountTransaction()
        {
        }

        public AccountTransaction(IRepository<AccountTransaction> repository)
        {
            this.repository = repository;
        }
        #endregion

        public virtual int Id { get; protected set; }
        public virtual decimal TransactionAmount { get; protected set; }
        public virtual DateTime CreateDate { get; protected set; }
        public virtual Account Account { get; protected set; }
        public virtual TransactionStatus TransactionStatus { get; protected set; }

        public virtual AccountTransaction With(decimal transactionAmount, Account account, TransactionStatus transactionStatus)
        {
            TransactionAmount = transactionAmount;
            Account = account;
            TransactionStatus = transactionStatus;
            CreateDate = DateTime.Now;

            repository.Insert(this);

            return this;
        }
    }
}
