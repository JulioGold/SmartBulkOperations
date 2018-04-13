using System;

namespace SBO.Domain.Entities
{
    public partial class Contact
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }

        public Nullable<Boolean> IsPrincipal { get; set; }

        public DateTime InsertedDate { get; set; }

        public Nullable<DateTime> UpdateDate { get; set; }

        public virtual Person Person { get; set; }
    }
}
