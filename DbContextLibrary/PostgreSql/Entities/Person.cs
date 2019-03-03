using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextLibrary.Entities
{
    public class Person:IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Account Account { get; set; }
    }
}
