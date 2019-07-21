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
        private readonly IAccountTransactionRepository accountTransactionRepository;
        private readonly ITransactionStatusRepository transactionStatusRepository;
        private readonly IContainer container;

        public AccountManager(
            IContainer container,
            IAccountRepository accountRepository,
            IAccountTypeRepository accountTypeRepository,
            IAccountTransactionRepository accountTransactionRepository,
            ITransactionStatusRepository transactionStatusRepository)
        {
            this.container = container;
            this.accountRepository = accountRepository;
            this.accountTypeRepository = accountTypeRepository;
            this.accountTransactionRepository = accountTransactionRepository;
            this.transactionStatusRepository = transactionStatusRepository;
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

            bool depositSucceed = false;
            bool transactionInsertationSucceed = false;

            try
            {
                account.Deposit(amount);
                Update(account);
                depositSucceed = true;

                int a = Convert.ToInt32("a");
                var transactionStatus = transactionStatusRepository.GetByKey(TransactionStatus.SUCCEED);
                var accountTransaction = container.GetInstance<AccountTransaction>()
                    .With(amount, account, transactionStatus);
                transactionInsertationSucceed = true;
            }
            catch (Exception ex)
            {
                if (depositSucceed && !transactionInsertationSucceed)
                {
                    // revert deposit operation
                    account.Withdraw(amount);
                    Update(account);

                    var transactionStatus = transactionStatusRepository.GetByKey(TransactionStatus.FAILED);
                    container.GetInstance<AccountTransaction>()
                        .With(amount, account, transactionStatus);
                }
                return false;
            }

            return true;
        }

        public bool Withdraw(string accountNumber, decimal amount)
        {
            Account account = this.GetAccountByAccountNumber(accountNumber);
            if (account == null)
                return false; // todo: when account is not found this is not true response

            bool withdrawSucceed = false;
            bool transactionInsertationSucceed = false;

            try
            {
                account.Withdraw(amount);
                Update(account);
                withdrawSucceed = true;

                var transactionStatus = transactionStatusRepository.GetByKey(TransactionStatus.SUCCEED);
                container.GetInstance<AccountTransaction>()
                    .With(-amount, account, transactionStatus);
                transactionInsertationSucceed = true;
            }
            catch (Exception ex)
            {
                if (withdrawSucceed && !transactionInsertationSucceed)
                {
                    // revert withdraw
                    account.Deposit(amount);
                    Update(account);

                    var transactionStatus = transactionStatusRepository.GetByKey(TransactionStatus.FAILED);
                    container.GetInstance<AccountTransaction>()
                        .With(-amount, account, transactionStatus);
                }
                return false;
            }
            

            return true;
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
