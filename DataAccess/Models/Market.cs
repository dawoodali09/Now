using System;
using System.Collections.Generic;

#nullable disable

namespace SQLDataAccess.Models
{
    public partial class Market
    {
        public Market()
        {
            ClosingDays = new HashSet<ClosingDay>();
            Instruments = new HashSet<Instrument>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsOpen { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public TimeSpan OpeningTimeNz { get; set; }
        public TimeSpan ClosingTimeNz { get; set; }

        public virtual ICollection<ClosingDay> ClosingDays { get; set; }
        public virtual ICollection<Instrument> Instruments { get; set; }
    }
}
