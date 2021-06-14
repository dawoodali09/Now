using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
 
using System.IO;
 
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO.Compression;
using Newtonsoft.Json;

namespace Now {
	class Program {
		static void Main(string[] args) {

            Trader ninja = new Trader();

            if (string.IsNullOrEmpty(ninja.session.credentials.auth_token)) {
                string email = "dawoodali@gmail.com"; // should come from config.
                string password = "Newzealand123!";// should come from config.
                ninja.Login(new Credentials() { email = email, password = password });
            }
            // ninja.GetShares(ninja.session);


            HttpClient _httpClient = new HttpClient();
       
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ninja.session.credentials.auth_token);
            string requestUri = "https://data.sharesies.nz/api/v1/instruments?Page=1&PerPage=60&Sort=marketCap&PriceChangeTime=1y&Query=&InstrumentTypes=equity";


            //_httpClient.DefaultRequestHeaders.Add(["DNT"] = "1";
            //request.Headers["Referer"] = "https://app.sharesies.nz";
            //request.Headers["Authorization"] = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJFUzI1NiJ9.eyJleHAiOjE2MjIwODk0MjAsImlzcyI6InNoYXJlc2llcyIsImx2bCI6ImFzeCJ9.RLbCpWcF1f6hsnGeVbngeqQF-yz6Ac553SvAfbJo1fvOLJ-RRg9grat0Zo1jrOWv5L7wb32TauBzU8QiHyxkGw";
            //request.Headers["Origin"] = "https://app.sharesies.nz";
            //request.Headers["sec-ch-ua"] = " \"Not; A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\";";
            //request.Headers["sec-ch-ua-mobile"] = "?0";
            //request.Headers["Sec-Fetch-Dest"] = "empty";
            //request.Headers["Sec-Fetch-Mode"] = "cors";
            //request.Headers["Sec-Fetch-Site"] = "same-Site";
            //request.Host = "app.sharesies.nz";

            //request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = "application/json";



            HttpResponseMessage httpResponse =  _httpClient.GetAsync(requestUri).Result;

            var res = httpResponse.Content.ReadAsStringAsync();

            //const string WEBSERVICE_URL = "https://data.sharesies.nz/api/v1/instruments/info";
            //try {
            //    var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
            //    if (webRequest != null) {
            //        webRequest.Method = "GET";
            //        webRequest.Timeout = 12000;
            //        webRequest.ContentType = "application/json";
            //        webRequest.Headers.Add("Authorization", "Bearer "+ ninja.session.credentials.auth_token + "");


            //        //webRequest.Accept = "*/*";
            //        //webRequest.Headers["Accept-Encoding"] = "gzip, deflate, br";
            //        //webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36";
            //        webRequest.Headers["Accept-Language"] = "en-GB,en-US;q=0.9,en;q=0.8";
            //        //System.Net.NetworkCredential nc = new NetworkCredential("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJFUzI1NiJ9.eyJleHAiOjE2MjIwODk0MjAsImlzcyI6InNoYXJlc2llcyIsImx2bCI6ImFzeCJ9.RLbCpWcF1f6hsnGeVbngeqQF-yz6Ac553SvAfbJo1fvOLJ-RRg9grat0Zo1jrOWv5L7wb32TauBzU8QiHyxkGw");
            //        //request.Credentials = nc;

            //        webRequest.Headers["DNT"] = "1";
            //        webRequest.Headers["Referer"] = "https://app.sharesies.nz";
            //        //webRequest.Headers["Authorization"] = "Bearer eyJ0eXAiOiJKV1QiLJhbGciOiJFUzI1NiJ9.eyJleHAiOjE2MjIwODk0MjAsImlzcyI6InNoYXJlc2llcyIsImx2bCI6ImFzeCJ9.RLbCpWcF1f6hsnGeVbngeqQF-yz6Ac553SvAfbJo1fvOLJ-RRg9grat0Zo1jrOWv5L7wb32TauBzU8QiHyxkGw";
            //        webRequest.Headers["Origin"] = "https://app.sharesies.nz";
            //        webRequest.Headers["sec-ch-ua"] = " \"Not; A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\";";
            //        webRequest.Headers["sec-ch-ua-mobile"] = "?0";
            //        webRequest.Headers["Sec-Fetch-Dest"] = "empty";
            //        webRequest.Headers["Sec-Fetch-Mode"] = "cors";
            //        webRequest.Headers["Sec-Fetch-Site"] = "same-Site";
            //        webRequest.Headers["Host"] = "app.sharesies.nz";


            //        //request.ContentType = "application/x-www-form-urlencoded";
            //       // request.ContentType = "application/json";



            //        using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream()) {
            //            using (System.IO.StreamReader sr = new System.IO.StreamReader(s)) {
            //                var jsonResponse = sr.ReadToEnd();
            //                Console.WriteLine(String.Format("Response: {0}", jsonResponse));
            //            }
            //        }
            //    }
            //}             
            //catch (WebException wex)
            //{
            //    string cp = wex.Message;
            //} catch (Exception ex) {
            //    Console.WriteLine(ex.ToString());
            //}


            var request = (HttpWebRequest)WebRequest.Create("https://data.sharesies.nz/api/v1/instruments?Page=1&PerPage=60&Sort=marketCap&PriceChangeTime=1y&Query=&InstrumentTypes=equity");
            request.Accept = "*/*";
            request.Headers["Accept-Encoding"] = "gzip, deflate, br";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36";
            request.Headers["Accept-Language"] = "en-GB,en-US;q=0.9,en;q=0.8";
            System.Net.NetworkCredential nc = new NetworkCredential("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJFUzI1NiJ9.eyJleHAiOjE2MjIwODk0MjAsImlzcyI6InNoYXJlc2llcyIsImx2bCI6ImFzeCJ9.RLbCpWcF1f6hsnGeVbngeqQF-yz6Ac553SvAfbJo1fvOLJ-RRg9grat0Zo1jrOWv5L7wb32TauBzU8QiHyxkGw");
            request.Credentials = nc;

            request.Headers["DNT"] = "1";
            request.Headers["Referer"] = "https://app.sharesies.nz";
            request.Headers["Authorization"] = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJFUzI1NiJ9.eyJleHAiOjE2MjIwODk0MjAsImlzcyI6InNoYXJlc2llcyIsImx2bCI6ImFzeCJ9.RLbCpWcF1f6hsnGeVbngeqQF-yz6Ac553SvAfbJo1fvOLJ-RRg9grat0Zo1jrOWv5L7wb32TauBzU8QiHyxkGw";
            request.Headers["Origin"] = "https://app.sharesies.nz";
            request.Headers["sec-ch-ua"] = " \"Not; A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\";";
            request.Headers["sec-ch-ua-mobile"] = "?0";            
            request.Headers["Sec-Fetch-Dest"] = "empty";
            request.Headers["Sec-Fetch-Mode"] = "cors";
            request.Headers["Sec-Fetch-Site"] = "same-Site";
            request.Host = "app.sharesies.nz";
            
            request.Method = "GET";  
            request.ContentType = "application/json";
            
            var response = (HttpWebResponse)request.GetResponse();

        }
	}
}
