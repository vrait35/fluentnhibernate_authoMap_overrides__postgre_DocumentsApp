using DbContextLibrary.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextLibrary.PostgreSql.Overrides
{

    public class AccountMappingOverride : IAutoMappingOverride<Account>
    {
        public void Override(AutoMapping<Account> mapping)
        {
            mapping.Map(x => x.Login).Not.Nullable();
            mapping.Map(x => x.Password).Not.Nullable();
            mapping.HasMany(x => x.Persons).Inverse();                 
        }
    }
}
