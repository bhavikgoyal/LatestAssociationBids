using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class AcitivitybyManagementCompanyService : BaseService, IAcitivitybyManagementCompanyService
    {
        protected IAcitivitybyManagementCompanyRepository __vAcitivitybyManagementCompanyService;


        public AcitivitybyManagementCompanyService()
           : this(new AcitivitybyManagementCompanyRepository()) { }

        public AcitivitybyManagementCompanyService(string connectionString)
           : this(new AcitivitybyManagementCompanyRepository(connectionString)) { }

        public AcitivitybyManagementCompanyService(AcitivitybyManagementCompanyRepository vAcitivitybyManagementCompanyService)
        {
            ConnectionString = vAcitivitybyManagementCompanyService.ConnectionString;

            __vAcitivitybyManagementCompanyService = vAcitivitybyManagementCompanyService;
        }
        public List<AcitivitybyManagementCompanyModel> Activity(long ReportPageSize, long PageIndex, string Search, string Sort,Int64 CompanyKey)
        {
            return __vAcitivitybyManagementCompanyService.Activity(ReportPageSize, PageIndex, Search, Sort, CompanyKey);
        }
        public List<AcitivitybyManagementCompanyModel> ActivityVendor(long ReportPageSize, long PageIndex, string Search, string Sort, Int64 CompanyKey)
        {
            return __vAcitivitybyManagementCompanyService.ActivityVendor(ReportPageSize, PageIndex, Search, Sort, CompanyKey);
        }
        public List<AcitivitybyManagementCompanyModel> VendorPortalActivity(long ReportPageSize, long PageIndex, string Search, string Sort, Int64 ResourceKey)
        {
            return __vAcitivitybyManagementCompanyService.VendorPortalActivity(ReportPageSize, PageIndex, Search, Sort, ResourceKey);
        }
        public List<AcitivitybyManagementCompanyModel> ActivityAssociation(long ReportPageSize, long PageIndex, string Search, string Sort, Int64 CompanyKey)
        {
            return __vAcitivitybyManagementCompanyService.ActivityAssociation(ReportPageSize, PageIndex, Search, Sort, CompanyKey);
        }
        public List<AcitivitybyManagementCompanyModel> ActivityByManager(long ReportPageSize, long PageIndex, string Search, string Sort, Int64 CompanyKey,Int64 PortalKey)
        {
            return __vAcitivitybyManagementCompanyService.ActivityByManager(ReportPageSize, PageIndex, Search, Sort, CompanyKey, PortalKey);
        }
        public List<AcitivitybyManagementCompanyModel> GetAllState()
        {
            return __vAcitivitybyManagementCompanyService.GetAllState();
        }
        public List<AcitivitybyManagementCompanyModel> GetAllVendorList(int CompanyKey)
        {
            return __vAcitivitybyManagementCompanyService.GetAllVendorList(CompanyKey);
        }
        public List<AcitivitybyManagementCompanyModel> GetAllProperty(int CompanyKey)
        {
            return __vAcitivitybyManagementCompanyService.GetAllProperty(CompanyKey);
        }
        public List<AcitivitybyManagementCompanyModel> GetAllManager(int CompanyKey)
        {
            return __vAcitivitybyManagementCompanyService.GetAllManager(CompanyKey);
        }
    }
}
