using System;

namespace AssociationBids.Portal.Model
{
    public class InvoiceLineModel : BaseModel
    {
        public int InvoiceLineKey { get; set; }
        public int InvoiceKey { get; set; }
        public int Quantity { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Amount { get; set; }
        public string Description { get; set; }
        public double SortOrder { get; set; }
    }
}
