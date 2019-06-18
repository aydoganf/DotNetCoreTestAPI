using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.Mappers
{
    public class AccountMapper : ClassMap<Account>
    {
        public AccountMapper()
        {
            Table("account");
            Id(o => o.Id).Column("AccountId");
            Map(o => o.AccountNumber).Column("AccountNumber");
            Map(o => o.Balance).Column("Balance");
            Map(o => o.CreateDate).Column("CreateDate");
            Map(o => o.FirstName).Column("FirstName");
            Map(o => o.IdentityNumber).Column("IdentityNumber");
            Map(o => o.LastName).Column("LastName");
            References(o => o.AccountType, "AccountTypeId")
                .ForeignKey("fk_account_accounttype");
        }
    }
}
