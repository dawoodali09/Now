using System;
using System.Collections.Generic;

#nullable disable

namespace SQLDataAccess.Models
{
    public partial class Category
    {
        public Category()
        {
            InstrumentCategories = new HashSet<InstrumentCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<InstrumentCategory> InstrumentCategories { get; set; }
    }
}
