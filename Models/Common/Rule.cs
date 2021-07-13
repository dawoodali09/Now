using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models {
	public class Rule {
		public int Id { get; set; }
		public Guid UUID { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Reason { get; set; }
		public string Message { get; set; }
		public string Description { get; set; }
		public int  SupportPoint {get;set;}
		public int SupportPointGiven { get; set; }
		public bool IsValid { get; set; }
	}
}
