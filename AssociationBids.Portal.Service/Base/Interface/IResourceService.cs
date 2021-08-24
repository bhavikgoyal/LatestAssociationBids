using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IResourceService : IBaseService
    {
        bool Validate(ResourceModel item);
        bool IsFilterEnabled(ResourceFilterModel filter);
        ResourceFilterModel CreateFilter();
        ResourceFilterModel CreateFilter(ResourceModel item);
        ResourceFilterModel UpdateFilter(ResourceFilterModel filter);
        ResourceModel Create();
        IList<ResourceModel> AppoGetAllService(string PleaseSelect);
        IList<ResourceModel> GetServiceByCompany(int CompanyKey);
        IList<VendorManagerModel> Getbindservice(int CompanyKey);
        bool Create(ResourceModel item);
        bool Update(ResourceModel item);
        bool Delete(int id);
        ResourceModel Get(int id);
        ResourceModel GetDataViewEditByCompanyKey(int id);
        IList<ResourceModel> GetAll();
        IList<ResourceModel> GetAll(ResourceFilterModel filter);
        IList<ResourceModel> GetAll(ResourceFilterModel filter, PagingModel paging);
        List<ResourceModel> SearchUser(Int64 PageSize, Int64 PageIndex, string Search, String Sort);
        bool Insert(ResourceModel item);
        IList<ResourceModel> GetAllCompany();
        IList<ResourceModel> GetAllState();
        ResourceModel GetDataViewEdit(int id);
        bool Edit(ResourceModel item);
        bool PropertyMangerProfileEdit(ResourceModel item);
        bool PropertyMangerProfileImage(Int32 ResourceKey, string Title, string Controller, string Action, string ImageName, Int64 ImageLength);
        string SaveProfilePassword(Int32 ResourceId, string OldPassword, string NewPassword);
        bool Remove(int id);
    }
}
