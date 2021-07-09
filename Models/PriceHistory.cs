using System;
using System.Collections.Generic;
using System.Text;

namespace Models {
	public class PriceHistory {
	public string Id { get; set; }
	public decimal Price { get; set; }
	public DateTime RecordedOn { get; set; }
	}
}
