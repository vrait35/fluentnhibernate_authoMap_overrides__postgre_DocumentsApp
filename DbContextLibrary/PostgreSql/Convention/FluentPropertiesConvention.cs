using DbContextLibrary.Entities;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextLibrary.PostgreSql.Convention
{    
    public class FluentPropertiesConvention : IPropertyConvention,IPropertyConventionAcceptance
    {
       // public void Accept(IAcceptanceCriteria<IClassInspector> criteria)
       // {
       //     criteria.Expect(x => x.EntityType!=typeof(Person));
       // }       

        public void Apply(IPropertyInstance instance)
        {
            instance.Not.Nullable();
           // instance.Nullable();
        }

        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.GetType() == typeof(Person));
        }

    }
}
