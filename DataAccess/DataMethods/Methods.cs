﻿using Common.Models;
using System.Collections.Generic;
using System.Text;
using SQLDataAccess.Models;
using System;
using System.Linq;


namespace SQLDataAccess.DataMethods {
	public static class Methods {
		public static void AddUpdateInstrument(Common.Models.Instrument ins,string connection) {
			SQLDataAccess.Models.NowDBContext con = new SQLDataAccess.Models.NowDBContext(connection);
			if (con.Database.CanConnect()) {
				//store the market
				Market mkt = new Market();
				if (!con.Markets.Any(x => x.Code == ins.Exchange)) {
					mkt.Code = ins.Exchange;
					mkt.Country = ins.ExchangeCountry;
					mkt.Currency = "USD";
					mkt.IsOpen = false;
					mkt.Name = ins.Exchange;
					con.Markets.Add(mkt);
					con.SaveChanges();
				} else {
					mkt = con.Markets.FirstOrDefault(x => x.Code == ins.Exchange);
				}

				// store the instrument 
				SQLDataAccess.Models.Instrument dbIns = new SQLDataAccess.Models.Instrument();
				if (!con.Instruments.Any(x => x.Shid == Guid.Parse(ins.Id))) {
					dbIns = new SQLDataAccess.Models.Instrument() {
						AnnualisedReturnPercent = (string.IsNullOrEmpty(ins.AnnualisedReturnPercent) ? 0 : float.Parse(ins.AnnualisedReturnPercent)),
						Categories = String.Join(",", ins.Categories.ToArray<string>()), // ins.Categories,
						Ceo = ins.Ceo,
						Description = ins.Description,
						Employee = ins.Employees,
						GrossDividendYieldPercent = (string.IsNullOrEmpty(ins.GrossDividendYieldPercent) ? 0 : float.Parse(ins.GrossDividendYieldPercent)),
						InstrumentType = ins.InstrumentType,
						IssVolatile = ins.IsVolatile,
						KidsRecommended = ins.KidsRecommended,
						MarketCap = ins.MarketCap,
						MarketId = con.Markets.Where(s => s.Code == ins.Exchange).FirstOrDefault().Id,
						MarketPrice = ins.MarketPrice,// string.IsNullOrEmpty(ins.MarketPrice) ? 0.00M : decimal.Parse(ins.MarketPrice),
						Name = ins.Name,
						Peratio = string.IsNullOrEmpty(ins.PeRatio) ? 0.00M : decimal.Parse(ins.PeRatio),
						RiskRating = ins.RiskRating,
						Shid = Guid.Parse(ins.Id),
						Symbol = ins.Symbol,
						UpdatedOn = ins.MarketLastCheck,
						WebsiteUrl = ins.WebsiteUrl
					};					
					mkt.IsOpen = (ins.TradingStatus == "active");
					SetComparativePrices(ref dbIns, ins);
					con.Instruments.Add(dbIns);
				} else {
					dbIns = con.Instruments.FirstOrDefault(x => x.Shid == Guid.Parse(ins.Id));
					dbIns.AnnualisedReturnPercent = (string.IsNullOrEmpty(ins.AnnualisedReturnPercent) ? 0 : float.Parse(ins.AnnualisedReturnPercent));
					dbIns.Ceo = ins.Ceo;
					dbIns.Employee = ins.Employees;
					dbIns.GrossDividendYieldPercent = string.IsNullOrEmpty(ins.GrossDividendYieldPercent) ? 0 : float.Parse(ins.GrossDividendYieldPercent);
					dbIns.InstrumentType = ins.InstrumentType;
					dbIns.IssVolatile = ins.IsVolatile;
					dbIns.KidsRecommended = ins.KidsRecommended;
					dbIns.MarketCap = ins.MarketCap;
					dbIns.MarketId = con.Markets.Where(s => s.Code == ins.Exchange).FirstOrDefault().Id;
					dbIns.MarketPrice = ins.MarketPrice;// string.IsNullOrEmpty(ins.MarketPrice) ? 0.00M : decimal.Parse(ins.MarketPrice);
					dbIns.Name = ins.Name;
					dbIns.Peratio = string.IsNullOrEmpty(ins.PeRatio) ? 0.00M : decimal.Parse(ins.PeRatio);
					dbIns.RiskRating = ins.RiskRating;
					dbIns.Shid = Guid.Parse(ins.Id);
					dbIns.Symbol = ins.Symbol;

					//add another historic data 
					if (dbIns.UpdatedOn != ins.MarketLastCheck) {
						NowDBContext newCon = new NowDBContext();
						newCon.PriceHistories.Add(new PriceHistory() { InstrumentId = dbIns.Id, RecordedOn = ins.MarketLastCheck, Price = ins.MarketPrice });
						newCon.SaveChanges();
						newCon.Dispose();
					}
					dbIns.UpdatedOn = ins.MarketLastCheck;
					dbIns.WebsiteUrl = ins.WebsiteUrl;

					SetComparativePrices(ref dbIns, ins);
				}

				
				con.SaveChanges();
				con.Dispose();
			}
		}

