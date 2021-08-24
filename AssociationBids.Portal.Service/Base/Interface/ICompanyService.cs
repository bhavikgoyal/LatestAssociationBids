using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface ICompanyService : IBaseService
    {
        bool Validate(CompanyModel item);
        bool IsFilterEnabled(CompanyFilterModel filter);
        CompanyFilterModel CreateFilter();
        CompanyFilterModel CreateFilter(CompanyModel item);
        CompanyFilterModel UpdateFilter(CompanyFilterModel filter);
        CompanyModel Create();
        bool Create(CompanyModel item);
        bool Update(CompanyModel item);
        bool Delete(int id);
        CompanyModel Get(int id);
        IList<CompanyModel> GetAll();
        IList<CompanyModel> GetAll(CompanyFilterModel filter);
        IList<CompanyModel> GetAll(CompanyFilterModel filter, PagingModel paging);
    }
}
