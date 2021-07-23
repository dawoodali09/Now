using System;
using System.Collections.Generic;
using System.Text;
using Common.Models;
using System.Linq;

namespace SQLDataAccess.Models
{
	public partial class Instrument {

		public Common.Models.Instrument ToCommonInstrument(){
			Common.Models.Instrument result = new Common.Models.Instrument() {
				AnnualisedReturnPercent = this.AnnualisedReturnPercent.ToString(),
				Categories = this.InstrumentCategories.Select(S => S.Category.Name).ToList(),
				Ceo = this.Ceo,
				Description = this.Description,
				Employees = this.Employee.HasValue ? this.Employee.Value : 0,
				Exchange = this.Market.Code,
				ExchangeCountry = this.Market.Country,
				GrossDividendYieldPercent = this.GrossDividendYieldPercent.HasValue ? this.GrossDividendYieldPercent.Value.ToString() : null,
				Id = this.Id.ToString(),
				InstrumentType = this.InstrumentType,
				IsVolatile = this.IssVolatile.HasValue ? this.IssVolatile.Value : false,
				KidsRecommended = this.KidsRecommended.HasValue ? this.KidsRecommended.Value : false,
				MarketCap = this.MarketCap,
				MarketLastCheck = this.UpdatedOn,
				MarketPrice = this.MarketPrice,
				Name = this.Name,
				PeRatio = this.Peratio.HasValue ? this.Peratio.Value.ToString() : null,
				RiskRating = this.RiskRating.HasValue ? this.RiskRating.Value : 0,
				Symbol = this.Symbol,
				WebsiteUrl = this.WebsiteUrl
			};
			result.ComparisonPrices = new comparisonPrices()
			{
				FiveYear = new comparisonValues()
				{
					Max = this.FiveYearMaximum.ToString(),
					Min = this.FiveYearMinimum.ToString(),
					Percent = this.FiveYearPercentage.ToString(),
					Value = this.FiveYearValue.ToString()
                 },
				OneDay = new comparisonValues()
                {
					Max = this.OneDayMaximum.ToString(),
					Min = this.OneDayMinimum.ToString(),
					Percent = this.OneDayPercentage.ToString(),
					Value = this.OneDayValue.ToString()
				},
				OneMonth = new comparisonValues()
                {
					Max = this.OneMonthMaximum.ToString(),
					Min = this.OneMonthMinimum.ToString(),
					Percent = this.OneMonthPercentage.ToString(),
					Value = this.OneMonthValue.ToString()
						
                },
				OneWeek= new comparisonValues()
                {
					Max = this.OneWeekMaximum.ToString(),
				Min = this.OneWeekMinimum.ToString(),	
				Percent = this.OneWeekPercentage.ToString(),
				Value = this.OneWeekValue.ToString()
                },
				OneYear = new comparisonValues()
                {
					Max = this.OneYearMaximum.ToString(),
					Min = this.OneYearMinimum.ToString(),
					Percent = this.OneYearPercentage.ToString(),
					Value = this.OneYearValue.ToString()
                },
				SixMonth = new comparisonValues()
                {
					 Max = this.SixMonthMaximum.ToString(),
					 Min = this.SixMonthMinimum.ToString(),
					 Percent = this.SixMonthPercentage.ToString(),
					 Value = this.SixMonthValue.ToString()
                },
				ThreeMonth = new comparisonValues()
                {
					Max = this.ThreeMonthMaximum.ToString(),
					Min = this.ThreeMonthMinimum.ToString(),
					Percent = this.ThreeMonthPercentage.ToString(),
					Value = this.ThreeMonthValue.ToString()
                }
			};
			return result;
		}

	}
}
