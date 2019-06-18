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

        public Account()
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
            return this;
        }

        protected internal virtual Account Save()
        {
            return accountRepository.Insert(this);
        }

        protected internal virtual bool Deposit(decimal amount)
        {
            if (accountRepository == null)
                throw new Exception("hoaydaaa!");
            Balance += amount;
            accountRepository.Update(this, Id);
            return true;
        }

        protected internal virtual bool Withdraw(decimal amount)
        {
            try
            {
                if (Balance >= amount)
                {
                    Balance -= amount;
                    accountRepository.Update(this, Id);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected internal virtual Account SetAccountOwnerName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            return accountRepository.Update(this, Id);
        }
    }
}
