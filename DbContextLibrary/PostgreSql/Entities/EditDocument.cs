using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextLibrary.Entities
{
    public class EditDocument:IEntity
    {
        public virtual long Id { get; set; }
        public virtual DateTime DateTimeEdit { get; set; }
        public virtual string Action { get; set; }
        public virtual Person Author { get; set; }
        public virtual Document Document { get; set; }       
    }
}
