using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class StockPriceHistory
    {
        public string Id { get; set; }
        public List<PriceHistory> History { get; set; }

        public StockPriceHistory()
        {
        }
    }
}
