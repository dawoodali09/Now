using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Enums;
using Common.Models;
using Special.Models;

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

            List<Instrument> filteredInstruments = new List<Instrument>();
            // get all shares in that market
            if(dataMode == DataMode.MONGO)
            {
                filteredInstruments = MongoAccess.DataMethods.Methods.ListInstrumentsByMarket(exchange,budgetPerShare, connection);
                
            }
            else if(dataMode == DataMode.SQL)
            {
                //todo....
            }

            // filter shares with budget price and non zero price shares
            filteredInstruments = filteredInstruments.Where(s => s.MarketPrice <= budgetPerShare && s.MarketPrice > 0).ToList();

            //Prepare BigDataList
            List<BigData> bigData = GetBigData(filteredInstruments, dataMode, connection);

            //run rule evaluation 

            RuleEngine rEngine = new RuleEngine();
            foreach(var bd in bigData)
            {
                var rec = rEngine.Evaluate(bd);
                if (rec != null && rec.Recommended)
                    result.Add(rec);
            }

            return result;
        }

        List<BigData> GetBigData(List<Instrument> instruments, DataMode dataMode, string connection)
        {
            List<BigData> result = new List<BigData>();
            if (dataMode == DataMode.MONGO) {
                foreach (var ins in instruments)
                {
                    BigData bd = new BigData()
                    {
                        Instrument = ins,
                        PriceHistory = MongoAccess.DataMethods.Methods.GetStockPriceHistory(ins.Id, connection).History.ToList()
                    };
                    // fillin the missing price history
                    // populate attributes
                    result.Add(bd);
                }
            }
            else
            {

                // todo
            }

            return result;

        }
        
        // sell now input shares from portfolio , fee , age) set profit and loss list of shares to sell with rule id
        //TODO....
    }
}
