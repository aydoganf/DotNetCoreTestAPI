using System;
using System.Collections.Generic;
using System.Text;

namespace TestBussiness.Entity
{
    public class TransactionStatus : IEntity
    {
        public virtual int Id { get; protected set; }
        public virtual string StatusKey { get; protected set; }
        public virtual string StatusDescription { get; protected set; }
        public virtual bool IsSucceeded { get; set; }
    }
}
