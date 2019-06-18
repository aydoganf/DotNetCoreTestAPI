using System;
using System.Collections.Generic;
using System.Text;

namespace TestBussiness.Entity
{
    public class AccountTransaction : IEntity
    {
        public virtual int Id { get; protected set; }
        public virtual decimal TransactionAmount { get; protected set; }
        public virtual DateTime CreateDate { get; protected set; }
        public virtual Account Account { get; protected set; }
        public virtual TransactionStatus TransactionStatus { get; protected set; }
    }
}
