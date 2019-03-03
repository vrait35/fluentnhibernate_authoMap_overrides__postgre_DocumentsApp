using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextLibrary.Entities
{
    public class Organization:IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
    }
}
