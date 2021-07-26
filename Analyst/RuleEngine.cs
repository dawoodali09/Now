using System;
using System.Collections.Generic;
using System.Text;
using Common.Models;
using Special.Models;

namespace Analyst {
	public class RuleEngine {
		private List<Common.Models.Rule> RulesToEvaluate { get; set; }
		public BigData input { get; set; }

		public Recommendation Evaluate(BigData bigData) {
			Recommendation rec = new Recommendation(bigData.Instrument, Common.Enums.RuleType.Buying);

			foreach (Common.Enums.Rules rule in Enum.GetValues(typeof(Common.Enums.Rules))) {
				var ruleResults = EvaluateRule(bigData, rule);
				if (ruleResults.IsValid)
					rec.Rules.Add(ruleResults);
			}

			return rec;
		}

		//each rule will have 2 points 0 for unsattisfactory, 1 for condition met, 2 for very optimistic value
		private Rule EvaluateRule(BigData bigData, Common.Enums.Rules rule) {
			Rule Result = new Rule() { IsValid = false, Type = Common.Enums.RuleType.Buying.ToString(), UUID = Guid.NewGuid(), SupportPoint = 2, SupportPointGiven = 0 };

			switch (rule) {
				case Common.Enums.Rules.PERatio: {
						Result.Description = "PE Ratio Rule";
						Result.Id = 1;
						if (!string.IsNullOrEmpty(bigData.Instrument.PeRatio)) {
							if (decimal.Parse(bigData.Instrument.PeRatio) > 1) {
								Result.IsValid = true;
								Result.Message = "Positive PE Ratio";
								Result.Reason = "Positive PE Ratio";
								Result.SupportPointGiven = (decimal.Parse(bigData.Instrument.PeRatio) > 10) ? 2 : 1;
							}
						}
						break;
					};
				case Common.Enums.Rules.FiveYearCheck: {
						Result.Description = "Five year check";
						Result.Id = 2;
						// current price shud be lower then five year max,
						// current price shud be higher then five year min
						// five year percentage shoud be in positive 
						// current value should be higher then two quad

						//if current value is higher then three quad then assign full points.
						if (bigData.Instrument.ComparisonPrices != null && bigData.Instrument.ComparisonPrices.FiveYear != null) {
							if (bigData.Instrument.MarketPrice < decimal.Parse(bigData.Instrument.ComparisonPrices.FiveYear.Max) && bigData.Instrument.MarketPrice > decimal.Parse(bigData.Instrument.ComparisonPrices.FiveYear.Min) && decimal.Parse(bigData.Instrument.ComparisonPrices.FiveYear.Percent) > 0) {
								decimal singleQuadValue = (decimal.Parse(bigData.Instrument.ComparisonPrices.FiveYear.Max) - decimal.Parse(bigData.Instrument.ComparisonPrices.FiveYear.Min)) / 4;
								if (bigData.Instrument.MarketPrice > (2 * singleQuadValue)) {
									Result.IsValid = true;
									Result.Message = "Qualifying on five year data values";
									Result.Reason = "Qualifying on five year data values";
									Result.SupportPointGiven = 1;
									if (bigData.Instrument.MarketPrice > (3 * singleQuadValue)) {
										Result.SupportPointGiven = 2;
									}
								}
							}
						}
						break;
					};

				default: break;
			}
		

			return Result;
		}
	}
}
