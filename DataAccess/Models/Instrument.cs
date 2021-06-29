using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Instrument
    {
        public Instrument()
        {
            PriceHistories = new HashSet<PriceHistory>();
        }

        public int Id { get; set; }
        public Guid Shid { get; set; }
        public string InstrumentType { get; set; }
        public string Symbol { get; set; }
        public bool? KidsRecommended { get; set; }
        public bool? IssVolatile { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Categories { get; set; }
        public int? RiskRating { get; set; }
        public decimal MarketPrice { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int MarketId { get; set; }
        public decimal? Peratio { get; set; }
        public long MarketCap { get; set; }
        public string WebsiteUrl { get; set; }
        public float? GrossDividendYieldPercent { get; set; }
        public float? AnnualisedReturnPercent { get; set; }
        public string Ceo { get; set; }
        public int? Employee { get; set; }

        public virtual Market Market { get; set; }
        public virtual ICollection<PriceHistory> PriceHistories { get; set; }
    }
}
