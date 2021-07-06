using System;
using System.Collections.Specialized;

namespace Models
{

    public class Session
    {
        public User account { get; set; }
        public NameValueCollection headers { get; set; }
        public Credentials credentials { get; set; }

    }

}
