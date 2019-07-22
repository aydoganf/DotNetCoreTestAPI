using System;
using System.Collections.Generic;
using TestBussiness.Entity;
using TestBussiness.ManagerService;
using TestBussiness.Repository;
using IContext = TestBussiness.Context.IContext;

namespace TestBussiness.Manager
{
    public class AccountManager : IAccountManager
    {
        #region IoC
        private readonly IContext context;

        public AccountManager(IContext context)
        {
            this.context = context;
        }
        #endregion

        public List<Account> GetAll()
        {
            return context.Query<AccountRepository>().GetAll();
        }

        public Account CreateNewAccount(string firstName, string lastName, string identityNumber, string accountTypeKey)
        {
            AccountType accountType = context.Query<AccountTypeRepository>().GetByKey(accountTypeKey);
            if (accountType == null)
            {
                // return null or wtf response
                return null;
            }

            string accountNumber = context.Query<AccountRepository>().GetNextAccountNumber();
            Account account = context.New<Account>()
                .With(firstName, lastName, identityNumber, accountType, accountNumber);
            return account;
        }

        public Account GetAccountByAccountNumber(string accountNumber)
        {
            return context.Query<AccountRepository>().GetAccountByAccountNumber(accountNumber);
        }

        public Account GetAccountById(int id)
        {
            return context.Query<AccountRepository>().GetAccountById(id);
        }

        public List<Account> GetAccountListByIdentityNumber(string identityNumber)
        {
            return context.Query<AccountRepository>().GetAccountListByIdentityNumber(identityNumber);
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

                var transactionStatus = context.Query<TransactionStatusRepository>()
                    .GetByKey(TransactionStatus.SUCCEED);
                var accountTransaction = context.New<AccountTransaction>()
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

                    var transactionStatus = context.Query<TransactionStatusRepository>()
                        .GetByKey(TransactionStatus.FAILED);
                    context.New<AccountTransaction>()
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

                var transactionStatus = context.Query<TransactionStatusRepository>().GetByKey(TransactionStatus.SUCCEED);
                context.New<AccountTransaction>()
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

                    var transactionStatus = context.Query<TransactionStatusRepository>().GetByKey(TransactionStatus.FAILED);
                    context.New<AccountTransaction>()
                        .With(-amount, account, transactionStatus);
                }
                return false;
            }
            

            return true;
        }

        public Account SetAccountOwnerName(string accountNumber, string firstName, string lastName)
        {
            Account account = GetAccountByAccountNumber(accountNumber);
            if (account == null)
                return null;

            return Update(account.SetAccountOwnerName(firstName, lastName));
        }

        #region Entity operations
        private Account Update(Account account)
        {
            return context.Query<AccountRepository>().Update(account);
        }
        #endregion
    }
}
