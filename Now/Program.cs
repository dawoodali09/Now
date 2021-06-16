using System.Net.Http;
using Newtonsoft.Json;
using DataAccess;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;

namespace Now {
	class Program {
		static void Main(string[] args) {

        Trader ninja = new Trader();    
            if (string.IsNullOrEmpty(ninja.session.credentials.auth_token)) {
                string email = "dawoodali@gmail.com"; // should come from config.
                string password = "Newzealand123!";// should come from config.
                ninja.Login(new Credentials() { email = email, password = password });
            }
            ninja.FeedData(ninja.session);

        }
	}
}
