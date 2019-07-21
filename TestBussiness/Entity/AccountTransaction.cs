using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.RepositoryService;

namespace TestBussiness.Entity
{
    public class AccountTransaction : IEntity
    {
        private readonly IAccountTransactionRepository repository;
        protected AccountTransaction()
        {
        }

        public AccountTransaction(IAccountTransactionRepository repository)
        {
            this.repository = repository;
        }

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
