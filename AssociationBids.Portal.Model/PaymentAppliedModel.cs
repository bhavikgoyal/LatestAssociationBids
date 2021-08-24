using System;

namespace AssociationBids.Portal.Model
{
    public class PaymentAppliedModel : BaseModel
    {
        public int PaymentAppliedKey { get; set; }
        public int PaymentKey { get; set; }
        public int InvociceKey { get; set; }
        public Decimal Amount { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}
