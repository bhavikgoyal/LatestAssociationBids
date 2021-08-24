using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IUserService : IBaseService
    {
        bool Validate(UserModel item);
        bool IsFilterEnabled(UserFilterModel filter);
        UserFilterModel CreateFilter();
        UserFilterModel CreateFilter(UserModel item);
        UserFilterModel UpdateFilter(UserFilterModel filter);
        UserModel Create();
        bool Create(UserModel item);
        bool Update(UserModel item);
        bool Delete(int id);
        UserModel Get(int id);
        IList<UserModel> GetAll();
        IList<UserModel> GetAll(UserFilterModel filter);
        IList<UserModel> GetAll(UserFilterModel filter, PagingModel paging);
    }
}
