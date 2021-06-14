using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Now {
	public class Session {
		public User account { get; set; }
		public NameValueCollection headers { get; set; }
		public Credentials credentials { get; set; }

	}
}
