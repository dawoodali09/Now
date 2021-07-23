using Microsoft.VisualStudio.TestTools.UnitTesting;
using Analyst;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Analyst.Tests
{
    [TestClass()]
    public class AnalystTests
    {
        [TestMethod()]
        public void RefinePriceHistoryNoMissingDateTest()
        {
            var ana = Analyst.GetInstance();
            List<Common.Models.PriceHistory> input = new List<Common.Models.PriceHistory>();
            Common.Models.PriceHistory ph1 = new Common.Models.PriceHistory() { Price = 1.0M, RecordedOn=DateTime.Now.Date.AddDays(-3) } ;
            Common.Models.PriceHistory ph2 = new Common.Models.PriceHistory() { Price = 1.5M, RecordedOn = DateTime.Now.Date.AddDays(-2) };
            Common.Models.PriceHistory ph3 = new Common.Models.PriceHistory() { Price = 2.0M, RecordedOn = DateTime.Now.Date.AddDays(-1) };
            Common.Models.PriceHistory ph4 = new Common.Models.PriceHistory() { Price = 2.0M, RecordedOn = DateTime.Now.Date };
            input.Add(ph1);
            input.Add(ph2);
            input.Add(ph3);
            input.Add(ph4);
            input = input.OrderBy(s => s.RecordedOn).ToList();
            List<Common.Models.PriceHistory> result = ana.RefinePriceHistory(input);
            Assert.AreEqual(input.Count, result.Count);            
        }


        [TestMethod()]
        public void RefinePriceHistoryTwoMissingDateTest()
        {
            var ana = Analyst.GetInstance();
            List<Common.Models.PriceHistory> input = new List<Common.Models.PriceHistory>();
            Common.Models.PriceHistory ph1 = new Common.Models.PriceHistory() { Price = 1.0M, RecordedOn = DateTime.Now.Date.AddDays(-3) };
            Common.Models.PriceHistory ph4 = new Common.Models.PriceHistory() { Price = 2.0M, RecordedOn = DateTime.Now.Date };
            input.Add(ph1);
            input.Add(ph4);
            input = input.OrderBy(s => s.RecordedOn).ToList();
            List<Common.Models.PriceHistory> result = ana.RefinePriceHistory(input);
            Assert.AreEqual(result.Count, 4);
        }


        [TestMethod()]
        public void RefinePriceHistorySingleDateTest()
        {
            var ana = Analyst.GetInstance();
            List<Common.Models.PriceHistory> input = new List<Common.Models.PriceHistory>();            
            Common.Models.PriceHistory ph4 = new Common.Models.PriceHistory() { Price = 2.0M, RecordedOn = DateTime.Now.Date };            
            input.Add(ph4);
            input = input.OrderBy(s => s.RecordedOn).ToList();
            List<Common.Models.PriceHistory> result = ana.RefinePriceHistory(input);
            Assert.AreEqual(result.Count, 1);
        }


        [TestMethod()]
        public void RefinePriceHistorySingleMissingDateTest()
        {
            var ana = Analyst.GetInstance();
            List<Common.Models.PriceHistory> input = new List<Common.Models.PriceHistory>();
            Common.Models.PriceHistory ph1 = new Common.Models.PriceHistory() { Price = 2.0M, RecordedOn = DateTime.Now.Date.AddDays(-2) };
            Common.Models.PriceHistory ph2 = new Common.Models.PriceHistory() { Price = 2.0M, RecordedOn = DateTime.Now.Date };
            input.Add(ph1);
            input.Add(ph2);
            input = input.OrderBy(s => s.RecordedOn).ToList();
            List<Common.Models.PriceHistory> result = ana.RefinePriceHistory(input);
            Assert.AreEqual(result.Count, 3);
        }
    }
}