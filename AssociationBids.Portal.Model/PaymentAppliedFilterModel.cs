using System;

namespace AssociationBids.Portal.Model
{
    public class PaymentAppliedFilterModel : BaseFilterModel
    {
        public int PaymentKey { get; set; }
        public int InvociceKey { get; set; }
    }
}
