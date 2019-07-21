using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.ServiceMessage;
using TestBussiness.Entity;
using TestBussiness.ManagerService;
using TestBussiness.RepositoryService;
using StructureMap;

namespace TestBussiness.Manager
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepository accountRepository;
        private readonly IAccountTypeRepository accountTypeRepository;
        private readonly IContainer container;

        public AccountManager(
            IContainer container,
            IAccountRepository accountRepository,
            IAccountTypeRepository accountTypeRepository)
        {
            this.container = container;
            this.accountRepository = accountRepository;
            this.accountTypeRepository = accountTypeRepository;
        }

        public List<Account> GetAll()
        {
            return accountRepository.GetAll();
        }

        public Account CreateNewAccount(string firstName, string lastName, string identityNumber, string accountTypeKey)
        {
            AccountType accountType = this.accountTypeRepository.GetByKey(accountTypeKey);
            if (accountType == null)
            {
                // return null or wtf response
                return null;
            }

            Account account = container.GetInstance<Account>()
                .With(firstName, lastName, identityNumber, accountType);
            return account;
        }

        public Account GetAccountByAccountNumber(string accountNumber)
        {
            return this.accountRepository.GetAccountByAccountNumber(accountNumber);
        }

        public Account GetAccountById(int id)
        {
            return this.accountRepository.GetAccountById(id);
        }

        public List<Account> GetAccountListByIdentityNumber(string identityNumber)
        {
            return this.accountRepository.GetAccountListByIdentityNumber(identityNumber);
        }

        public bool Deposit(string accountNumber, decimal amount)
        {
            Account account = this.GetAccountByAccountNumber(accountNumber);
            if (account == null)
                return false; // todo: when account is not found this is not true response

            account.Deposit(amount);
            Update(account);
            return true;
        }

        public bool Withdraw(string accountNumber, decimal amount)
        {
            Account account = this.GetAccountByAccountNumber(accountNumber);
            if (account == null)
                return false; // todo: when account is not found this is not true response

            bool isOk = account.Withdraw(amount);
            if (isOk)
                Update(account);

            return isOk;
        }

        public Account SetAccountOwnerName(string accountNumber, string firstName, string lastName)
        {
            Account account = this.GetAccountByAccountNumber(accountNumber);
            if (account == null)
                return null;

            return Update(account.SetAccountOwnerName(firstName, lastName));
        }

        #region Entity operations
        private Account Update(Account account)
        {
            return accountRepository.Update(account); 
        }
        #endregion
    }
}
