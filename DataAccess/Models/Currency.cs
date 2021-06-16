using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isocode { get; set; }
        public int Isonumber { get; set; }
        public decimal ValueInNzd { get; set; }
    }
}
