using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class SharePriceHistory
    {
        public string Id { get; set; }
        public List<PriceHistory> History { get; set; }

        public SharePriceHistory()
        {
        }
    }
}
