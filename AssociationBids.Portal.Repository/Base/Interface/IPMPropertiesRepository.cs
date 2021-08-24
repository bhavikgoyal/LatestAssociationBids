using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base.Interface
{
    public interface IPMPropertiesRepository : IBaseRepository
    {
        List<PropertyModel> SearchUser(Int64 PageSize, Int64 PageIndex, string Search, String Sort, int ResourceKey);
        List<PropertyModel> SearchBidrequest(Int64 PageSize, Int64 PageIndex, String Sort, string PropertyKey);
        List<PropertyModel> SearchWorkOrder(Int64 PageSize, Int64 PageIndex, String Sort, string PropertyKey);
        PropertyModel GetDataViewEdit(int id);
        IList<PropertyModel> GetAll(PropertyFilterModel propertyFilterModel, PagingModel paging);
        Int64 Insert(PropertyModel item, string strinbuilder, string strinbuilder1);
        //Int64 Insert(PropertyModel item);
        bool DocInsert(int PropertyKey, string strinbuilder, string strinbuilder1);
        int GetGroupKey(string managername);
        IList<BidRequestModel> GetbindDocumentBid(int CompanyKey, int ModuleKey);
        bool Updatemanager(PropertyModel item, int PropertyKey, string managername);
        IList<PropertyModel> GetAllManager(int Groupkey, int PropertyKey, int CompnyKey);
        IList<PropertyModel> GetManagerForAdd(int ResourceId, int PropertyKey, int CompnyKey);
        IList<PropertyModel> GetbindDocument(int PropertyKey);
        IList<PropertyModel> GetAllCompany();
        IList<PropertyModel> GetAllState();
        IList<PropertyModel> GetAll();
    
        IList<PropertyModel> GetAll(PropertyFilterModel filter);
        bool PropertyEdit(PropertyModel item);
        IList<PropertyModel> GetPropertyManagerToAddProperty(int Groupkey, int CompnyKey);
        List<PropertyModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey);
        List<PropertyModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey,int resourcekey);

        PropertyModel GetBidDataForProperties(int BidRequestKey);
        bool DocumentDelete(int PropertyKey, string Docname);
        bool ManagerDelete(int PropertyKey, string ResourceKey);
        PropertyModel checkmanager(string managername);
        //bool Delete(int id);
        bool Remove(int id);
    }
}
