using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace AssociationBids.Portal.Model
{
    public class VendorInvoiceModal : BaseModel
    {

        public int InvoiceKey { get; set; }
        public int RefundRequestKey { get; set; }
        public int ResourceKey { get; set; }
        public int Memberfeekey { get; set; }
        public bool Status { get; set; }
        public string Reason { get; set; }
        public string ContactPerson { get; set; }
        public string InvoiceNo { get; set; }
        public string vendorname { get; set; }
        public int MarkasRefund { get; set; }   
        public string Date { get; set; }
        public string Amt { get; set; }
        public string Description { get; set; }
        public string BAl { get; set; }
        public string Amount { get; set; }
        public string Title { get; set; }
        public string TotalRecords { get; set; }
        public string Stripe_tokenID { get; set; }
        public string Balance { get; set; }
        public string RefrenceNumber { get; set; }
        public int VendorKey { get; set; }
        public string InvoiceDate { get; set; }
        public string PaymentApplied { get; set; }
        public string DueDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public DateTime TransDate { get; set; }
        public string CompanyName { get; set; }
        public int priority { get; set; }
        public string NotificationId { get; set; }
        public string NotificationType { get; set; }

        public string AmtTotal { get; set; }
        public bool isPriority { get; set; }

        public string RefundAmount { get; set; }
    }
}
