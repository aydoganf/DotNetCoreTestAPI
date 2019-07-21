using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TestBussiness.Entity;

namespace TestBussiness.ServiceMessage.Builders
{
    public class AccountDto : IDto
    {
        [IgnoreDataMember]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string AccountNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Balance { get; set; }
        public string AccountTypeKey { get; set; }
        public AccountTypeDto AccountType { get; set; }
    }

    public class AccountDtoBuilder : IDtoBuilder<AccountDto, Account>
    {
        private readonly IDtoBuilder<AccountTypeDto, AccountType> accountTypeDtoBuilder;

        public AccountDtoBuilder(IDtoBuilder<AccountTypeDto, AccountType> accountTypeDtoBuilder)
        {
            this.accountTypeDtoBuilder = accountTypeDtoBuilder;
        }

        public AccountDto MapToDto(Account entity)
        {
            return new AccountDto()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IdentityNumber = entity.IdentityNumber,
                AccountNumber = entity.AccountNumber,
                CreateDate = entity.CreateDate,
                Balance = entity.Balance,
                AccountTypeKey = entity.AccountType.TypeKey,
                AccountType = accountTypeDtoBuilder.MapToDto(entity.AccountType)
            };
        }

        public IEnumerable<AccountDto> MapToDtoList(IEnumerable<Account> entities)
        {
            List<AccountDto> dtos = new List<AccountDto>();
            foreach (var entity in entities)
            {
                dtos.Add(MapToDto(entity));
            }
            return dtos;
        }
    }
}
