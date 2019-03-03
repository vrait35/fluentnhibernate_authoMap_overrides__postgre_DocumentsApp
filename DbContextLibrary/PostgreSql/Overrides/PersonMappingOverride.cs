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
    public class PersonMappingOverride: IAutoMappingOverride<Person>
    {
        public void Override(AutoMapping<Person> mapping)
        {
            mapping.Map(x => x.Name).Nullable();
            mapping.References(x => x.Organization).Nullable();
            mapping.References(x => x.Account).Not.Nullable();
        }
    }
}
