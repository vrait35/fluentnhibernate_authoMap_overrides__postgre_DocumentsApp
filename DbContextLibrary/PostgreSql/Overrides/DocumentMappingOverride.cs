using DbContextLibrary.Entities;
using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;

namespace DbContextLibrary.PostgreSql.Overrides
{
    public class DocumentMappingOverride : IAutoMappingOverride<Document>
    {
        public void Override(AutoMapping<Document> mapping)
        {
            mapping.References(x => x.PersonTo).Not.Nullable();           
            mapping.References(x => x.PersonFrom).Not.Nullable();         
            mapping.Map(x => x.DateTimeCreateDocument).Not.Nullable();
            mapping.Map(x => x.DateTimeRegistration).Not.Nullable();
            mapping.Map(x => x.Name).Not.Nullable();
            mapping.HasMany(x => x.FilesInfo).Inverse();            
        }
    }
}
