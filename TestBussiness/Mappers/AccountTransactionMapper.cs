using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.Mappers
{
    public class AccountTransactionMapper : ClassMap<AccountTransaction>
    {
        public AccountTransactionMapper()
        {
            Table("accounttransaction");
            Id(o => o.Id).Column("AccountTransactionId");
            Map(o => o.CreateDate).Column("CreateDate");
            Map(o => o.TransactionAmount).Column("TransactionAmount");
            References(o => o.Account, "AccountId")
                .ForeignKey("fk_accounttransaction_account");
            References(o => o.TransactionStatus, "TransactionStatusId")
                .ForeignKey("fk_accounttransaction_transactionstatus");
        }
    }
}
