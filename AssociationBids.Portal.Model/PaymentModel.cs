using System;
using System.ComponentModel.DataAnnotations;

namespace AssociationBids.Portal.Model
{
    public class PaymentModel : BaseModel
    {

        [Key]
        public Guid PaymentModelID { get; set; }
        public string CardNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string CVC { get; set; }
        public int Value { get; set; }

        public int PaymentKey { get; set; }
        public int VendorKey { get; set; }
        public int PaymentTypeKey { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Balance { get; set; }
        public string Description { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}
