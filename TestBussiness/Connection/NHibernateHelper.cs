using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using StructureMap;

namespace TestBussiness.Connection
{
    public class NHibernateHelper : INHibernateHelper
    {
        private readonly string connectionString;
        private readonly object _lockObject = new object();
        private ISessionFactory _sessionFactory;
        private IContainer container;

        public NHibernateHelper(IConfiguration configuration, IContainer container)
        {
            connectionString = configuration["ConnectionString"];
            this.container = container;
        }

        private ISessionFactory sessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    CreateSessionFactoryAndConfigure();
                }
                return _sessionFactory;
            }
        }

        public ISession OpenSession()
        {
            //var sb = sessionFactory.WithOptions();
            //return sb.Interceptor(new DependencyInjectionEntityInterceptor(container)).OpenSession();
            //sessionFactory.OpenSession(new DependencyInjectionEntityInterceptor(container));
            return sessionFactory.OpenSession();
        }

        private void CreateSessionFactoryAndConfigure()
        {
            lock (_lockObject)
            {
                var fluentConfiguration = Fluently.Configure();
                fluentConfiguration.Database(
                    MySQLConfiguration.Standard.ConnectionString(connectionString))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Mappers.AccountMapper>())
                    //.ExposeConfiguration(config => 
                    //    config.SetInterceptor(new DependencyInjectionEntityInterceptor(container)))
                    .BuildConfiguration();
                
                _sessionFactory = fluentConfiguration.BuildSessionFactory();
            }
        }
    }
}
