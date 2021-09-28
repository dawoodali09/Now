using Microsoft.Extensions.Configuration;
using Common.Models;
using Trader;
using System.Diagnostics;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NowConsole {
	class Program {
        const string LocalWindowsConfigLocation = "C:\\temp\\Now\\appsettings.json";
        const string LocalMACConfigLocation = "/Users/dawoodali/temp/Now/appsettings.json"; 

        static void Main(string[] args)
        {
            Common.Enums.Machine currentMachine = Common.Enums.Machine.MAC;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                currentMachine = Common.Enums.Machine.WINDOWS;
            }

            IConfiguration Configuration = GetConfiguration(args, currentMachine);
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
                trader.PopulateRules();
                trader.SuggestBuyNow("NZX", 5.00M);

                //trader.GetYahooData();
                //trader.GetNZXShares();
                //trader.StoreCategories(); // call this method only once in a blue moon
                //trader.SharesiesLogin(new Credentials() { email = email, password = password });
                //trader.FeedSharesiesInstrumentData(trader.session);
                //trader.FeedPriceHistory(trader.session);
            }

        }

        static void oldCall(string[] args)
        {
            Common.Enums.Machine currentMachine = Common.Enums.Machine.MAC;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                currentMachine = Common.Enums.Machine.WINDOWS;
            }

            IConfiguration Configuration = GetConfiguration(args, currentMachine);
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
                //trader.PopulateRules();
                //trader.SuggestBuyNow("NZX", 5.00M);
                //trader.GetYahooData();
                //trader.GetNZXShares();
                //trader.StoreCategories(); // call this method only once in a blue moon
                trader.SharesiesLogin(new Credentials() { email = email, password = password });
                trader.FeedSharesiesInstrumentData(trader.session);
                //trader.FeedPriceHistory(trader.session);
            }
        }

        static IConfiguration GetConfiguration(string[] args, Common.Enums.Machine? mac = null)
        {
            if (mac == Common.Enums.Machine.WINDOWS)
            {
                return new ConfigurationBuilder()
                       .AddJsonFile(LocalWindowsConfigLocation, optional: true, reloadOnChange: true).AddEnvironmentVariables().AddCommandLine(args).Build();
            }
            else if (mac == Common.Enums.Machine.MAC)
            {
                return new ConfigurationBuilder()
                       .AddJsonFile(LocalMACConfigLocation, optional: true, reloadOnChange: true).AddEnvironmentVariables().AddCommandLine(args).Build();
            }
            return new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().AddCommandLine(args).Build();
        }
    }
}