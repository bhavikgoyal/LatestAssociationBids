using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IUserRepository : IBaseRepository
    {
        bool Create(UserModel item);
        bool Update(UserModel item);
        bool Delete(int id);
        UserModel Get(int id);
        IList<UserModel> GetAll();
        IList<UserModel> GetAll(UserFilterModel filter);
        IList<UserModel> GetAll(UserFilterModel filter, PagingModel paging);
    }
}
