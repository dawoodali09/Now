using System;
using System.Collections.Generic;
using System.Text;
using Common.Models;

namespace Special.Models {
	public class BigData {
		public Instrument Instrument { get; set; }
		public List<PriceHistory> PriceHistory { get; set; } // we need to make sure all dates are present and remove the redundant dates 
		private List<DataAttribute> Attributes; // { get; set; }

		public void AddDataAttribute(string name, string value)
		{
			DataAttribute da = new DataAttribute() { Name = name, Value = value };
			if (this.Attributes == null)
				this.Attributes = new  List<DataAttribute>();
			this.Attributes.Add(da);
		}

	}
}
