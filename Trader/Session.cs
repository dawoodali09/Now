using System.Collections.Specialized;

namespace Trader {
	public class Session {
		public User account { get; set; }
		public NameValueCollection headers { get; set; }
		public Credentials credentials { get; set; }

	}
}
