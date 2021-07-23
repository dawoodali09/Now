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
                filteredInstruments = SQLDataAccess.DataMethods.Methods.ListInstrumentsByMarket(exchange, budgetPerShare, connection);
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

        List<BigData> GetBigData(List<Instrument> instruments, DataMode dataMode, string connection) {
            List<BigData> result = new List<BigData>();

            foreach (var ins in instruments) {

                List<PriceHistory> ph = new List<PriceHistory>();
                if (dataMode == DataMode.SQL) {
                    ph = SQLDataAccess.DataMethods.Methods.GetStockPriceHistory(int.Parse(ins.Id), connection).History.ToList();
                } else if (dataMode == DataMode.MONGO){
                    ph = MongoAccess.DataMethods.Methods.GetStockPriceHistory(ins.Id, connection).History.ToList();
                }

                BigData bd = new BigData() { Instrument = ins, PriceHistory = RefinePriceHistory(ph) };

                // populate attributes                  
                result.Add(bd);
            }

            return result;
        }
        
        // removing redundant data and filling in the gap 
        public List<Common.Models.PriceHistory> RefinePriceHistory(List<PriceHistory> hist) {
            List<Common.Models.PriceHistory> priceHistories = new List<Common.Models.PriceHistory>();
            int index = 0;
            foreach(var ph in hist.OrderBy(s=> s.RecordedOn).GroupBy(s=> s.RecordedOn.Date).Select(f=> f.First()).ToList()) {
                if(!priceHistories.Where(s=> s.RecordedOn.Date == ph.RecordedOn.Date).Any()){
                    // first entry ad it as it is or // successive day
                    if (index == 0 || (priceHistories[index - 1].RecordedOn.Date == ph.RecordedOn.Date.AddDays(-1))){
                        priceHistories.Add(ph);
                        index++;
                    }                    
                    else
                    {
                        // fill in the avg 
                        decimal avg = (priceHistories[index - 1].Price + ph.Price) / 2;
                        decimal toAddatEnd = ph.Price;
                        DateTime dateatEnd = ph.RecordedOn.Date;

                        for (int i=0; i <= (ph.RecordedOn.Date - priceHistories[index-1].RecordedOn.Date).TotalDays; i++)
                        {
                            DateTime dateToAssign = priceHistories[index - 1].RecordedOn.Date.AddDays(1);
                            if (dateToAssign.Date != dateatEnd.Date)
                            {
                                priceHistories.Add(new PriceHistory() { Price = avg, RecordedOn = dateToAssign });
                                index++;
                            }
                        }

                        priceHistories.Add(new PriceHistory() { Price = toAddatEnd, RecordedOn = dateatEnd });
                        index++;
                    }
                }

            }

            return priceHistories;
         }
    }
}
