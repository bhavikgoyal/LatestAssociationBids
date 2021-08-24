using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface ICompanyVendorService : IBaseService
    {
        bool Validate(CompanyVendorModel item);
        bool IsFilterEnabled(CompanyVendorFilterModel filter);
        CompanyVendorFilterModel CreateFilter();
        CompanyVendorFilterModel CreateFilter(CompanyVendorModel item);
        CompanyVendorFilterModel UpdateFilter(CompanyVendorFilterModel filter);
        CompanyVendorModel Create();
        bool Create(CompanyVendorModel item);
        bool Update(CompanyVendorModel item);
        bool Delete(int id);
        CompanyVendorModel Get(int id);
        IList<CompanyVendorModel> GetAll();
        IList<CompanyVendorModel> GetAll(CompanyVendorFilterModel filter);
        IList<CompanyVendorModel> GetAll(CompanyVendorFilterModel filter, PagingModel paging);
    }
}
