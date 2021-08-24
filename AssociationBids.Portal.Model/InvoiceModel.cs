using System;

namespace AssociationBids.Portal.Model
{
    public class InvoiceModel : BaseModel
    {
        public int InvoiceKey { get; set; }
        public int VendorKey { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime DueDate { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Balance { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int Status { get; set; }
    }
}
