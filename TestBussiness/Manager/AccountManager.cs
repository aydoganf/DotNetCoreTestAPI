using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Context;
using TestBussiness.ServiceMessage;
using TestBussiness.Entity;
using TestBussiness.ManagerService;
using TestBussiness.RepositoryService;

namespace TestBussiness.Manager
{
    public class AccountManager : IAccountManagerService
    {
        private IAccountRepository accountRepository;
        private IAccountTypeRepository accountTypeRepository;

        public AccountManager(
            IAccountRepository accountRepository,
            IAccountTypeRepository accountTypeRepository)
        {
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

            Account account = this.accountRepository
                .Instance()
                .With(firstName, lastName, identityNumber, accountType)
                .Save();
            
            return account;
        }

        public Account GetAccountByAccountNumber(string accountNumber)
        {
            return this.accountRepository.GetAccountDetailByAccountNumber(accountNumber);
        }

        public Account GetAccountById(int id)
        {
            return this.accountRepository.GetAccountDetailById(id);
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

            return account.Deposit(amount);
        }

        public bool Withdraw(string accountNumber, decimal amount)
        {
            Account account = this.GetAccountByAccountNumber(accountNumber);
            if (account == null)
                return false; // todo: when account is not found this is not true response

            return account.Withdraw(amount);
        }

        public Account SetAccountOwnerName(string accountNumber, string firstName, string lastName)
        {
            Account account = this.GetAccountByAccountNumber(accountNumber);
            if (account == null)
                return null;

            return account.SetAccountOwnerName(firstName, lastName);
        }
    }
}
