using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;

namespace TestBussiness.Context
{
    public class Context : IContext
    {
        //private INHibernateHelper nhibernateHelper;
        //private IAccountRepository accountRepository;

        public Context()
        {
            //this.nhibernateHelper = nHibernateHelper;
            //this.accountRepository = accountRepository;
        }

        public T New<T>()
        {
            //if(typeof(Account) is T)
            //{
            //    return (T)Activator.CreateInstance(typeof(T), accountRepository);
            //}

            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
