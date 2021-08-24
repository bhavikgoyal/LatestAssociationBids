using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IResourceRepository : IBaseRepository
    {
        bool Create(ResourceModel item);
        bool Update(ResourceModel item);
        bool Delete(int id);
        ResourceModel Get(int id);
        IList<ResourceModel> GetAll();
        IList<ResourceModel> AppoGetAllService(string PleaseSelect);
        IList<ResourceModel> GetServiceByCompany(int CompanyKey);
        IList<VendorManagerModel> Getbindservice(int CompanyKey);
        IList<ResourceModel> GetAll(ResourceFilterModel filter);
        IList<ResourceModel> GetAll(ResourceFilterModel filter, PagingModel paging);
        List<ResourceModel> SearchUser(Int64 PageSize, Int64 PageIndex, string Search, String Sort);
        bool Insert(ResourceModel item);
        IList<ResourceModel> GetAllCompany();
        IList<ResourceModel> GetAllState();
        ResourceModel GetDataViewEdit(int id);
        ResourceModel GetDataViewEditByCompanyKey(int id);
        bool Edit(ResourceModel item);
        bool PropertyMangerProfileEdit(ResourceModel item);
        bool PropertyMangerProfileImage(Int32 ResourceKey, string Title, string Controller, string Action, string ImageName, Int64 ImageLength);
        string SaveProfilePassword(Int32 ResourceId, string OldPassword, string NewPassword);

        bool Remove(int id);

    }
}
