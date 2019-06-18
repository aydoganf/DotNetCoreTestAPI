using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestBussiness.Connection;
using TestBussiness.Context;
using TestBussiness.ServiceMessage;
using TestBussiness.Entity;
using TestBussiness.ManagerService;
using TestBussiness.RepositoryService;

namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountManagerService accountManagerService;
        private IAccountRepository accountRepository;
        private IAccountTypeRepository accountTypeRepository;

        public AccountsController(
            IAccountManagerService accountManagerService,
            IAccountRepository accountRepository,
            IAccountTypeRepository accountTypeRepository
            )
        {
            this.accountManagerService = accountManagerService;
            this.accountRepository = accountRepository;
            this.accountTypeRepository = accountTypeRepository;
        }

        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<AccountInfo> Get()
        {
            List<Account> accounts = accountManagerService.GetAll();
            if (accounts.Count == 0)
                return null;
            return AccountInfoBuilder.MapToInfoList(accounts);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public AccountInfo Get(int id)
        {
            Account account = this.accountManagerService
                .GetAccountById(id);
            if (account == null)
                return null;
            return AccountInfoBuilder.MapToInfoInstance(account);
        }

        [HttpGet("accountNumber/{accountNumber}")]
        public AccountInfo GetAccountByAccountNumber(string accountNumber)
        {
            Account account = this.accountManagerService
                .GetAccountByAccountNumber(accountNumber);
            if (account == null)
                return null;

            return AccountInfoBuilder.MapToInfoInstance(account);
        }

        [HttpGet("identityNumber/{identityNumber}")]
        public IEnumerable<AccountInfo> GetAccountsByIdentityNumber(string identityNumber)
        {
            List<Account> accounts = this.accountManagerService
                .GetAccountListByIdentityNumber(identityNumber);
            if (accounts.Count == 0)
                return null;
            return AccountInfoBuilder.MapToInfoList(accounts);
        }

        // POST: api/Accounts
        [HttpPost]
        public AccountInfo Post([FromBody] AccountInfo accountDto)
        {
            Account account = this.accountManagerService
                .CreateNewAccount(accountDto.FirstName, 
                accountDto.LastName, 
                accountDto.IdentityNumber, 
                accountDto.AccountTypeKey);
            if (account == null)
                return null;
            return AccountInfoBuilder.MapToInfoInstance(account);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPut("deposit")]
        public AccountInfo DepositAccount([FromBody] AccountBalanceInfoMessage message)
        {
            bool isOk = accountManagerService.Deposit(message.AccountNumber, message.Amount);
            if (!isOk)
                throw new Exception("naaptın aga");

            Account account = accountManagerService.GetAccountByAccountNumber(message.AccountNumber);
            return AccountInfoBuilder.MapToInfoInstance(account);

        }

        [HttpPut("ownerName")]
        public AccountInfo UpdateAccountOwnerName([FromBody] AccountInfo accountInfo)
        {
            Account account = accountManagerService
                .SetAccountOwnerName(accountInfo.AccountNumber, accountInfo.FirstName, accountInfo.LastName);

            return AccountInfoBuilder.MapToInfoInstance(account);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
