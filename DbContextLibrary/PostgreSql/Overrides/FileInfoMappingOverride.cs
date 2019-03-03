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
    public class FileInfoMappingOverride : IAutoMappingOverride<FileInfo>
    {
        public void Override(AutoMapping<FileInfo> mapping)
        {
            mapping.Map(x => x.ExternalId).Not.Nullable();
            mapping.Map(x => x.FileName).Not.Nullable();
            mapping.References(x => x.Document).Not.Nullable();
            mapping.Map(x => x.FileType).CustomType<FileTypeEnum>();            
        }
    }
}
