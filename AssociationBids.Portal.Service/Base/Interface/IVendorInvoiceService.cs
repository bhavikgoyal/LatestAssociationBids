using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;
namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface IVendorInvoiceService : IBaseService
   
    {
        string SendInsertMessage(string Body, string Status, Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string Title);
        string SendInsertMessageList(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string UpdatemsgStatus);
        string MessageNewCount(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, Int64 UserId);
        VendorInvoiceModal GetDataViewEdit(int id);
        List<VendorInvoiceModal> SearchUser(Int64 PageSize, Int64 PageIndex, string Search, String Sort, int CompanyKey);
        List<VendorInvoiceModal> RefundVendorList(Int64 PageSize, Int64 PageIndex, string Search, String Sort, long ResourceKey = 0);

        List<VendorInvoiceModal> RefundVendorListPriority(long PageSize, long PageIndex, string Search, string Sort, long ResourceKey);

        bool UpdateRefundRequest(int InvoiceKey, string Reason,string StripeTokenID);
        VendorInvoiceModal CheckRefundRequest(int Vendorkey, int InvoiceKey);

        bool InsertRefundRequest(int Vendorkey, int InvoiceKey, string RefrenceNumber, DateTime date ,  string Amount, string Reason, string Strip_TokenID,int ResourceKey);
        IList<VendorInvoiceModal> RefundMailToAdmin(string lookUpTitle,string Title, string Amount,string VendorName);
        IList<VendorInvoiceModal> RefundMailToVendor(string lookUpTitle, string Title, string Amount, int ResourceKey);
    }
}
