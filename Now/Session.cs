using System.Collections.Specialized;

namespace Now {
	public class Session {
		public User account { get; set; }
		public NameValueCollection headers { get; set; }
		public Credentials credentials { get; set; }

	}
}
