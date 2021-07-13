using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Enums;
using Common.Models;

namespace Analyst
{
    public class Analyst
    {
        private Analyst()
        {
        }

        private static Analyst _instance;
        public static Analyst GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Analyst();
            }
            return _instance;
        }

        // buy now  input (market, budget per share) returns shares with rule id
        public List<Recommendation> BuyNow(string exchange, decimal budgetPerShare, DataMode dataMode, string connection) {
            List<Recommendation> result = new List<Recommendation>();

            // get all shares in that market
            // filter shares with budget price 
            //Prepare BigDataList 
            //run rule evaluation 

            RuleEngine rEngine = new RuleEngine();
            var rec = rEngine.Evaluate(new Special.Models.BigData());
            if (rec != null && rec.Recommended)
                result.Add(rec);

                

            return result;
        }
        
        // sell now input shares from portfolio , fee , age) set profit and loss list of shares to sell with rule id
        //TODO....
    }
}
