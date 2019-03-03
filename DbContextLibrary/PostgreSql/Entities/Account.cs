using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextLibrary.Entities
{
    public class Account:IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        public virtual ISet<Person> Persons { get; set; }
        public Account()
        {
            Persons = new HashSet<Person>();
        }
    }
}
