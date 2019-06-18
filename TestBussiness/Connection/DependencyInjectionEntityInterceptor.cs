using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestBussiness.Context;
using TestBussiness.Entity;

namespace TestBussiness.Connection
{
    public class AccountEntityInterceptor : EmptyInterceptor
    {
        IContainer _container;
        ISession _session;

        public AccountEntityInterceptor(IContainer container)
        {
            _container = container;
        }

        public override void SetSession(ISession session)
        {
            base.SetSession(session);
            _session = session;
        }

        public override object Instantiate(string clazz, object id)
        {
            var type = Assembly.GetAssembly(typeof(Account)).GetTypes().FirstOrDefault(x => x.FullName == clazz);
            var hasParameter = type.GetConstructors().Any(x => x.GetParameters().Any());
            if (type != null && hasParameter)
            {
                var instance = _container.GetInstance(type);

                var metaData = _session.SessionFactory.GetClassMetadata(clazz);
                metaData.SetIdentifier(instance, id);
                return instance;
            }

            return base.Instantiate(clazz, id);
        }
    }
}
