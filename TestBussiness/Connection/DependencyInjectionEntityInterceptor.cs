using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestBussiness.Entity;
using StructureMap;

namespace TestBussiness.Connection
{
    public class DependencyInjectionEntityInterceptor : EmptyInterceptor
    {
        IContainer container;
        ISession session;

        public DependencyInjectionEntityInterceptor(IContainer container)
        {
            this.container = container;
        }

        public override void SetSession(ISession session)
        {
            //throw new Exception("Set session is run");
            this.session = session ?? throw new Exception("");
            base.SetSession(session);            
        }



        public override object Instantiate(string clazz, object id)
        {
            var type = Assembly.GetAssembly(typeof(Account)).GetTypes().FirstOrDefault(x => x.FullName == clazz);
            var hasParameter = type.GetConstructors().Any(x => x.GetParameters().Any());
            if (type != null && hasParameter)
            {
                var instance = container.GetInstance(type);

                if (session == null)
                {
                    throw new Exception("ISession is null");
                }

                if (session.SessionFactory == null)
                {
                    throw new Exception("SessionFactory is null");
                }
                var metaData = session.SessionFactory.GetClassMetadata(clazz);
                metaData.SetIdentifier(instance, id);
                return instance;
            }

            return base.Instantiate(clazz, id);
        }
    }
}
