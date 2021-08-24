using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IUserAgreementService : IBaseService
    {
        bool Validate(UserAgreementModel item);
        bool IsFilterEnabled(UserAgreementFilterModel filter);
        UserAgreementFilterModel CreateFilter();
        UserAgreementFilterModel CreateFilter(UserAgreementModel item);
        UserAgreementFilterModel UpdateFilter(UserAgreementFilterModel filter);
        UserAgreementModel Create();
        bool Create(UserAgreementModel item);
        bool Update(UserAgreementModel item);
        bool Delete(int id);
        UserAgreementModel Get(int id);
        IList<UserAgreementModel> GetAll();
        IList<UserAgreementModel> GetAll(UserAgreementFilterModel filter);
        IList<UserAgreementModel> GetAll(UserAgreementFilterModel filter, PagingModel paging);
    }
}
