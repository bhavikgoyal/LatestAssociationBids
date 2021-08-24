using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface IACompanymangService: IBaseService
    {
      

        IList<CompanyModel> GetAll();

        IList<CompanyModel> GetAll(CompanyFilterModel filter);

        List<CompanyModel> SearchCompany(Int64 PageSize, Int64 PageIndex, string Search, String Sort, string State, int Status);

        IList<CompanyModel> GetAllStatee();

        Int64 Insert(CompanyModel companyModel);

        UserModel InsertedCompanyUser(CompanyModel companyModel);

        bool CheckDuplicatedEmaill(string Email);

        CompanyModel GetDataViewEditt(int id);

        Int64 CompanyEdit(CompanyModel item);

        Int64 CompanydefaultEdit(CompanyModel item);
        Int64 APrimarycontactEdit(CompanyModel item);

        IList<CompanyModel> Getbindservice(int CompanyKey);

        IList<CompanyModel> GetAllService();

        Int64 Removee(int CompanyKey, int ResourceKey);
        bool RemoveStaffDirecroty(Int32 ResourceKey);


        //property module:-

        PropertyModel Get(int id);

        IList<PropertyModel> GetAll(PropertyFilterModel filter);

        IList<PropertyModel> GetAll(PropertyFilterModel filter, PagingModel paging);

        Int64 Insert(PropertyModel item, string strinbuilder, string strinbuilder1);

        bool DocInsert(int PropertyKey, string strinbuilder, string strinbuilder1);

        int GetGroupKey(string managername);

        bool Updatemanager(int PropertyKey, string managername);

        IList<PropertyModel> GetAllManager(int Groupkey);

        IList<PropertyModel> GetbindDocument(int PropertyKey);

        IList<PropertyModel> GetAllCompany();

        List<PropertyModel> SearchUser(Int64 PageSize, Int64 PageIndex, string Search, String Sort);

        List<PropertyModel> SearchCompanyViewProperty(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int32 CompanyKey);

        PropertyModel GetDataViewEdit(int id);

        bool PropertyEdit(PropertyModel item);

        bool DocumentDelete(int PropertyKey, string Docname);

        bool ManagerDelete(int PropertyKey, int ResourceKey);

        int checkmanager(int PropertyKey);

        bool Remove(int id);
        bool WinFeeMain(string fromemail, string lookUpTitle, int UserKey, string UserName, DateTime ResetExpirationDate, string CompanyName);
        IList<PropertyModel> GetAllManager();

        IList<PropertyModel> GetAllState();

        //User Module:-
        List<ResourceModel> Searchcompany(int CompanyKey);
        Int64 Insert(StaffDirectoryModel item);
        Int64 ACompanyStaffInsert(StaffDirectoryModel item);
        IList<StaffDirectoryModel> GetAllStateee();
        List<StaffDirectoryModel> GetAllGroup();
        List<StaffDirectoryModel> GetDataViewEdittt(int id);
        List<StaffDirectoryModel> GetDataviewGroupCheckbox(int id);
        Int64 StaffDirectoryEditStaff(StaffDirectoryModel item);
        Int64 StaffDirectoryEditGroup(StaffDirectoryModel item);
        Int64 StaffDirectoryEditUser(StaffDirectoryModel item);
        StaffDirectoryModel GetStaffDirectoryByResource(Int32 ResourceKey);
        bool ResetPassword(int ResourceKey);
        bool CheckDuplicatedEmail(string Email);
        bool Removeee(int ResourceKey);
    }
}
