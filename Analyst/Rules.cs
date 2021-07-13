 
using SQLDataAccess.Models;
using System;
using System.Linq;

namespace Analyst {
	//public abstract class Rules {
	//	public int DecisionPoints { get; set; }
	//	public abstract int GetDecisionPoints(int stockId);
	//}

	//public class BuyingRules : Rules, IBuyingRules {
	//	public override int GetDecisionPoints(int stockId) {
	//		DecisionPoints = 0;

	//		//Instrument ins = new Instrument();
	//		//NowDBContext con = new NowDBContext();
	//		//if (!con.Instruments.Any(s => s.Id == stockId))
	//		//	return -1;
	//		//else
	//		//	ins = con.Instruments.Where(s => s.Id == stockId).FirstOrDefault();


	//		//DecisionPoints += ins.Peratio.HasValue ? PERationRule(ins.Peratio.Value) : PERationRule(0);

	//		return DecisionPoints;
	//	}

	//	public int PERationRule(decimal peRatio) {
	//		int result = 0;
	//		if (peRatio > 1)
	//			result = 1;
	//		else if (peRatio < 0)
	//			result = -1;
	//		return result;
	//	}
	//}

	//public class SellingRules : Rules {
	//	public override int GetDecisionPoints(int stockId) {
	//		throw new NotImplementedException();
	//	}
	//}
}
