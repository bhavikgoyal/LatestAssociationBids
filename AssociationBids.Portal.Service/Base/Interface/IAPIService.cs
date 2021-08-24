using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface IAPIService : IBaseService
    {
        IList<APIModel> SendMailToBidDueRemiders(string lookUpTitle);
        IList<APIModel> SendMailToBidSubmissionReminderDue(string lookUpTitle);
        IList<APIModel> CardExpiremail(string lookUpTitle);
        
        IList<ReportEmailModel> GetReportList();
        bool BidAbdWorkOrderEmailSent(ReportEmailModel reportEmail);
        IList<APIModel> GetBidRequestListPassedResponsedate();
        IList<APIModel> InsaurenceRenawalmail(string lookUpTitle);
        IList<APIModel> InsaurenceExpiredmail(string lookUpTitle);
        IList<APIModel> MembershipRenewalMail(string lookUpTitle);
        IList<APIModel> MembershipExpired(string lookUpTitle);
        IList<APIModel> WinFeechargeMailAsync(string lookUpTitle);
        IList<APIModel> InvitemailToVendor(string lookUpTitle);
        IList<APIModel> MembershipFees(string lookUpTitle);
        IList<APIModel> BidFinePayment(string lookUpTitle);
        bool MembershipInsert(int VendorKey);

        bool insertBidVendorForBid(int BidRequestKey);
        bool Updatemembership(int VendorKey);
        bool DowmloadInvoice(string imagepath, string destinationPath, int Vendorkey, string fromemail, string UserName, string Title, int Amount, string addressline1, string addressline2, string zip, string City, string state);
        APIModel GetDataforMembershipPayment(int id);
        APIModel GetdataWinfeechargePaymentAccpectBid(int id, string BidVendorkey);
        bool PaymentmodalInsert(string CVV, string StripToken);
        bool UpdateVendorStatus(int  BidVendorKey);
        bool Insertpaymet(string RefrenceNumber, int Amount, int vendorKey, int PricingTypeKey, string stripeToken);
        bool WinFeeMain(string fromemail, string UserName, int Amount, string Title, string lookUpTitle);
        bool Invitemail(string fromemail, string Title, string PropertyTitle, string VendorName, string CompanyName, string BidDueDate,  string lookUpTitle);
        bool Insertpaymetfail(string RefrenceNumber, int Amount, int vendorKey, int PricingTypeKey, string stripeToken);
        Int64 InsertIntoErrorLog(string Remarks);
        IList<VendorModel> GetbindDocument12(int CompanyKey);
    }
    
}

