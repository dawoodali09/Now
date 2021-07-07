using System;
using System.Collections.Generic;

namespace Common.Models {
	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
	public class CanWriteUntil {
		public CanWriteUntil(long quantum) {
			this.Quantum = quantum;
		}
		public long Quantum { get; }
	}

	public class LiveData {
		public LiveData(
			bool eligibleForFreeMonth,
			bool isActive
		) {
			this.EligibleForFreeMonth = eligibleForFreeMonth;
			this.IsActive = isActive;
		}

		public bool EligibleForFreeMonth { get; }
		public bool IsActive { get; }
	}

	public class Components {
		public Components(
			string locality,
			string postalCode,
			string route,
			string streetNumber,
			string sublocality
		) {
			this.Locality = locality;
			this.PostalCode = postalCode;
			this.Route = route;
			this.StreetNumber = streetNumber;
			this.Sublocality = sublocality;
		}

		public string Locality { get; }
		public string PostalCode { get; }
		public string Route { get; }
		public string StreetNumber { get; }
		public string Sublocality { get; }
	}

	public class Address {
		public Components Components { get; set; }
		public string Formatted { get; set; }
		public object Lat { get; set; }
		public object Lng { get; set; }
	}

	public class Checks {
		public Checks(
			bool addressEntered,
			bool addressVerified,
			bool dependentDeclaration,
			bool idVerified,
			bool madeDeposit,
			bool prescribedAnswered,
			bool taxQuestions,
			bool tcAccepted
		) {
			this.AddressEntered = addressEntered;
			this.AddressVerified = addressVerified;
			this.DependentDeclaration = dependentDeclaration;
			this.IdVerified = idVerified;
			this.MadeDeposit = madeDeposit;
			this.PrescribedAnswered = prescribedAnswered;
			this.TaxQuestions = taxQuestions;
			this.TcAccepted = tcAccepted;
		}

		public bool AddressEntered { get; }
		public bool AddressVerified { get; }
		public bool DependentDeclaration { get; }
		public bool IdVerified { get; }
		public bool MadeDeposit { get; }
		public bool PrescribedAnswered { get; }
		public bool TaxQuestions { get; }
		public bool TcAccepted { get; }
	}

	public class HasSeen {
		public HasSeen(
			bool auSharesIntro,
			bool autoinvest,
			bool companies,
			bool exchangeInvestor,
			bool funds,
			bool investor,
			bool limitOrders,
			bool managedFundsInvestor,
			bool showAuCurrency
		) {
			this.AuSharesIntro = auSharesIntro;
			this.Autoinvest = autoinvest;
			this.Companies = companies;
			this.ExchangeInvestor = exchangeInvestor;
			this.Funds = funds;
			this.Investor = investor;
			this.LimitOrders = limitOrders;
			this.ManagedFundsInvestor = managedFundsInvestor;
			this.ShowAuCurrency = showAuCurrency;
		}

		public bool AuSharesIntro { get; }
		public bool Autoinvest { get; }
		public bool Companies { get; }
		public bool ExchangeInvestor { get; }
		public bool Funds { get; }
		public bool Investor { get; }
		public bool LimitOrders { get; }
		public bool ManagedFundsInvestor { get; }
		public bool ShowAuCurrency { get; }
	}

	public class WalletBalances {
		public string Aud { get; set; }
		public string Nzd { get; set; }
		public string Usd { get; set; }
	}

	public class User {
		public bool AccountFrozen { get; set; }
		public string AccountReference { get; set; }
		public bool AccountRestricted { get; set; }
		public object AccountRestrictedDate { get; set; }
		public Address Address { get; set; }
		public object AddressRejectReason { get; set; }
		public string AddressState { get; set; }
		public Checks Checks { get; set; }
		public string Email { get; set; }
		public int FirstTaxYear { get; set; }
		public HasSeen HasSeen { get; set; }
		public string HoldingBalance { get; set; }
		public string HomeCurrency { get; set; }
		public string Id { get; set; }
		public string Intercom { get; set; }
		public string IrdNumber { get; set; }
		public bool IsDependent { get; set; }
		public bool IsOwnerPrescribed { get; set; }
		public string Jurisdiction { get; set; }
		public string MaximumWithdrawalAmount { get; set; }
		public bool MfaEnabled { get; set; }
		public string MinimumWalletBalance { get; set; }
		public object OtherPrescribedParticipant { get; set; }
		public List<object> ParticipantEmails { get; set; }
		public string Phone { get; set; }
		public object Pir { get; set; }
		public string PortfolioId { get; set; }
		public string PreferredName { get; set; }
		public bool PrescribedApproved { get; set; }
		public object PrescribedParticipant { get; set; }
		public List<string> RecentSearches { get; set; }
		public bool SeenFirstTimeAutoinvest { get; set; }
		public bool SeenFirstTimeInvestor { get; set; }
		public string State { get; set; }
		public List<object> TaxResidencies { get; set; }
		public int TaxYear { get; set; }
		public object TfnNumber { get; set; }
		public object TransferAge { get; set; }
		public bool TransferAgePassed { get; set; }
		public bool UsEquitiesEnabled { get; set; }
		public string UsTaxTreatyStatus { get; set; }
		public WalletBalances WalletBalances { get; set; }
		public List<object> Watchlist { get; set; }
	}


	public class UserList {
		public string Id { get; set; }
		public string PreferredName { get; set; }
		public bool Primary { get; set; }
		public string State { get; set; }
	}

	public class LoginResponse {
		public bool Authenticated { get; set; }
		public bool CanEnterAddressToken { get; set; }
		public CanWriteUntil CanWriteUntil { get; set; }
		public string DistillToken { get; set; }
		public List<string> Flags { get; set; }
		public string GaId { get; set; }
		public LiveData LiveData { get; set; }
		public bool NzxIsOpen { get; set; }
		public List<object> Orders { get; set; }
		public object OutstandingSubscription { get; set; }
		public List<string> Participants { get; set; }
		public List<object> Portfolio { get; set; }
		public object RakaiaToken { get; set; }
		public object RakaiaTokenExpiry { get; set; }
		public string ReferralCode { get; set; }
		public string Type { get; set; }
		public List<object> UpcomingDividends { get; set; }
		public User User { get; set; }
		public List<UserList> UserList { get; set; }
	}

	public class DayPrice {
		public DateTime Day { get; set; }
		public decimal Price { get; set; }
	}

	public class PriceHistoryResponse {
		public Guid instrumentId { get; set; }
		public DateTime first { get; set; }
		public DateTime last { get; set; }
		public List<DayPrice> dayPrices { get; set; }
	}
}
