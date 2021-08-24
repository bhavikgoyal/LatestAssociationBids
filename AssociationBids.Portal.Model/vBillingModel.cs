using System;


namespace AssociationBids.Portal.Model
{
    public class vBillingModel : BaseModel
    {
        public int PaymentMethodKey { get; set; }
        public int CompanyKey { get; set; }
        public string CardHolderFirstName { get; set; }
        public string CardHolderLastName { get; set; }
        public string MaskedCCNumber { get; set; }
        public string StripeTokenID { get; set; }
        public DateTime AddedOn { get; set; }
        public int AddedByResourceKey { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int Status { get; set; }
        public bool PrimaryMethod { get; set; }
        public string ValidTillMM { get; set; }        
        public string ValidTillYY { get; set; }
        public string CVV { get; set; }

        public string PaymentMethodId { get; set; }

        public int priority { get; set; }
        public string NotificationId { get; set; }
        public string NotificationType { get; set; }
    }
}
