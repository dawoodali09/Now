using Microsoft.Extensions.Configuration;
using Common.Models;
using Trader;
using System.Diagnostics;
using System;
using System.IO;

namespace NowConsole {
	class Program {
        const string LocalWindowsConfigLocation = "C:\\temp\\Now\\appsettings.json";
        const string LocalMACConfigLocation = "C:\\temp\\Now\\appsettings.json"; //???

        static void Main(string[] args)
        {
            IConfiguration Configuration = GetConfiguration(args, Common.Enums.Machine.WINDOWS);
            var emailSection = Configuration.GetSection("Email");
            var passwordSection = Configuration.GetSection("Password");
            var DatamodeSection = Configuration.GetSection("DataMode");

            var MongoConnection = Configuration.GetSection("MongoConnection");
            var SQLConnection = Configuration.GetSection("SQLConnection");

            if (emailSection != null && passwordSection != null && DatamodeSection != null)
            {
               
                string email = emailSection.Value;
                string password = passwordSection.Value;
                string strDataMode = DatamodeSection.Value;
                
                Enum.TryParse(strDataMode, out Common.Enums.DataMode mode);
                string connectionStr = mode == Common.Enums.DataMode.MONGO ? MongoConnection.Value : SQLConnection.Value;
                Trader.Trader trader = new Trader.Trader(mode, connectionStr);
                //trader.StoreCategories();
                //trader.SharesiesLogin(new Credentials() { email = email, password = password });
                //trader.FeedSharesiesInstrumentData(trader.session);
            }

            static IConfiguration GetConfiguration(string[] args, Common.Enums.Machine? mac = null) {
                if (mac == Common.Enums.Machine.WINDOWS) {
                    return new ConfigurationBuilder()
                           .AddJsonFile(LocalWindowsConfigLocation, optional: true, reloadOnChange: true).AddEnvironmentVariables().AddCommandLine(args).Build();
                } else if (mac == Common.Enums.Machine.MAC) {
                    return new ConfigurationBuilder()
                           .AddJsonFile(LocalMACConfigLocation, optional: true, reloadOnChange: true).AddEnvironmentVariables().AddCommandLine(args).Build();
                }
                return new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().AddCommandLine(args).Build();
            }
        }
    }
}