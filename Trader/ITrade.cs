namespace Trader {
	interface ITrade {
		public void Login(Credentials crendials);

		public User ReAuth(Session session);

		public void FeedData(Session session);

		public void GetInstruments(Session session);

		public void GetHistory(Session session, string shareID);

		public void GetCompanies(Session session);

		public void GetInfo(Session session);

		public void GetProfile(Session session);

		public void Buy(Session session, string company, int amount);

		public void Sell(Session session, string company, int amount);

	}
}
