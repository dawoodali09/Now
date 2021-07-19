using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums {
	public enum DataMode { SQL = 0, MONGO =1 }
	public enum Machine { MAC = 0, WINDOWS = 1}
	public enum RuleType {  Buying = 0, Selling =1 }
	public enum Rules { PERatio = 0, FiveYearCheck = 1, OneYearCheck = 2, SixMonthCheck = 3, ThreeMonthCheck = 4, OneWeekCheck = 5 }
}
