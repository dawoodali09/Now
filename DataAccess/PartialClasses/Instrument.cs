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
			return result;
		}

	}
}
