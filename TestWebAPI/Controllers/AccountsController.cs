using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestBussiness.Connection;
using TestBussiness.ServiceMessage;
using TestBussiness.Entity;
using TestBussiness.ManagerService;
using TestBussiness.RepositoryService;
using TestBussiness.ServiceMessage.Builders;
using TestBussiness.ServiceMessage.Responses;
using StructureMap;
using TestBussiness.ServiceMessage.Responses.Factories;

namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager accountManagerService;
        private readonly IControllerActionItemResponseFactory<AccountDto, Account> controllerActionItemResponseFactory;
        private readonly IControllerActionListResponseFactory<AccountDto, Account> controllerActionListResponseFactory;
        
        public AccountsController(
            IAccountManager accountManagerService,
            IControllerActionItemResponseFactory<AccountDto,Account> controllerActionItemResponseFactory,
            IControllerActionListResponseFactory<AccountDto, Account> controllerActionListResponseFactory)
        {
            this.accountManagerService = accountManagerService;
            this.controllerActionItemResponseFactory = controllerActionItemResponseFactory;
            this.controllerActionListResponseFactory = controllerActionListResponseFactory;
        }

        // GET: api/Accounts
        [HttpGet]
        public IControllerActionListResponse<AccountDto> Get()
        {
            List<Account> accounts = accountManagerService.GetAll();
            if (accounts.Count == 0)
            {
                return controllerActionListResponseFactory
                    .NotFound("no account found");
            }

            return controllerActionListResponseFactory
                .Success(accounts);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public IControllerActionItemResponse<AccountDto> Get(int id)
        {
            Account account = this.accountManagerService
                .GetAccountById(id);
            if (account == null)
            {
                return controllerActionItemResponseFactory
                    .NotFound(string.Format("{0} is not found with the given identifier", nameof(account)));
            }

            return controllerActionItemResponseFactory
                .Success(account);
        }

        [HttpGet("accountNumber/{accountNumber}")]
        public IControllerActionItemResponse<AccountDto> GetAccountByAccountNumber(string accountNumber)
        {
            Account account = this.accountManagerService
                .GetAccountByAccountNumber(accountNumber);
            if (account == null)
            {
                return controllerActionItemResponseFactory
                    .NotFound(string.Format("{0} is not found with the given identifier", nameof(account)));
            }

            return controllerActionItemResponseFactory
                .Success(account);
        }

        [HttpGet("identityNumber/{identityNumber}")]
        public IControllerActionListResponse<AccountDto> GetAccountsByIdentityNumber(string identityNumber)
        {
            List<Account> accounts = this.accountManagerService
                .GetAccountListByIdentityNumber(identityNumber);
            
            if (accounts.Count == 0)
            {
                return controllerActionListResponseFactory
                    .NotFound(string.Format("no account found with the given {0}: {1}", 
                        nameof(identityNumber), identityNumber));
            }

            return controllerActionListResponseFactory
                .Success(accounts);
        }

        // POST: api/Accounts
        [HttpPost]
        public IControllerActionItemResponse<AccountDto> Post([FromBody] AccountDto accountDto)
        {
            Account account = this.accountManagerService
                .CreateNewAccount(accountDto.FirstName, 
                accountDto.LastName, 
                accountDto.IdentityNumber, 
                accountDto.AccountTypeKey);

            if (account == null)
            {
                return controllerActionItemResponseFactory
                    .WithResponseCode("2")
                    .WithResponseMessage("account did not created.")
                    .BuildResponse();
            }

            return controllerActionItemResponseFactory
                    .Success(account);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPut("deposit")]
        public IControllerActionItemResponse<AccountDto> DepositAccount([FromBody] AccountBalanceInfoMessage message)
        {
            bool isOk = accountManagerService.Deposit(message.AccountNumber, message.Amount);
            if (!isOk)
            {
                return controllerActionItemResponseFactory
                    .WithResponseCode("2")
                    .WithResponseMessage("deposit operation failed")
                    .BuildResponse();
            }

            Account account = accountManagerService.GetAccountByAccountNumber(message.AccountNumber);

            return controllerActionItemResponseFactory
                .Success(account);
        }

        [HttpPut("withdraw")]
        public IControllerActionItemResponse<AccountDto> WithdrawAccount([FromBody] AccountBalanceInfoMessage message)
        {
            bool isOk = accountManagerService.Withdraw(message.AccountNumber, message.Amount);
            if (!isOk)
            {
                return controllerActionItemResponseFactory
                    .WithResponseCode("2")
                    .WithResponseMessage("withdraw operation failed")
                    .BuildResponse();
            }

            Account account = accountManagerService.GetAccountByAccountNumber(message.AccountNumber);

            return controllerActionItemResponseFactory
                .Success(account);
        }

        [HttpPut("ownerName")]
        public IControllerActionItemResponse<AccountDto> UpdateAccountOwnerName([FromBody] AccountDto accountInfo)
        {
            Account account = accountManagerService
                .SetAccountOwnerName(accountInfo.AccountNumber, accountInfo.FirstName, accountInfo.LastName);

            return controllerActionItemResponseFactory
                .Success(account);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
