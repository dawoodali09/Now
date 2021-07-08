using System;
using System.Collections.Generic;

#nullable disable

namespace SQLDataAccess.Models
{
    public partial class ClosingDay
    {
        public int Id { get; set; }
        public int MarketId { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Reason { get; set; }

        public virtual Market Market { get; set; }
    }
}
