using System;
using System.Collections.Generic;

#nullable disable

namespace SQLDataAccess.Models
{
    public partial class InstrumentCategory
    {
        public int Id { get; set; }
        public int InstrumentId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Instrument Instrument { get; set; }
    }
}
