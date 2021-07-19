using SQLDataAccess.Models;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using PriceHistory = SQLDataAccess.Models.PriceHistory;
using Common.Enums;

namespace Trader {
	public class Trader : ITrade {

		#region "Properties"
		public Analyst.Analyst analyst;
		Common.Enums.DataMode DataMode;
		public Session session;
		public string ConnectionString { get; set; }
		#endregion

		#region "Active Methods"
		public Trader(Common.Enums.DataMode mode) {
			this.DataMode = mode;
		}

		public Trader(Common.Enums.DataMode mode, string DataConnection) {
			this.DataMode = mode;
			this.ConnectionString = DataConnection;
		}

		public void SharesiesLogin(Credentials suppliedCredentials) {

			string loginPostRequest = "{\"email\":\"" + suppliedCredentials.email + "\",\"password\":\"" + suppliedCredentials.password + "\",\"remember\":false}";
			HttpWebResponse response = Utility.Methods.PostData("https://app.sharesies.com/api/identity/login", loginPostRequest);
			var responseBytes = Utility.Methods.StreamToByteArray(response.GetResponseStream());
			var decodedData = Utility.Methods.ExtractGZipData(responseBytes);

			// set session user
			var responseString = Encoding.UTF8.GetString(decodedData, 0, decodedData.Length);
			LoginResponse loginResponseObject = JsonConvert.DeserializeObject<LoginResponse>(responseString);

			var parsed = Newtonsoft.Json.Linq.JObject.Parse(responseString);
			suppliedCredentials.auth_token = parsed.SelectToken("distill_token").ToString();
			suppliedCredentials.accountReference = parsed.SelectToken("user.account_reference").ToString();
			suppliedCredentials.userID = parsed.SelectToken("user.id").ToString();
			suppliedCredentials.intercom = parsed.SelectToken("user.intercom").ToString();
			suppliedCredentials.portfolioID = parsed.SelectToken("user.portfolio_id").ToString();

			//this.session.account = loginResponseObject.User;
			//this.session.credentials = suppliedCredentials;

			System.Collections.Specialized.NameValueCollection collection = new System.Collections.Specialized.NameValueCollection();
			foreach (var hk in response.Headers) {
				collection.Add(hk.ToString(), response.Headers[hk.ToString()]);
			}
			Session.SetSession(loginResponseObject.User, collection, suppliedCredentials);
			Session.Expiry = DateTime.Now.AddMinutes(5);
		}

		public void FeedSharesiesInstrumentData(Session session) {
			// this function will make a call to sharesied and load import all the missing data into database 
			// if data is already present it will update the data that needed to be updated
			// it will also add data that needed for history.

			int PageNumber = 0;
			bool allPagesDone = false;

			while (allPagesDone == false) {
				PageNumber++;

				if (Session.IsExpired()) {
					this.SharesiesLogin(Session.credentials);
				}

				HttpClient _httpClient = new HttpClient();
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.credentials.auth_token);
				string requestUri = "https://data.sharesies.nz/api/v1/instruments?Page=" + PageNumber.ToString().Trim() + "&PerPage=60&Sort=marketCap&PriceChangeTime=1y&Query=&InstrumentTypes=equity";
				HttpResponseMessage httpResponse = _httpClient.GetAsync(requestUri).Result;
				var res = httpResponse.Content.ReadAsStringAsync();
				InstrumentDataResponse ird = new InstrumentDataResponse();
				ird = JsonConvert.DeserializeObject<InstrumentDataResponse>(res.Result);
				allPagesDone = (ird.NumberOfPages == ird.CurrentPage);

				Console.WriteLine("Processing PAge " + PageNumber.ToString() + Environment.NewLine);
				foreach (var ins in ird.Instruments) {
					if (this.DataMode == Common.Enums.DataMode.MONGO) {
						MongoAccess.DataMethods.Methods.AddUpdateInstrumment(ins, this.ConnectionString);
						Console.WriteLine(ins.Name + " Done");
					} else if (this.DataMode == Common.Enums.DataMode.SQL) {
						SQLDataAccess.DataMethods.Methods.AddUpdateInstrument(ins, this.ConnectionString);
					} else {
						//do nothing for now.
					}
				}
			}
		}

