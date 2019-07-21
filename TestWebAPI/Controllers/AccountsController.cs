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
using TestBussiness.ServiceMessage.Builders;

namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager accountManagerService;
        private readonly IDtoBuilder<AccountDto, Account> dtoBuilder;

        public AccountsController(IAccountManager accountManagerService, IDtoBuilder<AccountDto, Account> dtoBuilder)
        {
            this.accountManagerService = accountManagerService;
            this.dtoBuilder = dtoBuilder;
        }

        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<AccountDto> Get()
        {
            List<Account> accounts = accountManagerService.GetAll();
            if (accounts.Count == 0)
                return null;
            return dtoBuilder.MapToDtoList(accounts);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public AccountDto Get(int id)
        {
            Account account = this.accountManagerService
                .GetAccountById(id);
            if (account == null)
                return null;

            return dtoBuilder.MapToDto(account);
        }

        [HttpGet("accountNumber/{accountNumber}")]
        public AccountDto GetAccountByAccountNumber(string accountNumber)
        {
            Account account = this.accountManagerService
                .GetAccountByAccountNumber(accountNumber);
            if (account == null)
                return null;

            return dtoBuilder.MapToDto(account);
        }

        [HttpGet("identityNumber/{identityNumber}")]
        public IEnumerable<AccountDto> GetAccountsByIdentityNumber(string identityNumber)
        {
            List<Account> accounts = this.accountManagerService
                .GetAccountListByIdentityNumber(identityNumber);
            if (accounts.Count == 0)
                return null;

            return dtoBuilder.MapToDtoList(accounts);
        }

        // POST: api/Accounts
        [HttpPost]
        public AccountDto Post([FromBody] AccountDto accountDto)
        {
            Account account = this.accountManagerService
                .CreateNewAccount(accountDto.FirstName, 
                accountDto.LastName, 
                accountDto.IdentityNumber, 
                accountDto.AccountTypeKey);
            if (account == null)
                return null;

            return dtoBuilder.MapToDto(account);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPut("deposit")]
        public ActionResult<AccountDto> DepositAccount([FromBody] AccountBalanceInfoMessage message)
        {
            bool isOk = accountManagerService.Deposit(message.AccountNumber, message.Amount);
            if (!isOk)
                return BadRequest();

            Account account = accountManagerService.GetAccountByAccountNumber(message.AccountNumber);
            return dtoBuilder.MapToDto(account);

        }

        [HttpPut("withdraw")]
        public AccountDto WithdrawAccount([FromBody] AccountBalanceInfoMessage message)
        {
            bool isOk = accountManagerService.Withdraw(message.AccountNumber, message.Amount);
            if (!isOk)
                throw new Exception("not ok");

            Account account = accountManagerService.GetAccountByAccountNumber(message.AccountNumber);
            return dtoBuilder.MapToDto(account);
        }

        [HttpPut("ownerName")]
        public AccountDto UpdateAccountOwnerName([FromBody] AccountDto accountInfo)
        {
            Account account = accountManagerService
                .SetAccountOwnerName(accountInfo.AccountNumber, accountInfo.FirstName, accountInfo.LastName);

            return dtoBuilder.MapToDto(account);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
