using NHibernate;

namespace TestBussiness.Connection
{
    public interface INHibernateConfigurator
    {
        ISession OpenSession();
    }
}