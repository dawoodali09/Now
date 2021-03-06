using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thread = System.Threading.Thread;
using Queue = System.Collections.Queue;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Sharesies {
  
        // 
        //     def get_price_history(self, company):
        // 
        //         today = date.today()
        // 
        //         r = self.session.get(
        //             'https://app.sharesies.nz/api/fund/price-history?'
        //             'fund_id={}&first={}&last={}'.format(
        //                 company['id'], '2000-01-01', today.strftime("%Y-%m-%d")
        //             )
        //         )
        // 
        //         return r.json()['day_prices']
        //     
        public class SharesiesClient {

        public HttpClient client { get; set; }
        public string  user_id  { get; set; }
        public string password { get; set; }
        public string auth_token { get; set; }


        public SharesiesClient() {
            // session to remain logged in

            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 Firefox/71.0");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            //client.DefaultRequestHeaders.Add("content-type", "application/json");

            this.user_id = "";
            this.password = "";
            this.auth_token = "";
        }

        // 
        //         You must login first to access certain features
        //         
        public virtual object login(object email, object password) {
            var login_form = new Dictionary<object, object> {
                    {
                        "email",
                        email},
                    {
                        "password",
                        password},
                    {
                        "remember",
                        true}};

                        

            HttpContent _Body = new StringContent(JsonConvert.SerializeObject(login_form, Formatting.None));
            
            var r = this.client.PostAsync("https://app.sharesies.nz/api/identity/login", _Body).Result;
            //if (r["authenticated"]) {
            //    this.user_id = r["user_list"][0]["id"];
            //    this.password = password;
            //    this.auth_token = r["distill_token"];
            //    return true;
            //}
            return false;
        }

        //// 
        ////         Get all shares listed on Sharesies
        ////         
        //public virtual object get_shares() {
        //    var shares = new List<object>();
        //    var page = this.get_instruments(1);
        //    var number_of_pages = page["numberOfPages"];
        //    shares += page["instruments"];
        //    var threads = new List<object>();
        //    var que = Queue();
        //    // make threads
        //    foreach (var i in Enumerable.Range(2, number_of_pages - 2)) {
        //        threads.append(Thread(target: (q, arg1) => q.put(this.get_instruments(arg1)), args: (que, i)));
        //    }
        //    // start threads
        //    foreach (var thread in threads) {
        //        thread.start();
        //    }
        //    // join threads
        //    foreach (var thread in threads) {
        //        thread.join();
        //    }
        //    while (!que.empty()) {
        //        shares += que.get()["instruments"];
        //    }
        //    return shares;
        //}

        //// 
        ////         Get a certain page of shares
        ////         
        //public virtual object get_instruments(object page) {
        //    var headers = this.session.headers;
        //    headers["Authorization"] = "Bearer {self.auth_token}";
        //    var params = new Dictionary<object, object> {
        //        {
        //            "Page",
        //            page},
        //        {
        //            "Sort",
        //            "marketCap"},
        //        {
        //            "PriceChangeTime",
        //            "1y"},
        //        {
        //            "Query",
        //            ""}};
        //    var r = this.session.get("https://data.sharesies.nz/api/v1/instruments", params: params, headers: headers);
        //    var responce = r.json();
        //    // get dividends and price history
        //    foreach (var i in Enumerable.Range(0, responce["instruments"].Count)) {
        //        var id_ = responce["instruments"][i]["id"];
        //        //responce['instruments'][i]['dividends'] = self.get_dividends(id_)
        //        responce["instruments"][i]["priceHistory"] = this.get_price_history(id_);
        //    }
        //    return responce;
        //}

        //// 
        ////         Get certain stocks dividends
        ////         
        //public virtual object get_dividends(object share_id) {
        //    var headers = this.session.headers;
        //    headers["Authorization"] = "Bearer {self.auth_token}";
        //    var r = this.session.get("https://data.sharesies.nz/api/v1/instruments/{share_id}/dividends");
        //    // TODO: Clean up output
        //    return r.json()["dividends"];
        //}

        //// 
        ////         Get certain stocks price history
        ////         
        //public virtual object get_price_history(object share_id) {
        //    var headers = this.session.headers;
        //    headers["Authorization"] = "Bearer {self.auth_token}";
        //    var r = this.session.get("https://data.sharesies.nz/api/v1/instruments/{share_id}/pricehistory");
        //    return r.json()["dayPrices"];
        //}

        //// 
        ////         Returns all companies accessible through Sharesies
        ////         
        //public virtual object get_companies() {
        //    var r = this.session.get("https://app.sharesies.nz/api/fund/list");
        //    var funds = r.json()["funds"];
        //    return (from fund in funds
        //            where fund["fund_type"] == "company"
        //            select fund).ToList();
        //}

        //// 
        ////         Get basic market info
        ////         
        //public virtual object get_info() {
        //    var headers = this.session.headers;
        //    headers["Authorization"] = "Bearer {self.auth_token}";
        //    var r = this.session.get("https://data.sharesies.nz/api/v1/instruments/info");
        //    return r.text;
        //}

        //// 
        ////         Returns the logged in users profile
        ////         
        //public virtual object get_profile() {
        //    var r = this.session.get("https://app.sharesies.nz/api/identity/check");
        //    return r.json();
        //}

        //// 
        ////         Purchase stocks from the NZX Market
        ////         
        //public virtual object buy(object company, object amount) {
        //    this.reauth();
        //    var buy_info = new Dictionary<object, object> {
        //        {
        //            "action",
        //            "place"},
        //        {
        //            "amount",
        //            amount},
        //        {
        //            "fund_id",
        //            company["id"]},
        //        {
        //            "expected_fee",
        //            amount * 0.005},
        //        {
        //            "acting_as_id",
        //            this.user_id}};
        //    var r = this.session.post("https://app.sharesies.nz/api/cart/immediate-buy-v2", json: buy_info);
        //    return r.status_code == 200;
        //}

        //// 
        ////         Sell shares from the NZX Market
        ////         
        //public virtual object sell(object company, object shares) {
        //    this.reauth();
        //    var sell_info = new Dictionary<object, object> {
        //        {
        //            "shares",
        //            shares},
        //        {
        //            "fund_id",
        //            company["fund_id"]},
        //        {
        //            "acting_as_id",
        //            this.user_id}};
        //    var r = this.session.post("https://app.sharesies.nz/api/fund/sell", json: sell_info);
        //    return r.status_code == 200;
        //}

        //// 
        ////         Reauthenticates user on server
        ////         
        //public virtual object reauth() {
        //    var creds = new Dictionary<object, object> {
        //        {
        //            "password",
        //            this.password},
        //        {
        //            "acting_as_id",
        //            this.user_id}};
        //    var r = this.session.post("https://app.sharesies.nz/api/identity/reauthenticate", json: creds);
        //    return r.status_code == 200;
        //}
    }
  
}
