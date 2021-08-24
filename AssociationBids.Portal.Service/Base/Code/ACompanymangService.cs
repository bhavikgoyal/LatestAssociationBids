using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class ACompanymangService : BaseService, IACompanymangService
    {
        protected IACompanymangRepository __companyservice;

        public ACompanymangService()
        : this(new ACompanymangRepository()) { }


        public ACompanymangService(string connectionString)
           : this(new ACompanymangRepository(connectionString)) { }

        public ACompanymangService(ACompanymangRepository CompanyRepository)
        {
            ConnectionString = CompanyRepository.ConnectionString;

            __companyservice = CompanyRepository;


        }
        public virtual bool WinFeeMain(string fromemail, string lookUpTitle, int UserKey, string UserName, DateTime ResetExpirationDate, string CompanyName)
        {



            return __companyservice.WinFeeMain(fromemail,  lookUpTitle, UserKey, UserName, ResetExpirationDate, CompanyName);
        }


        public List<CompanyModel> SearchCompany(long PageSize, long PageIndex, string Search, string Sort, string State, int Status)
        {
            return __companyservice.SearchCompany(PageSize, PageIndex, Search, Sort,  State,  Status);
        }


        public virtual IList<CompanyModel> GetAll(CompanyFilterModel filter, PagingModel paging)
        {
            IList<CompanyModel> itemList = __companyservice.GetAll(UpdateFilter(filter), paging);

            // Account for last record update on a page
            if (itemList.Count == 0 && paging.TotalRecordCount > 0)
            {
                paging.PageNumber--;
                return GetAll(filter, paging);
            }
            else
            {
                return itemList;
            }
        }

        public IList<CompanyModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<CompanyModel> GetAllStatee()
        {
            return __companyservice.GetAllStatee();
        }

        public IList<CompanyModel> GetAll(CompanyFilterModel filter)
        {
            throw new NotImplementedException();
        }


        public UserModel InsertedCompanyUser(CompanyModel companyModel)
        {
            return __companyservice.InsertedCompanyUser(companyModel);
        }

        public Int64 Insert(CompanyModel companyModel)
        {
            return __companyservice.Insert(companyModel);
        }

        public bool CheckDuplicatedEmaill(string Email)
        {
            return __companyservice.CheckDuplicatedEmail(Email);
        }

        public virtual CompanyModel GetDataViewEditt(int id)
        {
            return __companyservice.GetDataViewEditt(id);
        }

        public Int64 CompanyEdit(CompanyModel item)
        {

            return __companyservice.CompanyEdit(item);

        }

        public Int64 CompanydefaultEdit(CompanyModel item)
        {

            return __companyservice.CompanydefaultEdit(item);

        }
        public Int64 APrimarycontactEdit(CompanyModel item)
        {

            return __companyservice.APrimarycontactEdit(item);

        }

        public IList<CompanyModel> Getbindservice(int CompanyKey)
        {
            return __companyservice.Getbindservice(CompanyKey);
        }

        public IList<CompanyModel> GetAllService()
        {
            return __companyservice.GetAllService();
        }


        public Int64 Removee(int CompanyKey, int ResourceKey)
        {
            return __companyservice.Removee(CompanyKey, ResourceKey);
        }
        //Property Module:-

        public virtual IList<PropertyModel> GetAll(PropertyFilterModel filter, PagingModel paging)
        {
            IList<PropertyModel> itemList = __companyservice.GetAll(UpdateFilter(filter), paging);

            // Account for last record update on a page
            if (itemList.Count == 0 && paging.TotalRecordCount > 0)
            {
                paging.PageNumber--;
                return GetAll(filter, paging);
            }
            else
            {
                return itemList;
            }
        }

        public List<PropertyModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort)
        {
            return __companyservice.SearchUser(PageSize, PageIndex, Search, Sort);
        }

        public List<PropertyModel> SearchCompanyViewProperty(long PageSize, long PageIndex, string Search, string Sort, Int32 CompanyKey)
        {
            return __companyservice.SearchCompanyViewProperty(PageSize, PageIndex, Search, Sort,CompanyKey);
        }

        public PropertyModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<PropertyModel> GetAll(PropertyFilterModel filter)
        {
            throw new NotImplementedException();
        }
        public virtual PropertyModel GetDataViewEdit(int id)
        {
            return __companyservice.GetDataViewEdit(id);
        }

        public Int64 Insert(PropertyModel item, string strinbuilder, string strinbuilder1)
        {
            return __companyservice.Insert(item, strinbuilder, strinbuilder1);
        }

        public virtual bool DocInsert(int PropertyKey, string strinbuilder, string strinbuilder1)
        {
            return __companyservice.DocInsert(PropertyKey, strinbuilder, strinbuilder1);
        }

        public int GetGroupKey(string managername)
        {
            return __companyservice.GetGroupKey(managername);
        }

        public IList<PropertyModel> GetAllState()
        {
            return __companyservice.GetAllState();
        }

        public virtual bool Updatemanager(int PropertyKey, string managername)
        {
            return __companyservice.Updatemanager(PropertyKey, managername);
        }




        public IList<PropertyModel> GetAllManager(int Groupkey)
        {
            return __companyservice.GetAllManager(Groupkey);
        }
        public IList<PropertyModel> GetAllCompany()
        {
            return __companyservice.GetAllCompany();
        }



        public virtual bool PropertyEdit(PropertyModel item)
        {

            return __companyservice.PropertyEdit(item);

        }
        public IList<PropertyModel> GetbindDocument(int PropertyKey)
        {
            return __companyservice.GetbindDocument(PropertyKey);
        }






        public virtual bool DocumentDelete(int PropertyKey, string Docname)
        {

            return __companyservice.DocumentDelete(PropertyKey, Docname);

        }

        public virtual bool ManagerDelete(int PropertyKey, int ResourceKey)
        {

            return __companyservice.ManagerDelete(PropertyKey, ResourceKey);

        }

        public int checkmanager(int PropertyKey)
        {

            return __companyservice.checkmanager(PropertyKey);

        }



        public virtual bool Remove(int id)
        {

            return __companyservice.Remove(id);

        }
        public IList<PropertyModel> GetAllManager()
        {
            throw new NotImplementedException();
        }


        //User Module:-

       

        public List<ResourceModel> Searchcompany(int CompanyKey)
        {
            return __companyservice.Searchcompany(CompanyKey);
        }



        public Int64 Insert(StaffDirectoryModel item)
        {
            return __companyservice.Insert(item);
        }

        public Int64 ACompanyStaffInsert(StaffDirectoryModel item)
        {
            return __companyservice.ACompanyStaffInsert(item);
        }

        public IList<StaffDirectoryModel> GetAllStateee()
        {
            return __companyservice.GetAllStateee();
        }

        public List<StaffDirectoryModel> GetAllGroup()
        {
            return __companyservice.GetAllGroup();
        }

        public List<StaffDirectoryModel> GetDataViewEdittt(int id)
        {
            return __companyservice.GetDataViewEdittt(id);
        }

        public List<StaffDirectoryModel> GetDataviewGroupCheckbox(int id)
        {
            return __companyservice.GetDataviewGroupCheckbox(id);
        }
        public Int64 StaffDirectoryEditStaff(StaffDirectoryModel item)
        {
            return __companyservice.StaffDirectoryEditStaff(item);
        }

        public Int64 StaffDirectoryEditGroup(StaffDirectoryModel item)
        {
            return __companyservice.StaffDirectoryEditGroup(item);
        }

        public Int64 StaffDirectoryEditUser(StaffDirectoryModel item)
        {
            return __companyservice.StaffDirectoryEditUser(item);
        }

        public StaffDirectoryModel GetStaffDirectoryByResource(Int32 ResourceKey)
        {
            return __companyservice.GetStaffDirectoryByResource(ResourceKey);
        }

        public bool ResetPassword(int ResourceKey)
        {
            return __companyservice.ResetPassword(ResourceKey);
        }

        public bool CheckDuplicatedEmail(string Email)
        {
            return __companyservice.CheckDuplicatedEmail(Email);
        }
        public bool Removeee(int ResourceKey)
        {
            return __companyservice.Remove(ResourceKey);
        }

        public bool RemoveStaffDirecroty(Int32 ResourceKey)
        {
            return __companyservice.RemoveStaffDirecroty(ResourceKey);
        }

    }
}
