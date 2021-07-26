using System;
using System.Collections.Generic;
using System.Text;


namespace Common.Models {
	public class Recommendation {
		public Recommendation(Instrument instrument, Enums.RuleType type) {
			this.Instrument = instrument;
			this.Type = type;
			this.Rules = new List<Rule>();
			this.Recommended = false;
		}
		public Enums.RuleType Type { get; set; }
		public Instrument Instrument { get; set; }
		public List<Rule> Rules { get; set; }
		public bool Recommended { get; set; }
	}
}
