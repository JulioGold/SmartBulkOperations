using System;
using System.Collections.Generic;

namespace SBO.Domain.Entities
{
    public partial class Person
    {
        public Person()
        {
            Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int UserCode { get; set; }

        public Nullable<int> Age { get; set; }

        public Nullable<DateTime> BornDate { get; set; }

        public Nullable<DateTime> UpdateDate { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
