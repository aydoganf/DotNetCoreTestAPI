using NHibernate;

namespace TestBussiness.Connection
{
    public interface INHibernateHelper
    {
        ISession OpenSession();
    }
}