using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbContextLibrary.Entities;

namespace DbContextLibrary.PostgreSql.Convention
{
    public class FluentConvention : IReferenceConvention
    {
       // public void Apply(IPropertyInstance instance)
       // {            
       //     instance.Not.Nullable();          
       // }

        public void Apply(IManyToOneInstance instance)
        {
            instance.Not.Nullable();
        }
    }
}
