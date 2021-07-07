using Common.Models;
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
				}
				con.SaveChanges();
				con.Dispose();
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
