using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class APIService : BaseService, IAPIService
    {
        protected IAPIRepositery __IAPIservice;


        public APIService()
           : this(new APIRepositery()) { }

        public APIService(string connectionString)
           : this(new APIRepositery(connectionString)) { }

        public APIService(APIRepositery apiservice)
        {
            ConnectionString = apiservice.ConnectionString;

            __IAPIservice = apiservice;
        }

        public IList<APIModel> SendMailToBidDueRemiders(string lookUpTitle)
        {
            return __IAPIservice.SendMailToBidDueRemiders(lookUpTitle);
        }
        public IList<APIModel> SendMailToBidSubmissionReminderDue(string lookUpTitle)
        {
            return __IAPIservice.SendMailToBidSubmissionReminderDue(lookUpTitle);
        }
        public IList<APIModel> CardExpiremail(string lookUpTitle)
        {
            return __IAPIservice.CardExpiremail(lookUpTitle);
        }
        
        public IList<ReportEmailModel> GetReportList()
        {
            return __IAPIservice.GetReportList();
        }
        public bool BidAbdWorkOrderEmailSent(ReportEmailModel reportEmail)
        {
            return __IAPIservice.BidAbdWorkOrderEmailSent(reportEmail);
        }
        public IList<APIModel> GetBidRequestListPassedResponsedate()
        {
            return __IAPIservice.GetBidRequestListPassedResponsedate();
        }

        public IList<APIModel> InsaurenceRenawalmail(string lookUpTitle)
        {
            return __IAPIservice.InsaurenceRenawalmail(lookUpTitle);
        }
        public IList<APIModel> InsaurenceExpiredmail(string lookUpTitle)
        {
            return __IAPIservice.InsaurenceExpiredmail(lookUpTitle);
        }

        public IList<APIModel> MembershipRenewalMail(string lookUpTitle)
        {
            return __IAPIservice.MembershipRenewalMail(lookUpTitle);
        }

        public IList<APIModel> MembershipExpired(string lookUpTitle)
        {
            return __IAPIservice.MembershipRenewalMail(lookUpTitle);
        }

        public IList<APIModel> InvitemailToVendor(string lookUpTitle)
        {
            return __IAPIservice.MembershipRenewalMail(lookUpTitle);
        }


        public IList<APIModel> BidFinePayment(string lookUpTitle)
        {
            return __IAPIservice.BidFinePayment(lookUpTitle);
        }

        public IList<APIModel> WinFeechargeMailAsync(string lookUpTitle)
        {
            return __IAPIservice.WinFeechargeMailAsync(lookUpTitle);
        }

        public IList<APIModel> MembershipFees(string lookUpTitle)
        {
            return __IAPIservice.MembershipFees(lookUpTitle);
        }
        public virtual bool PaymentmodalInsert(string CVV, string StripToken)
        {
            return __IAPIservice.PaymentmodalInsert(CVV, StripToken);
        }
        public virtual bool insertBidVendorForBid(int BidRequestKey)
        {
            return __IAPIservice.insertBidVendorForBid(BidRequestKey);
        }
        public virtual bool Insertpaymet(string RefrenceNumber, int Amount, int vendorKey, int PricingTypeKey, string stripeToken)
        {
            return __IAPIservice.Insertpaymet(RefrenceNumber, Amount, vendorKey, PricingTypeKey, stripeToken);
        }
        public virtual bool MembershipInsert(int VendorKey)
        {
            return __IAPIservice.MembershipInsert(VendorKey);
        }
        public virtual bool Updatemembership(int VendorKey)
        {
            return __IAPIservice.Updatemembership(VendorKey);
        }
        public virtual bool Insertpaymetfail(string RefrenceNumber, int Amount, int vendorKey, int PricingTypeKey, string stripeToken)
        {
            return __IAPIservice.Insertpaymetfail(RefrenceNumber, Amount, vendorKey, PricingTypeKey, stripeToken);
        }
        public virtual bool DowmloadInvoice(string imagepath, string destinationPath, int Vendorkey, string fromemail, string UserName, string Title, int Amount, string addressline1, string addressline2, string zip, string City, string state)
        {
            return __IAPIservice.DowmloadInvoice(imagepath, destinationPath, Vendorkey, fromemail, UserName, Title, Amount, addressline1, addressline2, zip, City, state);
        }



        public virtual bool WinFeeMain(string fromemail, string UserName, int Amount, string Title, string lookUpTitle)
        {
            return __IAPIservice.WinFeeMain(fromemail, UserName, Amount, Title, lookUpTitle);
        }

        public virtual bool UpdateVendorStatus( int BidVendorKey)
        {
            return __IAPIservice.UpdateVendorStatus(BidVendorKey);
        }


        public virtual bool Invitemail(string fromemail, string Title, string PropertyTitle, string VendorName, string CompanyName, string BidDueDate, string lookUpTitle)
        {
            return __IAPIservice.Invitemail(fromemail, Title, PropertyTitle, VendorName, CompanyName, BidDueDate,lookUpTitle);
        }

        public Int64 InsertIntoErrorLog(string Remarks) 
        {
            return __IAPIservice.InsertIntoErrorLog(Remarks);
        }



        public virtual APIModel GetDataforMembershipPayment(int id)
        {
            return __IAPIservice.GetDataforMembershipPayment(id);
        }
        public virtual APIModel GetdataWinfeechargePaymentAccpectBid(int id, string BidVendorkey)
        {
            return __IAPIservice.GetdataWinfeechargePaymentAccpectBid(id, BidVendorkey);
        }

        public virtual IList<VendorModel> GetbindDocument12(int CompanyKey)
        {
            return __IAPIservice.GetbindDocument12(CompanyKey);
        }

    }
}