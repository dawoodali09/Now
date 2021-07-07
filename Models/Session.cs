using System.Collections.Specialized;

namespace Common.Models {

	public class Session {
		private Session() {
			account = new User();
			headers = new NameValueCollection();
			credentials = new Credentials();
		}
		private static Session _instance;
		public static Session GetInstance() {
			if (_instance == null) {
				_instance = new Session();
			}
			return _instance;
		}

		public static void SetSession(User userAccount, NameValueCollection userHeaders, Credentials userCredentials) {
			account = userAccount;
			headers = userHeaders;
			credentials = userCredentials;
		}

		public static User account { get; set; }
		public static NameValueCollection headers { get; set; }
		public static Credentials credentials { get; set; }
	}
}
