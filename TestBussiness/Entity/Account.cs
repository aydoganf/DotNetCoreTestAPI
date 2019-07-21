using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Context;
using TestBussiness.RepositoryService;

namespace TestBussiness.Entity
{
    public class Account : IEntity
    {
        #region IoC
        private IAccountRepository accountRepository;

        protected Account()
        {
        }

        public Account(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        #endregion

        public virtual int Id { get; protected set; }
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string IdentityNumber { get; protected set; }
        public virtual string AccountNumber { get; protected set; }
        public virtual DateTime CreateDate { get; protected set; }
        public virtual decimal Balance { get; protected set; }
        public virtual AccountType AccountType { get; protected set; }

        protected internal virtual Account With(string firstName, string lastName, string identityNumber, AccountType accountType)
        {
            FirstName = firstName;
            LastName = lastName;
            IdentityNumber = identityNumber;
            AccountType = accountType;
            AccountNumber = accountRepository.GetNextAccountNumber();
            CreateDate = DateTime.Now;
            Balance = 0m;
            accountRepository.Insert(this);
            return this;
        }

        protected internal virtual void Deposit(decimal amount)
        {
            Balance += amount;
        }

        protected internal virtual bool Withdraw(decimal amount)
        {
            if (Balance < amount)
                return false;
            Balance -= amount;
            return true;
        }

        protected internal virtual Account SetAccountOwnerName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            return this;
        }
    }
}
