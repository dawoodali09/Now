using System;
using System.Collections.Generic;
using System.Text;

namespace Now {
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
        public Address(
            Components components,
            string formatted,
            object lat,
            object lng
        ) {
            this.Components = components;
            this.Formatted = formatted;
            this.Lat = lat;
            this.Lng = lng;
        }

        public Components Components { get; }
        public string Formatted { get; }
        public object Lat { get; }
        public object Lng { get; }
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
        public WalletBalances(
            string aud,
            string nzd,
            string usd
        ) {
            this.Aud = aud;
            this.Nzd = nzd;
            this.Usd = usd;
        }

        public string Aud { get; }
        public string Nzd { get; }
        public string Usd { get; }
    }

    public class User {
        public User() {

        }
            public User(
            bool accountFrozen,
            string accountReference,
            bool accountRestricted,
            object accountRestrictedDate,
            Address address,
            object addressRejectReason,
            string addressState,
            Checks checks,
            string email,
            int firstTaxYear,
            HasSeen hasSeen,
            string holdingBalance,
            string homeCurrency,
            string id,
            string intercom,
            string irdNumber,
            bool isDependent,
            bool isOwnerPrescribed,
            string jurisdiction,
            string maximumWithdrawalAmount,
            string minimumWalletBalance,
            object otherPrescribedParticipant,
            List<object> participantEmails,
            string phone,
            object pir,
            string portfolioId,
            string preferredName,
            bool prescribedApproved,
            object prescribedParticipant,
            List<string> recentSearches,
            bool seenFirstTimeAutoinvest,
            bool seenFirstTimeInvestor,
            string state,
            List<object> taxResidencies,
            int taxYear,
            object tfnNumber,
            object transferAge,
            bool transferAgePassed,
            bool usEquitiesEnabled,
            string usTaxTreatyStatus,
            WalletBalances walletBalances
        ) {
            this.AccountFrozen = accountFrozen;
            this.AccountReference = accountReference;
            this.AccountRestricted = accountRestricted;
            this.AccountRestrictedDate = accountRestrictedDate;
            this.Address = address;
            this.AddressRejectReason = addressRejectReason;
            this.AddressState = addressState;
            this.Checks = checks;
            this.Email = email;
            this.FirstTaxYear = firstTaxYear;
            this.HasSeen = hasSeen;
            this.HoldingBalance = holdingBalance;
            this.HomeCurrency = homeCurrency;
            this.Id = id;
            this.Intercom = intercom;
            this.IrdNumber = irdNumber;
            this.IsDependent = isDependent;
            this.IsOwnerPrescribed = isOwnerPrescribed;
            this.Jurisdiction = jurisdiction;
            this.MaximumWithdrawalAmount = maximumWithdrawalAmount;
            this.MinimumWalletBalance = minimumWalletBalance;
            this.OtherPrescribedParticipant = otherPrescribedParticipant;
            this.ParticipantEmails = participantEmails;
            this.Phone = phone;
            this.Pir = pir;
            this.PortfolioId = portfolioId;
            this.PreferredName = preferredName;
            this.PrescribedApproved = prescribedApproved;
            this.PrescribedParticipant = prescribedParticipant;
            this.RecentSearches = recentSearches;
            this.SeenFirstTimeAutoinvest = seenFirstTimeAutoinvest;
            this.SeenFirstTimeInvestor = seenFirstTimeInvestor;
            this.State = state;
            this.TaxResidencies = taxResidencies;
            this.TaxYear = taxYear;
            this.TfnNumber = tfnNumber;
            this.TransferAge = transferAge;
            this.TransferAgePassed = transferAgePassed;
            this.UsEquitiesEnabled = usEquitiesEnabled;
            this.UsTaxTreatyStatus = usTaxTreatyStatus;
            this.WalletBalances = walletBalances;
        }

        public bool AccountFrozen { get; }
        public string AccountReference { get; }
        public bool AccountRestricted { get; }
        public object AccountRestrictedDate { get; }
        public Address Address { get; }
        public object AddressRejectReason { get; }
        public string AddressState { get; }
        public Checks Checks { get; }
        public string Email { get; }
        public int FirstTaxYear { get; }
        public HasSeen HasSeen { get; }
        public string HoldingBalance { get; }
        public string HomeCurrency { get; }
        public string Id { get; }
        public string Intercom { get; }
        public string IrdNumber { get; }
        public bool IsDependent { get; }
        public bool IsOwnerPrescribed { get; }
        public string Jurisdiction { get; }
        public string MaximumWithdrawalAmount { get; }
        public string MinimumWalletBalance { get; }
        public object OtherPrescribedParticipant { get; }
        public IReadOnlyList<object> ParticipantEmails { get; }
        public string Phone { get; }
        public object Pir { get; }
        public string PortfolioId { get; }
        public string PreferredName { get; }
        public bool PrescribedApproved { get; }
        public object PrescribedParticipant { get; }
        public IReadOnlyList<string> RecentSearches { get; }
        public bool SeenFirstTimeAutoinvest { get; }
        public bool SeenFirstTimeInvestor { get; }
        public string State { get; }
        public IReadOnlyList<object> TaxResidencies { get; }
        public int TaxYear { get; }
        public object TfnNumber { get; }
        public object TransferAge { get; }
        public bool TransferAgePassed { get; }
        public bool UsEquitiesEnabled { get; }
        public string UsTaxTreatyStatus { get; }
        public WalletBalances WalletBalances { get; }
    }

    public class UserList {
        public UserList(
            string id,
            string preferredName,
            bool primary,
            string state
        ) {
            this.Id = id;
            this.PreferredName = preferredName;
            this.Primary = primary;
            this.State = state;
        }

        public string Id { get; }
        public string PreferredName { get; }
        public bool Primary { get; }
        public string State { get; }
    }

    public class LoginResponse {
        public LoginResponse(
            bool authenticated,
            bool canEnterAddressToken,
            CanWriteUntil canWriteUntil,
            string distillToken,
            List<string> flags,
            string gaId,
            LiveData liveData,
            bool nzxIsOpen,
            List<object> orders,
            object outstandingSubscription,
            List<string> participants,
            List<object> portfolio,
            object rakaiaToken,
            object rakaiaTokenExpiry,
            string referralCode,
            string type,
            List<object> upcomingDividends,
            User user,
            List<UserList> userList
        ) {
            this.Authenticated = authenticated;
            this.CanEnterAddressToken = canEnterAddressToken;
            this.CanWriteUntil = canWriteUntil;
            this.distill_token = distillToken;
            this.Flags = flags;
            this.GaId = gaId;
            this.LiveData = liveData;
            this.NzxIsOpen = nzxIsOpen;
            this.Orders = orders;
            this.OutstandingSubscription = outstandingSubscription;
            this.Participants = participants;
            this.Portfolio = portfolio;
            this.RakaiaToken = rakaiaToken;
            this.RakaiaTokenExpiry = rakaiaTokenExpiry;
            this.ReferralCode = referralCode;
            this.Type = type;
            this.UpcomingDividends = upcomingDividends;
            this.User = user;
            this.UserList = userList;
        }

        public bool Authenticated { get; }
        public bool CanEnterAddressToken { get; }
        public CanWriteUntil CanWriteUntil { get; }
        public string distill_token { get; }
        public IReadOnlyList<string> Flags { get; }
        public string GaId { get; }
        public LiveData LiveData { get; }
        public bool NzxIsOpen { get; }
        public IReadOnlyList<object> Orders { get; }
        public object OutstandingSubscription { get; }
        public IReadOnlyList<string> Participants { get; }
        public IReadOnlyList<object> Portfolio { get; }
        public object RakaiaToken { get; }
        public object RakaiaTokenExpiry { get; }
        public string ReferralCode { get; }
        public string Type { get; }
        public IReadOnlyList<object> UpcomingDividends { get; }
        public User User { get; }
        public IReadOnlyList<UserList> UserList { get; }
    }

    public class DayPrice{
        public DateTime Day { get; set; }
        public decimal  Price { get; set; }
    }

    public class ResponsePriceHistory{
    public Guid instrumentId { get; set; }
    public DateTime first { get; set; }
    public DateTime last { get; set; }
    public List<DayPrice> dayPrices { get; set; }
    }
}
