using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base.Interface
{
    public interface IMembershipRepository : IBaseRepository
    {
        bool Create(MembershipModel item);
        bool Update(MembershipModel item);
        bool Delete(int id);
        MembershipModel Get(int id);
        IList<MembershipModel> GetAll();
        IList<MembershipModel> GetAll(MembershipFilterModel filter);
        IList<MembershipModel> GetAll(MembershipFilterModel filter, PagingModel paging);
        MembershipModel GetDataViewEdit(int VendorKey);

    }
}
