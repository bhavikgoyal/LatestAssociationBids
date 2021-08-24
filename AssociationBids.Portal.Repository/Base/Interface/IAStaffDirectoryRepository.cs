using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IAStaffDirectoryRepository : IBaseRepository
    {
        List<AStaffDirectoryModel> SearchStaff(Int64 PageSize, Int64 PageIndex, string Search, String Sort,string CompanyKey);
        Int64 Insert(AStaffDirectoryModel item);
        IList<AStaffDirectoryModel> GetAllState();
        List<AStaffDirectoryModel> GetAllGroup();
        List<AStaffDirectoryModel> GetDataViewEdit(int id);
        Int64 StaffDirectoryEditGroup(AStaffDirectoryModel item);
        Int64 StaffDirectoryEditStaff(AStaffDirectoryModel item);
        Int64 StaffDirectoryEditUser(AStaffDirectoryModel item);
        bool ResetPassword(int UserKey);
        bool CheckDuplicatedEmail(string Email);
        bool Remove(int ResourceKey);
        long Insert(AStaffDirectoryModel item, int st);

        List<AStaffDirectoryModel> AdvancedSearchStaff(long PageSize, long PageIndex, string Search, string Status, string Sort, string CompanyKey);
    }
}
