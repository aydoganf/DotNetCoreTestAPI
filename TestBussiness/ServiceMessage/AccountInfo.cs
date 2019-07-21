using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.ServiceMessage
{
    public class AccountInfo
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
        public AccountTypeInfo AccountType { get; set; }
    }

    public class AccountInfoBuilder
    {
        public static AccountInfo MapToDto(Account entity)
        {
            return new AccountInfo()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IdentityNumber = entity.IdentityNumber,
                AccountNumber = entity.AccountNumber,
                CreateDate = entity.CreateDate,
                Balance = entity.Balance,
                AccountTypeKey = entity.AccountType.TypeKey,
                AccountType = AccountTypeInfoBuilder.MapToInfoInstance(entity.AccountType)
            };
        }

        public static List<AccountInfo> MapToInfoList(IEnumerable<Account> entities)
        {
            List<AccountInfo> dtos = new List<AccountInfo>();
            foreach (var entity in entities)
            {
                dtos.Add(AccountInfoBuilder.MapToDto(entity));
            }
            return dtos;
        }
    }
}
