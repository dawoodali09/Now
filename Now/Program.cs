using System.Net.Http;
using Newtonsoft.Json;
using DataAccess;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using System;


namespace Now {
    class Program {



        static void Main(string[] args) {
        
     

            ////Utility.CLoseWeekends();
            //Trader ninja = new Trader();  

            ////foreach(var mkt in ninja.GetOpenMarkets())
            ////{
            ////    Console.WriteLine(mkt.Name + "is open");
            ////}

            //if (string.IsNullOrEmpty(ninja.session.credentials.auth_token)) {
            //    string email = "myEmail@domain.com"; // should come from config.
            //    string password = "@MyPassword";// should come from config.
            //    ninja.Login(new Credentials() { email = email, password = password });
            //}
            ////ninja.EXP2(ninja.session);
            ////ninja.FeedPriceHistory(ninja.session,2458);
            //ninja.FeedData(ninja.session);

        }
    }
}