using System;
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
        // sell now input shares from portfolio , fee , age) set profit and loss list of shares to sell with rule id

    }
}
