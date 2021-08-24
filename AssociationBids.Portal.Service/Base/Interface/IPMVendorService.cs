using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface IPMVendorService : IBaseService
    {
        bool Validate(VendorModel item);

        bool IsFilterEnabled(VendorFilterModel filter);
       
        VendorFilterModel CreateFilter();

        VendorModel Get(int id);

        IList<VendorModel> GetAll();

        Int64 Insert(VendorModel vendorModel, int ResourceKey);

        IList<VendorModel> GetAll(VendorFilterModel filter);
        List<VendorModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey);
        List<VendorModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey, int resourcekey);

        VendorModel GetBidDataForProperties(int BidRequestKey, int CompanyKey);
        VendorModel GetDataViewEdit(int id);
        List<VendorModel> SearchVendor(Int64 PageSize, Int64 PageIndex, string Search, String Sort, int ResourceKey, string service, string checkstar, string Invited,string Duplicate);
        List<VendorModel> SearchBidrequest(Int64 PageSize, Int64 PageIndex, String Sort, string CompanyKey);
        List<VendorModel> SearchWorkOrder(Int64 PageSize, Int64 PageIndex, String Sort, string CompanyKey);
        List<VendorModel> SearchFeedbackvendor(Int64 PageSize, Int64 PageIndex, String Sort, string CompanyKey);
        List<VendorModel> Searchinsurance(int CompanyKey);

        IList<VendorModel> GetAllState();

        IList<VendorModel> GetAllService();

        IList<VendorModel> GetAllProperty(int ResourceKey);

        Int64 VendorEdit(VendorModel item);

        bool insuranceEdit(VendorModel item);

        IList<VendorModel> Getbindservice(int CompanyKey);

        IList<VendorModel> GetbindDocument(int CompanyKey, int ModuleKey);
        IList<VendorModel> GetbindDocument1(int CompanyKey, int ModuleKey);

        IList<VendorModel> GetbindDocumentByCompanyKey(int CompanyKey);

        bool CheckDuplicatedEmail(string Email);

        bool Remove(int CompanyKey);
        bool RemoveService(int CompanyKey, string servicename);
        bool RemoveDocument(int CompanyKey, int docId);
        bool MarkstarOrNot(int CompanyKey, int Resourcekey);
        IList<VendorModel> GetbindDocument12(int CompanyKey);
        IList<VendorModel> GetbindDocumentByInsuranceKey(int CompanyKey, int InsuranceKey);
    }
}
