using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Market
    {
        public Market()
        {
            Instruments = new HashSet<Instrument>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsOpen { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Instrument> Instruments { get; set; }
    }
}
