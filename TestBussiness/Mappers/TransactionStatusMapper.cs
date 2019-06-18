using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.Mappers
{
    public class TransactionStatusMapper : ClassMap<TransactionStatus>
    {
        public TransactionStatusMapper()
        {
            Table("transactionstatus");
            Id(o => o.Id).Column("TransactionStatusId");
            Map(o => o.IsSucceeded).Column("IsSucceeded");
            Map(o => o.StatusDescription).Column("StatusDescription");
            Map(o => o.StatusKey).Column("StatusKey");
        }
    }
}
