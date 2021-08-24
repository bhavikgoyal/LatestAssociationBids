using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;
namespace AssociationBids.Portal.Service.Base.Code
{
   public class VendorInvoiceService : BaseService , IVendorInvoiceService
    {

        protected IVendorInvoiceRepositery _vendorInvoiceRepositery;


        public VendorInvoiceService()
         : this(new VendorInvoiceRepositery()) { }

        public VendorInvoiceService(string connectionString)
           : this(new VendorInvoiceRepositery(connectionString)) { }

        public VendorInvoiceService(VendorInvoiceRepositery vendorInvoiceRepositery)
        {
            ConnectionString = vendorInvoiceRepositery.ConnectionString;

            _vendorInvoiceRepositery = vendorInvoiceRepositery;
        }

        public List<VendorInvoiceModal> SearchUser(long PageSize, long PageIndex, string Search, string Sort, int CompanyKey)
        {
            return _vendorInvoiceRepositery.SearchUser(PageSize, PageIndex, Search, Sort, CompanyKey);
        }
        public List<VendorInvoiceModal> RefundVendorList(long PageSize, long PageIndex, string Search, string Sort, long ResourceKey = 0)
        {
            return _vendorInvoiceRepositery.RefundVendorList(PageSize, PageIndex, Search, Sort,ResourceKey);
        }
        public IList<VendorInvoiceModal> RefundMailToAdmin(string lookUpTitle, string Title, string Amount,string VendorName)
        {
            return _vendorInvoiceRepositery.RefundMailToAdmin(lookUpTitle, Title, Amount, VendorName);
        }
        public IList<VendorInvoiceModal> RefundMailToVendor(string lookUpTitle, string Title, string Amount, int ResourceKey)
        {
            return _vendorInvoiceRepositery.RefundMailToVendor(lookUpTitle, Title, Amount, ResourceKey);
        }
        public string SendInsertMessage(string Body, string Status, Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string Title)
        {
            return _vendorInvoiceRepositery.SendInsertMessage(Body, Status, ObjectKey, ResourceKey, ModuleKeyName, Title);
        }
        public string SendInsertMessageList(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string UpdatemsgStatus)
        {
            return _vendorInvoiceRepositery.SendInsertMessageList(ObjectKey, ResourceKey, ModuleKeyName, UpdatemsgStatus);
        }
        public List<VendorInvoiceModal> RefundVendorListPriority(long PageSize, long PageIndex, string Search, string Sort, long ResourceKey)
        {
            return _vendorInvoiceRepositery.RefundVendorListPriority(PageSize, PageIndex, Search, Sort, ResourceKey);
        }
        public string MessageNewCount(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, Int64 UserId)
        {
            return _vendorInvoiceRepositery.MessageNewCount(ObjectKey, ResourceKey, ModuleKeyName, UserId);
        }
        public virtual VendorInvoiceModal GetDataViewEdit(int id)
        {
            return _vendorInvoiceRepositery.GetDataViewEdit(id);
        }
        public virtual bool InsertRefundRequest(int Vendorkey, int InvoiceKey, string RefrenceNumber, DateTime date, string Amount, string Reason, string Strip_TokenID,int ResourceKey)
        {
            return _vendorInvoiceRepositery.InsertRefundRequest(Vendorkey, InvoiceKey, RefrenceNumber, date, Amount, Reason, Strip_TokenID, ResourceKey);
        }


        public virtual bool UpdateRefundRequest(int InvoiceKey, string Reason, string StripeTokenID)
        {
            return _vendorInvoiceRepositery.UpdateRefundRequest(InvoiceKey, Reason, StripeTokenID);
        }

        public virtual VendorInvoiceModal CheckRefundRequest(int Vendorkey, int InvoiceKey )
        {
            return _vendorInvoiceRepositery.CheckRefundRequest(Vendorkey, InvoiceKey);
        }

    }
}   
