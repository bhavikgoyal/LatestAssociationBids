using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface IAcitivitybyManagementCompanyService : IBaseService
    {
        List<AcitivitybyManagementCompanyModel> Activity(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort,Int64 CompanyKey);
        List<AcitivitybyManagementCompanyModel> ActivityVendor(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort, Int64 CompanyKey);
        List<AcitivitybyManagementCompanyModel> VendorPortalActivity(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort, Int64 ResourceKey);
        List<AcitivitybyManagementCompanyModel> ActivityAssociation(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort, Int64 CompanyKey);
        List<AcitivitybyManagementCompanyModel> ActivityByManager(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort, Int64 CompanyKey,Int64 PortalKey);
        List<AcitivitybyManagementCompanyModel> GetAllState();
        List<AcitivitybyManagementCompanyModel> GetAllVendorList(int CompanyKey);
        List<AcitivitybyManagementCompanyModel> GetAllProperty(int CompanyKey);
        List<AcitivitybyManagementCompanyModel> GetAllManager(int CompanyKey);
    }
}
