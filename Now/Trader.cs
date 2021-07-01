using DataAccess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Now {
	public class Trader : ITrade {
		public Trader (){
			this.session = new Session() {
				account = null,
				credentials = new Credentials() { auth_token=string.Empty, email=string.Empty, password = string.Empty },
				headers = new System.Collections.Specialized.NameValueCollection()				
			};
		}
		public Session session { get; set; }

		public void Login(Credentials suppliedCredentials) {
			string loginPostRequest = "{\"email\":\"" + suppliedCredentials.email + "\",\"password\":\"" + suppliedCredentials.password + "\",\"remember\":false}";
			HttpWebResponse response = Utility.PostData("https://app.sharesies.com/api/identity/login", loginPostRequest);
			var responseBytes = Utility.StreamToByteArray(response.GetResponseStream());
			var decodedData = Utility.ExtractGZipData(responseBytes);

			// set session user
			var responseString = Encoding.UTF8.GetString(decodedData, 0, decodedData.Length);
			LoginResponse lr = JsonConvert.DeserializeObject<LoginResponse>(responseString);
			this.session.account = lr.User;

			var parsed = Newtonsoft.Json.Linq.JObject.Parse(responseString);
			suppliedCredentials.auth_token = parsed.SelectToken("distill_token").ToString();
			suppliedCredentials.accountReference = parsed.SelectToken("user.account_reference").ToString();
			suppliedCredentials.userID = parsed.SelectToken("user.id").ToString();
			suppliedCredentials.intercom = parsed.SelectToken("user.intercom").ToString();
			suppliedCredentials.portfolioID = parsed.SelectToken("user.portfolio_id").ToString();
			this.session.credentials = suppliedCredentials;

			// store all response header keys in future maybe not now.
			//if(File.Exists("C:\\temp\\headers.txt")) {
			//	File.Delete("C:\\temp\\headers.txt");
			//}
			//System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\temp\\headers.txt", true);
			//foreach (var hk in response.Headers) {
			//	this.session.headers.Add(hk.ToString(), response.Headers[hk.ToString()]);
			//	sw.WriteLine(hk.ToString() + " : " + response.Headers[hk.ToString()]);
			//}
			//sw.Close();
		}

		public void EXP(Session session) {

			HttpClient _httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.session.credentials.auth_token);
			string requestUri = "https://data.sharesies.nz/api/v1/instruments/info";
			HttpResponseMessage httpResponse = _httpClient.GetAsync(requestUri).Result;
			var res = httpResponse.Content.ReadAsStringAsync();
		}

		public void EXP2(Session session) {
 

			//'fund_id={}&first={}&last={}'.format(
			//company['id'], '2000-01-01', today.strftime("%Y-%m-%d")
			HttpClient _httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.session.credentials.auth_token);
			string requestUri = "https://app.sharesies.nz/api/fund/price-history?'id=C218F7FC-7A19-4527-B1FE-E7E71935E6FE&first=2021-06-01&last=2020-06-30";
			HttpResponseMessage httpResponse = _httpClient.GetAsync(requestUri).Result;
			var res = httpResponse.Content.ReadAsStringAsync();
		}

		//its a very long running method usually it fetches 5 years data for each stock.
		public void FeedPriceHistory(Session session, int LastRecordedId = 0) {
			// this function will make a call to sharesied and load import all the missing data into database 
			// if data is already present it will update the data that needed to be updated
			// it will also add data that needed for history.
			DataAccess.Models.NowDBContext con = new DataAccess.Models.NowDBContext();
			foreach (var sh in con.Instruments.Where(s=> s.Id > LastRecordedId).ToList()) {
				List<PriceHistory> historyList = new List<PriceHistory>();
				HttpClient _httpClient = new HttpClient();
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.session.credentials.auth_token);
				string requestUri = "https://data.sharesies.nz/api/v1/instruments/"  + sh.Shid.ToString() + "/pricehistory";
				HttpResponseMessage httpResponse = _httpClient.GetAsync(requestUri).Result;
				var res = httpResponse.Content.ReadAsStringAsync();
				
				if(res.IsCompleted){
					var response = res.Result;
					if( response.Contains("Auth expired"))
					{
						Trader ninja = new Trader();
						if (string.IsNullOrEmpty(ninja.session.credentials.auth_token)) {
							string email = "xxx@gmail.com"; // should come from config.
							string password = "xxxx!";// should come from config.
							ninja.Login(new Credentials() { email = email, password = password });
							this.session = ninja.session;
						}
						FeedPriceHistory(ninja.session, sh.Id - 1);
						return;
					}
					var JobjectResponse = Newtonsoft.Json.Linq.JObject.Parse(response);
					var pHistory = JobjectResponse["dayPrices"];

					//   \"2015-08-31\": 28.19
					foreach ( var his in pHistory)
					{
						string str = his.ToString().Replace("\\",string.Empty);
						str = his.ToString().Replace("\"", string.Empty);
						str = str.Trim();						
						DateTime dt = DateTime.Parse(str.Split(':')[0].Trim());
						decimal mon = decimal.Parse(str.Split(':')[1].Trim());
						PriceHistory dph = new PriceHistory() { InstrumentId = sh.Id, Price = mon, RecordedOn = dt };
						historyList.Add(dph);
					}
					NowDBContext newCon = new NowDBContext();
					newCon.PriceHistories.AddRange(historyList.AsEnumerable());
					newCon.SaveChanges();
					newCon.Dispose();					
					Console.WriteLine(" " + sh.Name + " Done;");
				}
			}
		}

		public void FeedData(Session session) {
			// this function will make a call to sharesied and load import all the missing data into database 
			// if data is already present it will update the data that needed to be updated
			// it will also add data that needed for history.

			int PageNumber = 0;
			bool allPagesDone = false;

			while (allPagesDone == false) {
				PageNumber++;
				HttpClient _httpClient = new HttpClient();
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.session.credentials.auth_token);
				string requestUri = "https://data.sharesies.nz/api/v1/instruments?Page=" + PageNumber.ToString().Trim() + "&PerPage=60&Sort=marketCap&PriceChangeTime=1y&Query=&InstrumentTypes=equity";
				HttpResponseMessage httpResponse = _httpClient.GetAsync(requestUri).Result;
				var res = httpResponse.Content.ReadAsStringAsync();
				InstrumentDataResponse ird = new InstrumentDataResponse();
				ird = JsonConvert.DeserializeObject<InstrumentDataResponse>(res.Result);
				allPagesDone = (ird.NumberOfPages == ird.CurrentPage);

				Console.WriteLine("Processing PAge " + PageNumber.ToString() + Environment.NewLine);
				foreach (var ins in ird.Instruments) {
					DataAccess.Models.NowDBContext con = new DataAccess.Models.NowDBContext();

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
					// update the market data..

					// store the instrument 
					DataAccess.Models.Instrument dbIns = new DataAccess.Models.Instrument();
					if (!con.Instruments.Any(x => x.Shid == Guid.Parse(ins.Id))) {
						dbIns = new DataAccess.Models.Instrument() {
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
							MarketPrice = string.IsNullOrEmpty(ins.MarketPrice) ? 0.00M : decimal.Parse(ins.MarketPrice),
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
						dbIns.MarketPrice = string.IsNullOrEmpty(ins.MarketPrice) ? 0.00M : decimal.Parse(ins.MarketPrice);
						dbIns.Name = ins.Name;
						dbIns.Peratio = string.IsNullOrEmpty(ins.PeRatio) ? 0.00M : decimal.Parse(ins.PeRatio);
						dbIns.RiskRating = ins.RiskRating;
						dbIns.Shid = Guid.Parse(ins.Id);
						dbIns.Symbol = ins.Symbol;
						
						//add another historic data 
						if (dbIns.UpdatedOn != ins.MarketLastCheck && !string.IsNullOrEmpty(ins.MarketPrice)){
							NowDBContext newCon = new NowDBContext();
							newCon.PriceHistories.Add(new PriceHistory() { InstrumentId = dbIns.Id, RecordedOn = ins.MarketLastCheck, Price = decimal.Parse(ins.MarketPrice) });
							newCon.SaveChanges();
							newCon.Dispose();
						}
						dbIns.UpdatedOn = ins.MarketLastCheck;
						dbIns.WebsiteUrl = ins.WebsiteUrl;
					}					
					con.SaveChanges();
				}
			}
		}

		public void Buy(Session session, string company, int amount) {
			throw new NotImplementedException();
		}

		public void GetCompanies(Session session) {
			throw new NotImplementedException();
		}

		public void GetHistory(Session session, string shareID) {
			throw new NotImplementedException();
		}

		public void GetInfo(Session session) {
			throw new NotImplementedException();
		}

		public void GetInstruments(Session session) {
			throw new NotImplementedException();
		}

		public void GetProfile(Session session) {
			throw new NotImplementedException();
		}		

		public User ReAuth(Session session) {
			throw new NotImplementedException();
		}

		public void Sell(Session session, string company, int amount) {
			throw new NotImplementedException();
		}

		public List<Market> GetOpenMarkets(){
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
