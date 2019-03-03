using DbContextLibrary.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using DbContextLibrary.PostgreSql.Overrides;
using DbContextLibrary.PostgreSql.Convention;
using FluentNHibernate.Conventions.Helpers.Builders;

namespace DbContextLibrary.PostgreSql
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory = null;       
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }

        public static NHibernate.ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        private static void InitializeSessionFactory()
        {
            try
            {
                _sessionFactory = Fluently.Configure()
      .Database(PostgreSQLConfiguration.PostgreSQL82.ConnectionString("User ID=postgres;Password=123;Host=localhost;Port=5432;Database=DocumentsDb1;"))
      .Mappings(m =>
        m.AutoMappings
          .Add(AutoMap.AssemblyOf<IEntity>()
          .Where(t => t.GetInterfaces().Any(x => x == typeof(IEntity)))
          .IgnoreBase<IEntity>()//.UseOverridesFromAssemblyOf<DocumentMappingOverride>()          
          .UseOverridesFromAssemblyOf<AccountMappingOverride>()        
          )) 
          .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))  // new SchemaUpdate(cfg) new SchemaExport(cfg).Create(true, true)
             .BuildSessionFactory();
            }
            catch (Exception e)
            {
               
            }
        }

    }
}