		public List<Recommendation> SuggestBuyNow(string exchange, decimal budgetPerShare) {
			
			return Analyst.Analyst.GetInstance().BuyNow(exchange, budgetPerShare, DataMode, ConnectionString);
		}

		#endregion


		#region "Beta Methods"
		public void PopulateRules(){
			if(DataMode == DataMode.SQL){
				SQLDataAccess.DataMethods.Methods.PoppulateRules(this.ConnectionString);
			}else if (DataMode == DataMode.MONGO){
				MongoAccess.DataMethods.Methods.PoppulateRules(this.ConnectionString);
			}
		}

		public void StoreCategories() {
			if (DataMode == Common.Enums.DataMode.MONGO) {
				MongoAccess.DataMethods.Methods.AddUpdateCategories(this.ConnectionString);
			} else if (DataMode == Common.Enums.DataMode.SQL) {
				SQLDataAccess.DataMethods.Methods.AddUpdateCategories(this.ConnectionString);
			}
		}

		public void EXP(Session session) {

			HttpClient _httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.credentials.auth_token);
			string requestUri = "https://data.sharesies.nz/api/v1/instruments/info";
			HttpResponseMessage httpResponse = _httpClient.GetAsync(requestUri).Result;
			var res = httpResponse.Content.ReadAsStringAsync();
		}

		public void EXP2(Session session) {


			//'fund_id={}&first={}&last={}'.format(
			//company['id'], '2000-01-01', today.strftime("%Y-%m-%d")
			HttpClient _httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.credentials.auth_token);
			string requestUri = "https://app.sharesies.nz/api/fund/price-history?'id=C218F7FC-7A19-4527-B1FE-E7E71935E6FE&first=2021-06-01&last=2020-06-30";
			HttpResponseMessage httpResponse = _httpClient.GetAsync(requestUri).Result;
			var res = httpResponse.Content.ReadAsStringAsync();
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
		public void GetYahooData() {
			HttpClient _httpClient = new HttpClient();
			//_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.credentials.auth_token);
			int offset = 1, count = 100;
			string requestUri = "https://nz.finance.yahoo.com/most-active?offset=" + offset.ToString() + "&count=100";
			HttpResponseMessage httpResponse = _httpClient.GetAsync(requestUri).Result;
			var res = httpResponse.Content.ReadAsStringAsync();

		}

		public void GetNZXShares() {
			foreach (var s in MongoAccess.DataMethods.Methods.ListInstruments("NZX", this.ConnectionString).ToList()) {
				//var sph = MongoAccess.DataMethods.Methods.ListStockPriceHistory(s.Id, this.ConnectionString);
			}
		}

		//its a very long running method usually it fetches 5 years data for each stock.
		public void FeedPriceHistory(Session session, int LastRecordedId = 0) {
			// this function will make a call to sharesied and load import all the missing data into database 
			// if data is already present it will update the data that needed to be updated
			// it will also add data that needed for history.
			if (this.DataMode == Common.Enums.DataMode.MONGO) {
				List<Common.Models.Instrument> Instruments = MongoAccess.DataMethods.Methods.ListInstruments(this.ConnectionString);

				foreach (var sh in Instruments.ToList()) {
					if (Session.IsExpired()) {
						this.SharesiesLogin(Session.credentials);
					}

					HttpClient _httpClient = new HttpClient();
					_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.credentials.auth_token);
					string requestUri = "https://data.sharesies.nz/api/v1/instruments/" + sh.Id.ToString() + "/pricehistory";
					HttpResponseMessage httpResponse = _httpClient.GetAsync(requestUri).Result;
					var res = httpResponse.Content.ReadAsStringAsync();

					if (res.IsCompleted) {

						var response = res.Result;
						if (response.Contains("Auth expired")) {
							throw new Exception("Why session expirty isn't working ?");
						}
						var JobjectResponse = Newtonsoft.Json.Linq.JObject.Parse(response);
						var pHistory = JobjectResponse["dayPrices"];
						Common.Models.StockPriceHistory sph = new StockPriceHistory() { Id = sh.Id, History = new List<Common.Models.PriceHistory>() };

						foreach (var his in pHistory) {
							string str = his.ToString().Replace("\\", string.Empty);
							str = his.ToString().Replace("\"", string.Empty);
							str = str.Trim();
							DateTime dt = DateTime.Parse(str.Split(':')[0].Trim());
							decimal mon = decimal.Parse(str.Split(':')[1].Trim());
							sph.History.Add(new Common.Models.PriceHistory() { Price = mon, RecordedOn = dt });
						}

						MongoAccess.DataMethods.Methods.AddUpdatePriceHistory(sph, this.ConnectionString);
						Console.WriteLine(sh.Name + " Done");
					}
				}
			} else if (this.DataMode == Common.Enums.DataMode.SQL) {
				SQLDataAccess.Models.NowDBContext con = new SQLDataAccess.Models.NowDBContext(this.ConnectionString);
				foreach (var sh in con.Instruments.Where(s => s.Id > LastRecordedId).ToList()) {
					List<SQLDataAccess.Models.PriceHistory> historyList = new List<SQLDataAccess.Models.PriceHistory>();

					if (Session.IsExpired()) {
						this.SharesiesLogin(Session.credentials);
					}

					HttpClient _httpClient = new HttpClient();
					_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Session.credentials.auth_token);
					string requestUri = "https://data.sharesies.nz/api/v1/instruments/" + sh.Shid.ToString() + "/pricehistory";
					HttpResponseMessage httpResponse = _httpClient.GetAsync(requestUri).Result;
					var res = httpResponse.Content.ReadAsStringAsync();

					if (res.IsCompleted) {
						var response = res.Result;
						if (response.Contains("Auth expired")) {
							throw new Exception("Why session expirty isn't working ?");
						}
						var JobjectResponse = Newtonsoft.Json.Linq.JObject.Parse(response);
						var pHistory = JobjectResponse["dayPrices"];

						//   \"2015-08-31\": 28.19
						foreach (var his in pHistory) {
							string str = his.ToString().Replace("\\", string.Empty);
							str = his.ToString().Replace("\"", string.Empty);
							str = str.Trim();
							DateTime dt = DateTime.Parse(str.Split(':')[0].Trim());
							decimal mon = decimal.Parse(str.Split(':')[1].Trim());
							PriceHistory dph = new PriceHistory() { InstrumentId = sh.Id, Price = mon, RecordedOn = dt };
							historyList.Add(dph);
						}
						NowDBContext newCon = new NowDBContext(this.ConnectionString);
						newCon.PriceHistories.AddRange(historyList.AsEnumerable());
						newCon.SaveChanges();
						newCon.Dispose();
						Console.WriteLine(" " + sh.Name + " Done;");
					}
				}
			}
		}

		void ITrade.SharesiesLogin(Credentials crendials) {
			throw new NotImplementedException();
		}

		User ITrade.ReAuth(Session session) {
			throw new NotImplementedException();
		}

		void ITrade.FeedSharesiesInstrumentData(Session session) {
			throw new NotImplementedException();
		}

		void ITrade.GetInstruments(Session session) {
			throw new NotImplementedException();
		}

		void ITrade.GetHistory(Session session, string shareID) {
			throw new NotImplementedException();
		}

		void ITrade.GetCompanies(Session session) {
			throw new NotImplementedException();
		}

		void ITrade.GetInfo(Session session) {
			throw new NotImplementedException();
		}

		void ITrade.GetProfile(Session session) {
			throw new NotImplementedException();
		}

		void ITrade.Buy(Session session, string company, int amount) {
			throw new NotImplementedException();
		}

		void ITrade.Sell(Session session, string company, int amount) {
			throw new NotImplementedException();
		}

		

		List<Recommendation> ITrade.SuggestSellNow(string exchange, decimal budgetPerShare) {
			throw new NotImplementedException();
		}
		#endregion

	}
}
