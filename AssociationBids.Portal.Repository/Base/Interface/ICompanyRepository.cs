using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface ICompanyRepository : IBaseRepository
    {
        bool Create(CompanyModel item);
        bool Update(CompanyModel item);
        bool Delete(int id);
        CompanyModel Get(int id);
        IList<CompanyModel> GetAll();
        IList<CompanyModel> GetAll(CompanyFilterModel filter);
        IList<CompanyModel> GetAll(CompanyFilterModel filter, PagingModel paging);
    }
}
