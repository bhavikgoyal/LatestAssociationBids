using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssociationBids.Portal.Repository.Base;
using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public class AStaffDirectoryService : BaseService, IAStaffDirectoryService
    {
        protected IAStaffDirectoryRepository __repository;

        public AStaffDirectoryService() : this(new AStaffDirectoryRepository()) { }

        public AStaffDirectoryService(IAStaffDirectoryRepository StaffDirectory)
        {
            ConnectionString = StaffDirectory.ConnectionString;

            __repository = StaffDirectory;
        }

        public List<AStaffDirectoryModel> SearchStaff(long PageSize, long PageIndex, string Search, string Sort,string CompanyKey)
        {
            return __repository.SearchStaff(PageSize, PageIndex, Search, Sort,CompanyKey);
        }

        public Int64 Insert(AStaffDirectoryModel item , Int32 st)
        {
            return __repository.Insert(item,st);
        }

        public Int64 StaffDirectoryEditUser(AStaffDirectoryModel item)
        {
            return __repository.StaffDirectoryEditUser(item);
        }


        public IList<AStaffDirectoryModel> GetAllState()
        {
            return __repository.GetAllState();
        }

        public List<AStaffDirectoryModel> GetAllGroup()
        {
            return __repository.GetAllGroup();
        }

        public List<AStaffDirectoryModel> GetDataViewEdit(int id)
        {
            return __repository.GetDataViewEdit(id);
        }

        public Int64 StaffDirectoryEditStaff(AStaffDirectoryModel item)
        {
            return __repository.StaffDirectoryEditStaff(item);
        }

        public Int64 StaffDirectoryEditGroup(AStaffDirectoryModel item)
        {
            return __repository.StaffDirectoryEditGroup(item);
        }

      
        public bool ResetPassword(int UserKey)
        {
            return __repository.ResetPassword(UserKey);
        }

        public bool CheckDuplicatedEmail(string Email)
        {
            return __repository.CheckDuplicatedEmail(Email);
        }
        public bool Remove(int ResourceKey)
        {
            return __repository.Remove(ResourceKey);
        }

        public List<AStaffDirectoryModel> AdvancedSearchStaff(long PageSize, long PageIndex, string Search, string Status, string Sort, string CompanyKey)
        {
            return __repository.AdvancedSearchStaff(PageSize, PageIndex, Search, Status, Sort, CompanyKey);
        }
    }
}

