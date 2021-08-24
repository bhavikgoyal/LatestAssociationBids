using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base.Interface
{
    public interface IAcitivitybyManagementCompanyRepository : IBaseRepository
    {
        List<AcitivitybyManagementCompanyModel> Activity(long ReportPageSize, long PageIndex, string Search, string Sort,long CompanyKey);
        List<AcitivitybyManagementCompanyModel> ActivityVendor(long ReportPageSize, long PageIndex, string Search, string Sort, long CompanyKey);
        List<AcitivitybyManagementCompanyModel> VendorPortalActivity(long ReportPageSize, long PageIndex, string Search, string Sort, long ResourceKey);
        List<AcitivitybyManagementCompanyModel> ActivityAssociation(long ReportPageSize, long PageIndex, string Search, string Sort, long CompanyKey);
        List<AcitivitybyManagementCompanyModel> ActivityByManager(long ReportPageSize, long PageIndex, string Search, string Sort, long CompanyKey,long PortalKey);
        List<AcitivitybyManagementCompanyModel> GetAllState();
        List<AcitivitybyManagementCompanyModel> GetAllVendorList(int CompanyKey);
        List<AcitivitybyManagementCompanyModel> GetAllProperty(int CompanyKey);
        List<AcitivitybyManagementCompanyModel> GetAllManager(int CompanyKey);
    }
}
