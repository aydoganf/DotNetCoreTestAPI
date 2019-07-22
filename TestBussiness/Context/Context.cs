using NHibernate;
using StructureMap;
using StructureMap.Pipeline;
using System.Linq;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;

namespace TestBussiness.Context
{
    public class Context : IContext
    {
        private readonly IContainer container;
        private readonly INHibernateConfigurator nhibernateConfigurator;

        private ISession _session;
        private ISession session
        {
            get
            {
                if (_session == null)
                {
                    _session = nhibernateConfigurator.OpenSession();
                }
                return _session;
            }
        }

        public ISession Session => session;

        public Context(IContainer container, INHibernateConfigurator nhibernateConfigurator)
        {
            this.container = container;
            this.nhibernateConfigurator = nhibernateConfigurator;
        }

        public T New<T>() where T : class
        {
            return container.GetInstance<T>();
        }

        public T Query<T>() where T : IRepositoryQuery
        {
            return container.GetInstance<T>();
        }
    }
}
