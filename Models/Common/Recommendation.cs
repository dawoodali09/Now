using System;
using System.Collections.Generic;
using System.Text;


namespace Common.Models {
	public class Recommendation {
		public Enums.RuleType Type  { get; set; }
		public Instrument Instrument { get; set; }
		public List<Rule> Rules { get; set; }
		public bool Recommended { get; set; }
	}
}
