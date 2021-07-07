using Microsoft.Extensions.Configuration;
using Common.Models;
using Trader;
using System.Diagnostics;
using System;
using System.IO;

namespace NowConsole {
	class Program {

		static void Main(string[] args) {

			IConfiguration Configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().AddCommandLine(args).Build();
			var emailSection = Configuration.GetSection("Email");
			var passwordSection = Configuration.GetSection("Password");
			var DatamodeSection = Configuration.GetSection("DataMode");

			if(emailSection != null && passwordSection != null && DatamodeSection != null){
				string email = emailSection.Value;
				string password = passwordSection.Value;
				string strDataMode = DatamodeSection.Value;
				Enum.TryParse(strDataMode, out Common.Enums.DataMode mode);
				Trader.Trader trader = new Trader.Trader(mode);
				trader.SharesiesLogin(new Credentials() { email = email, password = password });
				trader.FeedSharesiesInstrumentData(trader.session);
			}
		}
	}
}