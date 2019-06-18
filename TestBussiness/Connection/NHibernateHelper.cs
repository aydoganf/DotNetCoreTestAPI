using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using TestBussiness.Context;

namespace TestBussiness.Connection
{
    public class NHibernateHelper : INHibernateHelper
    {
        private readonly string _connectionString;
        private readonly object _lockObject = new object();
        private ISessionFactory _sessionFactory;
        private IContainer _container;

        public NHibernateHelper(IConfiguration configuration, IContainer container)
        {
            _connectionString = configuration["ConnectionString"];
            _container = container;
        }

        private ISessionFactory SessionFactory
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
            return SessionFactory.OpenSession();
        }

        private void CreateSessionFactoryAndConfigure()
        {
            lock (_lockObject)
            {
                var fluentConfiguration = Fluently.Configure();
                fluentConfiguration.Database(
                    MySQLConfiguration.Standard.ConnectionString(_connectionString))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Mappers.PostMap>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Mappers.PostDetailMap>())
                    .ExposeConfiguration(c => c.SetInterceptor(new AccountEntityInterceptor(_container)))
                    .BuildConfiguration();
                // mappings coming here bruh!
                
                _sessionFactory = fluentConfiguration.BuildSessionFactory();
            }
        }
    }
}
