using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbContextLibrary.PostgreSql;

namespace DbContextLibrary.Entities
{
    public class Document:IEntity
    {
        public virtual long Id { get; set; }
        public virtual Person PersonFrom { get; set; }
        public virtual Person PersonTo { get; set; }
        public virtual DateTime DateTimeRegistration { get; set; }
        public virtual DateTime DateTimeCreateDocument { get; set; }
        public virtual long Number { get; set; }
        public virtual string Name { get; set; }
        public virtual bool Status { get; set; }        
        //public virtual FileTypeEnum DocumentType { get; set; }
        public virtual ISet<FileInfo> FilesInfo { get; set; }
        public Document()
        {
            FilesInfo = new HashSet<FileInfo>();
        }
    }
}
