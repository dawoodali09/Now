using System;
using System.Collections.Generic;

#nullable disable

namespace SQLDataAccess.Models
{
    public partial class Instrument
    {
        public Instrument()
        {
            InstrumentCategories = new HashSet<InstrumentCategory>();
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
        public decimal FiveYearValue { get; set; }
        public decimal FiveYearPercentage { get; set; }
        public decimal FiveYearMinimum { get; set; }
        public decimal FiveYearMaximum { get; set; }
        public decimal OneYearValue { get; set; }
        public decimal OneYearPercentage { get; set; }
        public decimal OneYearMinimum { get; set; }
        public decimal OneYearMaximum { get; set; }
        public decimal SixMonthValue { get; set; }
        public decimal SixMonthPercentage { get; set; }
        public decimal SixMonthMinimum { get; set; }
        public decimal SixMonthMaximum { get; set; }
        public decimal ThreeMonthValue { get; set; }
        public decimal ThreeMonthPercentage { get; set; }
        public decimal ThreeMonthMinimum { get; set; }
        public decimal ThreeMonthMaximum { get; set; }
        public decimal OneMonthValue { get; set; }
        public decimal OneMonthPercentage { get; set; }
        public decimal OneMonthMinimum { get; set; }
        public decimal OneMonthMaximum { get; set; }
        public decimal OneWeekValue { get; set; }
        public decimal OneWeekPercentage { get; set; }
        public decimal OneWeekMinimum { get; set; }
        public decimal OneWeekMaximum { get; set; }
        public decimal OneDayValue { get; set; }
        public decimal OneDayPercentage { get; set; }
        public decimal OneDayMinimum { get; set; }
        public decimal OneDayMaximum { get; set; }

        public virtual Market Market { get; set; }
        public virtual ICollection<InstrumentCategory> InstrumentCategories { get; set; }
        public virtual ICollection<PriceHistory> PriceHistories { get; set; }
    }
}
