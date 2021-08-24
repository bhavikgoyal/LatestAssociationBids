using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IInsuranceRepository : IBaseRepository
    {
        bool Create(InsuranceModel item);
        bool Update(InsuranceModel item);
        bool Delete(int id);
        InsuranceModel Get(int id);
        IList<InsuranceModel> GetAll();
        IList<InsuranceModel> GetAll(InsuranceFilterModel filter);
        IList<InsuranceModel> GetAll(InsuranceFilterModel filter, PagingModel paging);
    }
}
