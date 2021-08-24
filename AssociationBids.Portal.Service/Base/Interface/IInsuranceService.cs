using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IInsuranceService : IBaseService
    {
        bool Validate(InsuranceModel item);
        bool IsFilterEnabled(InsuranceFilterModel filter);
        InsuranceFilterModel CreateFilter();
        InsuranceFilterModel CreateFilter(InsuranceModel item);
        InsuranceFilterModel UpdateFilter(InsuranceFilterModel filter);
        InsuranceModel Create();
        bool Create(InsuranceModel item);
        bool Update(InsuranceModel item);
        bool Delete(int id);
        InsuranceModel Get(int id);
        IList<InsuranceModel> GetAll();
        IList<InsuranceModel> GetAll(InsuranceFilterModel filter);
        IList<InsuranceModel> GetAll(InsuranceFilterModel filter, PagingModel paging);
    }
}
