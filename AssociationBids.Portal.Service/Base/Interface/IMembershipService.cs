using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface IMembershipService : IBaseService
    {
        bool Validate(MembershipModel item);
        bool IsFilterEnabled(MembershipFilterModel filter);
        MembershipFilterModel CreateFilter();
        MembershipFilterModel CreateFilter(MembershipModel item);
        MembershipFilterModel UpdateFilter(MembershipFilterModel filter);
        MembershipModel Create();
        bool Create(MembershipModel item);
        //bool Update(MembershipModel item);
        //bool Delete(int id);
        //IList<MembershipModel> GetAll();
        //MembershipModel Get(int id);
        MembershipModel GetDataViewEdit(int VendorKey);
        //IList<MembershipModel> GetAll(MembershipFilterModel filter);
        //IList<MembershipModel> GetAll(MembershipFilterModel filter, PagingModel paging);
    }
}
