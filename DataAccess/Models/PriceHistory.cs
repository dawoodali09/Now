using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class PriceHistory
    {
        public long Id { get; set; }
        public int InstrumentId { get; set; }
        public DateTime RecordedOn { get; set; }
        public decimal Price { get; set; }

        public virtual Instrument Instrument { get; set; }
    }
}
