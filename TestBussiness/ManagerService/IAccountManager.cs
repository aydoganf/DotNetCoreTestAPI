using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.ServiceMessage;
using TestBussiness.Entity;

namespace TestBussiness.ManagerService
{
    public interface IAccountManager
    {
        List<Account> GetAll();
        Account GetAccountById(int id);
        Account GetAccountByAccountNumber(string accountNumber);
        List<Account> GetAccountListByIdentityNumber(string identityNumber);
        Account CreateNewAccount(string firstName, string lastName, string identityNumber, string accountTypeKey);
        bool Deposit(string accountNumber, decimal amount);
        bool Withdraw(string accountNumber, decimal amount);
        Account SetAccountOwnerName(string accountNumber, string firstName, string lastName);
    }
}
