using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base.Interface
{
    public interface IPMVendorRepository : IBaseRepository
    {
        List<VendorModel> SearchVendor(Int64 PageSize, Int64 PageIndex, string Search, String Sort, int resourcekey, string service, string checkstar, string Invited,string Duplicate);
        List<VendorModel> SearchBidrequest(Int64 PageSize, Int64 PageIndex, String Sort, string CompanyKey);
        List<VendorModel> SearchWorkOrder(Int64 PageSize, Int64 PageIndex, String Sort, string CompanyKey);
        List<VendorModel> SearchFeedbackvendor(Int64 PageSize, Int64 PageIndex, String Sort,string CompanyKey);
        List<VendorModel> Searchinsurance(int CompanyKey);
        List<VendorModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey);
        List<VendorModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey, int resourcekey);

        VendorModel GetBidDataForProperties(int BidRequestKey, int CompanyKey);
        IList<VendorModel> GetAll(VendorFilterModel propertyFilterModel, PagingModel paging);

        IList<VendorModel> GetAllState();

        IList<VendorModel> GetAll(VendorFilterModel filter);

        Int64 Insert(VendorModel vendorModel, int ResourceKey);

        VendorModel GetDataViewEdit(int id);

        IList<VendorModel> Getbindservice(int CompanyKey);

        IList<VendorModel> GetbindDocument(int CompanyKey, int ModuleKey);
        IList<VendorModel> GetbindDocument1(int CompanyKey, int ModuleKey);

        IList<VendorModel> GetbindDocumentByCompanyKey(int CompanyKey);

        IList<VendorModel> GetAllService();

        IList<VendorModel> GetAllProperty(int resourcekey);

        Int64 VendorEdit(VendorModel item);
        bool CheckDuplicatedEmail(string Email);

        bool insuranceEdit(VendorModel item);

        bool Remove(int CompanyKey);
        bool RemoveService(int CompanyKey,string servicename);
        bool RemoveDocument(int CompanyKey, int docId);
        bool MarkstarOrNot(int CompanyKey, int Resourcekey);

        IList<VendorModel> GetbindDocument12(int CompanyKey);
        IList<VendorModel> GetbindDocumentByInsuranceKey(int CompanyKey,int InsuranceKey);
    }
}
