using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Models {
	public class InstrumentDataResponse {
		public int Total { get; set; }
		public int CurrentPage { get; set; }
		public int ResultsPerPage { get; set; }
		public int NumberOfPages { get; set; }
		public List<Instrument> Instruments { get; set; }
	}

	public class Logos {
		public string Wide { get; set; }
		public string Thumb { get; set; }
		public string Micro { get; set; }
	}

	public class comparisonValues
	{
		public string Value { get; set; }
		public string Percent { get; set; }
		public string Max { get; set; }
		public string Min { get; set; }
	}

	

	public class comparisonPrices {

		[JsonProperty(PropertyName = "1d")]
		public comparisonValues OneDay { get; set; }

		[JsonProperty(PropertyName = "1w")]
		public comparisonValues OneWeek { get; set; }

		[JsonProperty(PropertyName = "1m")]
		public comparisonValues OneMonth { get; set; }

		[JsonProperty(PropertyName = "3m")]
		public comparisonValues ThreeMonth { get; set; }

		[JsonProperty(PropertyName = "6m")]
		public comparisonValues SixMonth { get; set; }

		[JsonProperty(PropertyName = "1y")]
		public comparisonValues OneYear { get; set; }

		[JsonProperty(PropertyName = "5y")]
		public comparisonValues FiveYear { get; set; }
	}

	public class Instrument {
		public string Id { get; set; }
		public string UrlSlug { get; set; }
		public string InstrumentType { get; set; }
		public string Symbol { get; set; }
		public bool KidsRecommended { get; set; }
		public bool IsVolatile { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<string> Categories { get; set; }
		public string LogoIdentifier { get; set; }
		public Logos Logos { get; set; }
		public int RiskRating { get; set; }
		public comparisonPrices ComparisonPrices { get; set; }
		public decimal MarketPrice { get; set; }
		public DateTime MarketLastCheck { get; set; }
		public string TradingStatus { get; set; }
		public string ExchangeCountry { get; set; }
		public string PeRatio { get; set; }
		public long MarketCap { get; set; }
		public string WebsiteUrl { get; set; }
		public string Exchange { get; set; }
		public object LegacyImageUrl { get; set; }
		public string DominantColour { get; set; }
		public object PdsDriveId { get; set; }
		public object AssetManager { get; set; }
		public object FixedFeeSpread { get; set; }
		public object ManagementFeePercent { get; set; }
		public string GrossDividendYieldPercent { get; set; }
		public string AnnualisedReturnPercent { get; set; }
		public string Ceo { get; set; }
		public int Employees { get; set; }
	}
}
