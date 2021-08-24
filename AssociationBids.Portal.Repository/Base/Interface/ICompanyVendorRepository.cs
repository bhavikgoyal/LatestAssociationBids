using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface ICompanyVendorRepository : IBaseRepository
    {
        bool Create(CompanyVendorModel item);
        bool Update(CompanyVendorModel item);
        bool Delete(int id);
        CompanyVendorModel Get(int id);
        IList<CompanyVendorModel> GetAll();
        IList<CompanyVendorModel> GetAll(CompanyVendorFilterModel filter);
        IList<CompanyVendorModel> GetAll(CompanyVendorFilterModel filter, PagingModel paging);
    }
}
