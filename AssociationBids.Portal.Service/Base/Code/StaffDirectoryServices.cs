using AssociationBids.Portal.Repository.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public class StaffDirectoryServices : BaseService, IStaffDirectoryServices
    {
        protected IStaffDirectoryRepository __repository;

        public StaffDirectoryServices() : this(new StaffDirectoryRepository()) { }

        public StaffDirectoryServices(IStaffDirectoryRepository StaffDirectory)
        {
            ConnectionString = StaffDirectory.ConnectionString;

            __repository = StaffDirectory;
        }

        public List<StaffDirectoryModel> SearchStaff(long PageSize, long PageIndex, string Search, string Sort, string CompanyKey)
        {
            return __repository.SearchStaff(PageSize, PageIndex, Search, Sort,  CompanyKey);
        }

        public Int64 Insert(StaffDirectoryModel item)
        {
            return __repository.Insert(item);
        }

        public Int64 checkUserrole (int ResourceKey)
        {
            return __repository.checkUserrole(ResourceKey);
        }

        public IList<StaffDirectoryModel> GetAllState()
        {
            return __repository.GetAllState();
        }

        public List<StaffDirectoryModel> GetAllGroup()
        {
            return __repository.GetAllGroup();
        }

        public List<StaffDirectoryModel> GetDataViewEdit(int id)
        {
            return __repository.GetDataViewEdit(id);
        }

        public List<StaffDirectoryModel> GetDataviewGroupCheckbox(int id)
        {
            return __repository.GetDataviewGroupCheckbox(id);
        }
        public Int64 StaffDirectoryEditStaff(StaffDirectoryModel item)
        {
            return __repository.StaffDirectoryEditStaff(item);
        }

        public Int64 StaffDirectoryEditGroup(StaffDirectoryModel item)
        {
            return __repository.StaffDirectoryEditGroup(item);
        }

        public Int64 StaffDirectoryEditUser(StaffDirectoryModel item)
        {
            return __repository.StaffDirectoryEditUser(item);
        }

        public bool ResetPassword(int ResourceKey)
        {
            return __repository.ResetPassword(ResourceKey);
        }

        public bool CheckDuplicatedEmail(string Email)
        {
            return __repository.CheckDuplicatedEmail(Email);
        }
        public bool Remove(int ResourceKey)
        {
            return __repository.Remove(ResourceKey);
        }

        public List<StaffDirectoryModel> AdvancedSearchStaff(long PageSize, long PageIndex, string Search, string status, string Sort, string CompanyKey)
        {
            return __repository.AdvancedSearchStaff(PageSize, PageIndex, Search, status, Sort, CompanyKey);
        }
    }
}
