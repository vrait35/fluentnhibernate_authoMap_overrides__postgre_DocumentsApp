using DbContextLibrary.PostgreSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DbContextLibrary.Entities
{
    public class FileInfo:IEntity
    {        
        public virtual long Id { get; set; }
        public virtual string ExternalId { get; set; }
        public virtual string FileName { get; set; }     
        public virtual Document Document { get; set; }
        public virtual FileTypeEnum FileType { get; set; }
    }
}
