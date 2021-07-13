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
			Recommendation rec = new Recommendation();
			
			return rec;
		}

		private Rule EvaluateRule(BigData bigData , Rule rule){
			Rule Result = new Rule();


			return Result;				
		}
	}
}
