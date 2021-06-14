using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
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

			// store all response header keys
			System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\temp\\headers.txt", true);
			foreach (var hk in response.Headers) {
				this.session.headers.Add(hk.ToString(), response.Headers[hk.ToString()]);
				sw.WriteLine(hk.ToString() + " : " + response.Headers[hk.ToString()]);
			}
			sw.Close();
		}

		public void GetShares(Session session) {
			string payload = "params = {'Page': 1,'Sort': 'marketCap','PriceChangeTime': '1y','Query': ''}";
			NameValueCollection nvc = new NameValueCollection();
			nvc.Add("Authorization", "Bearer {"+ session.credentials.auth_token+ "}");
			//nvc.Add("Authorization", "Bearer " + session.credentials.auth_token + "");

			Utility.GetData("https://data.sharesies.nz/api/v1/instruments?Page=1&PerPage=60&Sort=marketCap&PriceChangeTime=1y&Query=", payload, nvc);
			//throw new NotImplementedException();
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
	}
}
