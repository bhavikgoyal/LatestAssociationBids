using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface IPMPropertiesService : IBaseService
    {
        bool Validate(PropertyModel item);
        bool IsFilterEnabled(PropertyFilterModel filter);
        PropertyFilterModel CreateFilter();
        PropertyModel Get(int id);
        IList<PropertyModel> GetAll();
        IList<PropertyModel> GetAll(PropertyFilterModel filter);
        IList<PropertyModel> GetAll(PropertyFilterModel filter, PagingModel paging);
        //Int64 Insert(PropertyModel item, string strinbuilder, string strinbuilder1);
        Int64 Insert(PropertyModel item, string strinbuilder, string strinbuilder1);
        bool DocInsert(int PropertyKey, string strinbuilder, string strinbuilder1);
        int GetGroupKey(string managername);
        bool Updatemanager(PropertyModel item, int PropertyKey, string managername);
        IList<PropertyModel> GetAllManager(int ResourceId, int PropertyKey, int CompnyKey);
        IList<PropertyModel> GetManagerForAdd(int ResourceId, int PropertyKey, int CompnyKey);
        IList<PropertyModel> GetbindDocument(int PropertyKey);
        IList<BidRequestModel> GetbindDocumentBid(int CompanyKey, int ModuleKey);
        IList<PropertyModel> GetAllCompany();
        IList<PropertyModel> GetAllState();
     
        List<PropertyModel> SearchUser(Int64 PageSize, Int64 PageIndex, string Search, String Sort, int ResourceKey);
        List<PropertyModel> SearchBidrequest(Int64 PageSize, Int64 PageIndex, String Sort, string PropertyKey);
        List<PropertyModel> SearchWorkOrder(Int64 PageSize, Int64 PageIndex, String Sort, string PropertyKey);
        List<PropertyModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey);
        List<PropertyModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey,int resourcrkey);

        PropertyModel GetDataViewEdit(int id);
        PropertyModel GetBidDataForProperties(int BidRequestKey);
        bool PropertyEdit(PropertyModel item);
        IList<PropertyModel> GetPropertyManagerToAddProperty(int ResourceId, int CompnyKey);
        bool DocumentDelete(int PropertyKey, string Docname);
        bool ManagerDelete(int PropertyKey, string ResourceKey);
        PropertyModel checkmanager(string managername);
        bool Remove(int id);
        IList<PropertyModel> GetAllManager();
    }
}
