using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IAStaffDirectoryService : IBaseService
    {
        List<AStaffDirectoryModel> SearchStaff(Int64 PageSize, Int64 PageIndex, string Search, String Sort,string ComapanyKey);
        Int64 Insert(AStaffDirectoryModel item,Int32 st);
        IList<AStaffDirectoryModel> GetAllState();
        List<AStaffDirectoryModel> GetAllGroup();
        List<AStaffDirectoryModel> GetDataViewEdit(int id);
        Int64 StaffDirectoryEditStaff(AStaffDirectoryModel item);
        Int64 StaffDirectoryEditGroup(AStaffDirectoryModel item);
        Int64 StaffDirectoryEditUser(AStaffDirectoryModel item);
        bool ResetPassword(int UserKey);
        bool CheckDuplicatedEmail(string Email);
        bool Remove(int ResourceKey);

        List<AStaffDirectoryModel> AdvancedSearchStaff(long PageSize, long PageIndex, string Search, string Status, string Sort, string CompanyKey);
    }
}
