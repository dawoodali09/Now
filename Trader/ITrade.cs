using Common.Models;
using System.Collections;
using System.Collections.Generic;


namespace Trader {
	interface ITrade {
		public void SharesiesLogin(Credentials crendials);

		public User ReAuth(Session session);

		public void FeedSharesiesInstrumentData(Session session);

		public void GetInstruments(Session session);

		public void GetHistory(Session session, string shareID);

		public void GetCompanies(Session session);

		public void GetInfo(Session session);

		public void GetProfile(Session session);

		public void Buy(Session session, string company, int amount);

		public void Sell(Session session, string company, int amount);

		public List<Recommendation> SuggestBuyNow(string exchange, decimal budgetPerShare);
		public List<Recommendation> SuggestSellNow(string exchange, decimal budgetPerShare);
		// buy now  input (market, budget per share) returns shares with rule id
		// sell now input shares from portfolio , fee , age) set profit and loss list of shares to sell with rule id

	}
}
