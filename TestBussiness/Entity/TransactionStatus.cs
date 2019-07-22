using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.RepositoryService;

namespace TestBussiness.Entity
{
    public class TransactionStatus : IEntity
    {
        #region IoC
        private readonly IRepository<TransactionStatus> repository;

        protected TransactionStatus()
        {
        }

        public TransactionStatus(IRepository<TransactionStatus> repository)
        {
            this.repository = repository;
        }
        #endregion

        public virtual int Id { get; protected set; }
        public virtual string StatusKey { get; protected set; }
        public virtual string StatusDescription { get; protected set; }
        public virtual bool IsSucceeded { get; set; }

        public static readonly string SUCCEED = "succeed";
        public static readonly string FAILED = "failed";
    }
}