		static void SetComparativePrices(ref SQLDataAccess.Models.Instrument dbins, Common.Models.Instrument ins){
			if(ins.ComparisonPrices != null){
				decimal unParsedValue = 0;
				if (ins.ComparisonPrices.OneDay != null) {

					if (decimal.TryParse(ins.ComparisonPrices.OneDay.Value, out unParsedValue)) {
						dbins.OneDayValue = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneDay.Percent, out unParsedValue)) {
						dbins.OneDayPercentage = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneDay.Min, out unParsedValue)) {
						dbins.OneDayMinimum = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneDay.Max, out unParsedValue)) {
						dbins.OneDayMaximum = unParsedValue;
						unParsedValue = 0;
					}
				}

				if (ins.ComparisonPrices.OneWeek != null) {
					if (decimal.TryParse(ins.ComparisonPrices.OneWeek.Value, out unParsedValue)) {
						dbins.OneWeekValue = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneWeek.Percent, out unParsedValue)) {
						dbins.OneWeekPercentage = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneWeek.Min, out unParsedValue)) {
						dbins.OneWeekMinimum = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneWeek.Max, out unParsedValue)) {
						dbins.OneWeekMaximum = unParsedValue;
						unParsedValue = 0;
					}
				}

				if (ins.ComparisonPrices.OneMonth != null) {
					if (decimal.TryParse(ins.ComparisonPrices.OneMonth.Value, out unParsedValue)) {
						dbins.OneMonthValue = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneMonth.Percent, out unParsedValue)) {
						dbins.OneMonthPercentage = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneMonth.Min, out unParsedValue)) {
						dbins.OneMonthMinimum = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneMonth.Max, out unParsedValue)) {
						dbins.OneMonthMaximum = unParsedValue;
						unParsedValue = 0;
					}
				}

				if (ins.ComparisonPrices.ThreeMonth != null) {
					if (decimal.TryParse(ins.ComparisonPrices.ThreeMonth.Value, out unParsedValue)) {
						dbins.ThreeMonthValue = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.ThreeMonth.Percent, out unParsedValue)) {
						dbins.ThreeMonthPercentage = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.ThreeMonth.Min, out unParsedValue)) {
						dbins.ThreeMonthMinimum = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.ThreeMonth.Max, out unParsedValue)) {
						dbins.ThreeMonthMaximum = unParsedValue;
						unParsedValue = 0;
					}
				}

				if (ins.ComparisonPrices.SixMonth != null) {
					if (decimal.TryParse(ins.ComparisonPrices.SixMonth.Value, out unParsedValue)) {
						dbins.SixMonthValue = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.SixMonth.Percent, out unParsedValue)) {
						dbins.SixMonthPercentage = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.SixMonth.Min, out unParsedValue)) {
						dbins.SixMonthMinimum = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.SixMonth.Max, out unParsedValue)) {
						dbins.SixMonthMaximum = unParsedValue;
						unParsedValue = 0;
					}
				}

				if (ins.ComparisonPrices.OneYear != null) {
					if (decimal.TryParse(ins.ComparisonPrices.OneYear.Value, out unParsedValue)) {
						dbins.OneYearValue = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneYear.Percent, out unParsedValue)) {
						dbins.OneYearPercentage = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneYear.Min, out unParsedValue)) {
						dbins.OneYearMinimum = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.OneYear.Max, out unParsedValue)) {
						dbins.OneYearMaximum = unParsedValue;
						unParsedValue = 0;
					}
				}

				if (ins.ComparisonPrices.FiveYear != null) {
					if (decimal.TryParse(ins.ComparisonPrices.FiveYear.Value, out unParsedValue)) {
						dbins.FiveYearValue = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.FiveYear.Percent, out unParsedValue)) {
						dbins.FiveYearPercentage = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.FiveYear.Min, out unParsedValue)) {
						dbins.FiveYearMinimum = unParsedValue;
						unParsedValue = 0;
					}

					if (decimal.TryParse(ins.ComparisonPrices.FiveYear.Max, out unParsedValue)) {
						dbins.FiveYearMaximum = unParsedValue;
						unParsedValue = 0;
					}
				}
			}
		}
		public static List<Market> GetOpenMarkets() {
			List<Market> OpenMarkets = new List<Market>();
			NowDBContext con = new NowDBContext();
			TimeSpan currentNZTime = DateTime.Now.TimeOfDay;
			DateTime today = DateTime.Now;
			OpenMarkets = con.Markets.Where(s => s.OpeningTimeNz < currentNZTime && s.ClosingTimeNz > currentNZTime && !s.ClosingDays.Any(c => c.ClosingDate.Date == today.Date)).ToList();
			con.Dispose();
			return OpenMarkets;
		}
	}
}