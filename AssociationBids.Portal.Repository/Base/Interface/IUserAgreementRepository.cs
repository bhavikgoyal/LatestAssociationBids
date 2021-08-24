using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IUserAgreementRepository : IBaseRepository
    {
        bool Create(UserAgreementModel item);
        bool Update(UserAgreementModel item);
        bool Delete(int id);
        UserAgreementModel Get(int id);
        IList<UserAgreementModel> GetAll();
        IList<UserAgreementModel> GetAll(UserAgreementFilterModel filter);
        IList<UserAgreementModel> GetAll(UserAgreementFilterModel filter, PagingModel paging);
    }
}
