using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IStaffDirectoryRepository: IBaseRepository
    {
        List<StaffDirectoryModel> SearchStaff(Int64 PageSize, Int64 PageIndex, string Search, String Sort,string CompanyKey);
        Int64 Insert(StaffDirectoryModel item);
        Int64 checkUserrole(int ResourceKey);
        IList<StaffDirectoryModel> GetAllState();
        List<StaffDirectoryModel> GetAllGroup();
        List<StaffDirectoryModel> GetDataViewEdit(int id);
        List<StaffDirectoryModel> GetDataviewGroupCheckbox(int id);
        Int64 StaffDirectoryEditGroup(StaffDirectoryModel item);
        Int64 StaffDirectoryEditStaff(StaffDirectoryModel item);
        Int64 StaffDirectoryEditUser(StaffDirectoryModel item);
        bool ResetPassword(int ResourceKey);
        bool CheckDuplicatedEmail(string Email);
        bool Remove(int ResourceKey);

        List<StaffDirectoryModel> AdvancedSearchStaff(long PageSize, long PageIndex, string Search, string status, string Sort, string CompanyKey);
    }
}
