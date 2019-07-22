using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.RepositoryService;

namespace TestBussiness.Context
{
    public interface IContext
    {
        ISession Session { get; }
        T New<T>() where T : class;
        T Query<T>() where T : IRepositoryQuery;
    }
}
