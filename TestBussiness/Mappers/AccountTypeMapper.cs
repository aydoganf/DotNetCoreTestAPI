using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.Mappers
{
    public class AccountTypeMapper : ClassMap<AccountType>
    {
        public AccountTypeMapper()
        {
            Table("accounttype");
            Id(o => o.Id).Column("AccountTypeId");
            Map(o => o.TypeKey).Column("TypeKey");
            Map(o => o.TypeName).Column("TypeName");
            //HasMany(o => o.Accounts).KeyColumn("AccountTypeId").Inverse().Cascade.All();
        }
    }
}
